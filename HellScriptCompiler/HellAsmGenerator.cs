using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using HellScriptShared.Bytecode;
using HellScriptShared.Exceptions;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace HellScriptCompiler;

internal sealed class HellAsmGenerator : HellAsm_ParserBaseVisitor<byte[]>
{
    private static readonly byte[] empty = [];

    public static void StartProgramParse(HellAsm_Parser parser, BinaryWriter bw)
    {
        var sw = Stopwatch.StartNew();

        // Parse program
        var visitor = new HellAsmGenerator();
        visitor.Visit(parser.program());

        // Resolve labels
        visitor.constants.FinalizeLabelReferences(visitor);

        sw.Stop();

        Console.WriteLine($"Compilation took {sw.ElapsedMilliseconds}ms");

        Console.WriteLine($"Final bytecode length: {visitor.bytecodeBuffer.Count}");
        Console.WriteLine($"Assembly errors: {visitor.parseErrors.Count}");
        ParseError.PrintErrors(visitor.parseErrors);

        if (visitor.parseErrors.Count > 0)
            return;

        Console.WriteLine($"\nStrings ({visitor.constants.constantStrings.Count}):");

        int count = 0;
        foreach (var str in visitor.constants.constantStrings)
        {
            Console.WriteLine($"{count++}: {str}");
        }

        Console.WriteLine($"\nNumbers ({visitor.constants.constantNumbers.Count}):");

        count = 0;
        foreach (var num in visitor.constants.constantNumbers)
        {
            Console.WriteLine($"{count++}: {num}");
        }

        Console.WriteLine("\nDecompile:");
        List<byte> bytecode = visitor.bytecodeBuffer;
        for (int i = 0; i < bytecode.Count; ++i)
        {
            byte b = bytecode[i];

            try
            {
                Opcode op = b.AsOpcode();

                Console.Write($"{op,-15} ");
                int argCount = BytecodeHelpers.ArgumentCount(op);

                if (argCount > 0)
                {
                    var rng = bytecode.GetRange(i + 1, argCount).ToArray();
                    var args = argCount is 4 
                        ? BitConverter.ToInt32(rng)
                        : BitConverter.ToInt16(rng);

                    Console.WriteLine(args);
                    i += argCount;
                }
                else
                    Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invalid opcode: {e.Message}\n{b}");
                break;
            }
        }

        Console.WriteLine("\nWriting binary to file...");

        /* Write it out to the file */

        // Magic number
        bw.Write(Encoding.ASCII.GetBytes(BytecodeHelpers.MagicNumber));

        visitor.constants.DumpConstantValues(bw);

        // Write the bytecode
        bw.Write(visitor.bytecodeBuffer.ToArray());

        Console.WriteLine($"Wrote {bw.BaseStream.Length} bytes");
    }

    private readonly List<byte> bytecodeBuffer;
    private readonly List<ParseError> parseErrors;

    private readonly ConstantsManager constants;

    public HellAsmGenerator()
    {
        constants = new();
        bytecodeBuffer = [];
        parseErrors = [];
    }

    private void ReportError(Exception ex, [Optional] ParserRuleContext context)
    {
        parseErrors.Add(new ParseError(ex, context));
    }

    private sealed class ParseError
    {
        public ParseError(Exception ex, [Optional] ParserRuleContext context)
        {
            Exception = ex;

            if (context is not null)
            {
                Location = FormatBranchLocation(context);
            }
        }

        public Exception Exception { get; init; }
        public string? Location { get; init; }

        public static string FormatBranchLocation(ParserRuleContext ctx)
        {
            return $"{ctx.Start.Line}:{ctx.Start.Column}";
        }

        public static void PrintErrors(List<ParseError> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine($"{error.GetType().Name} [{error.Location}]: {error.Exception.Message}.");
            }
        }
    }

    private sealed class ConstantsManager
    {
        internal readonly Dictionary<string, int> constantStrings;
        internal readonly Dictionary<BigInteger, int> constantNumbers;
        internal readonly Dictionary<string, int> methodSignatures;

        // Should only get to a height of 2 of the hell assembly was build correctly
        internal readonly Stack<List<AwaitingBranch>> branches;
        internal readonly Stack<List<AwaitingBranch>> labels;
        internal readonly List<int> offsetCounter;

        internal ConstantsManager()
        {
            constantStrings = [];
            constantNumbers = [];
            methodSignatures = [];
            branches = [];
            labels = [];
            offsetCounter = [0];

            // Global branches
            branches.Push([]);
            labels.Push([]);
        }

        public int GetIndexForString(string value)
        {
            if (!constantStrings.TryGetValue(value, out int key))
            {
                int newIndex = constantStrings.Count;
                constantStrings.Add(value, newIndex);

                return newIndex;
            }

            return key;
        }

        public int GetIndexForNumber(byte[] value, bool unsigned = false)
        {
            BigInteger newBig = new(value, unsigned);

            if (!constantNumbers.TryGetValue(newBig, out int key))
            {
                int newIndex = constantNumbers.Count;
                constantNumbers.Add(newBig, newIndex);

                return newIndex;
            }

            return key;
        }

        public int GetIndexForNumber(string value)
        {
            BigInteger newBig = BigInteger.Parse(value);

            if (!constantNumbers.TryGetValue(newBig, out int key))
            {
                int newIndex = constantNumbers.Count;
                constantNumbers.Add(newBig, newIndex);

                return newIndex;
            }

            return key;
        }

        public void AddBranch(string labelReference, int globalPosition)
        {
            branches.Last().Add(new(labelReference, globalPosition));
        }

        public void AddLabelDefinition(string labelName, int offset)
        {
            labels.Last().Add(new(labelName, offset));
        }

        public void IncreaseProgramCounter(int count = 1)
        {
            if (count is 0)
                return;

            // Increase all of the counters
            for (int i = 0; i < offsetCounter.Count; ++i)
                offsetCounter[i] += count;
        }

        public int GetCounter()
        {
            return offsetCounter[^1];
        }

        public int SetMethodSignature(string methodName)
        {
            if (methodSignatures.TryGetValue(methodName, out _))
            {
                throw new Exception($"Method '{methodName}' has already been defined");
            }

            int newIndex = methodSignatures.Count;
            methodSignatures.Add(methodName, newIndex);

            return newIndex;
        }

        public int GetMethodSignature(string methodName)
        {
            if (!methodSignatures.TryGetValue(methodName, out int key))
            {
                throw new Exception($"Reference to undefined method '{methodName}'");
            }

            return key;
        }

        public void FinalizeLabelReferences(HellAsmGenerator generator)
        {
            // Get the last elements on the stack (to also resolve methods)
            var branches = this.branches.Last();
            var labels = this.labels.Last();

            foreach (var label in labels)
            {
                var sbranches = branches.Where(branch => branch.ReferenceName == label.ReferenceName).ToArray();

                // No branches with the wanted label
                if (sbranches.Length is 0)
                    continue;

                var bytecode = generator.bytecodeBuffer;
                foreach (var branch in sbranches)
                {
                    byte[] data = BitConverter.GetBytes(label.ReferenceLocation);

                    // The label location is an offset, the branch location is absolute within the bytecode array
                    bytecode.RemoveRange(branch.ReferenceLocation, data.Length);
                    bytecode.InsertRange(branch.ReferenceLocation, data);
                }
            }
        }

        public void DumpConstantValues(BinaryWriter bw)
        {
            // Specify how many strings there are
            bw.Write(constantStrings.Count);

            foreach (string str in constantStrings.Keys)
            {
                // String length
                bw.Write(str.Length);

                // String data
                bw.Write(Encoding.Unicode.GetBytes(str));
            }

            // Number count
            bw.Write(constantNumbers.Count);

            foreach (var bi in constantNumbers.Keys)
            {
                byte[] bytes = bi.ToByteArray();

                // Number length
                bw.Write(bytes.Length);

                // Number data
                bw.Write(bytes);
            }
        }
    }

    private sealed class AwaitingBranch
    {
        public AwaitingBranch(string name, int location)
        {
            ReferenceName = name;
            ReferenceLocation = location;
        }

        public readonly string ReferenceName;
        public readonly int ReferenceLocation;
    }

    public override byte[] VisitProgramLine([NotNull] HellAsm_Parser.ProgramLineContext context)
    {
        if (context.line() is { } line)
        {
            byte[] bytes = Visit(line);

            if (bytes.Length is 0)
                return empty;

            bytecodeBuffer.AddRange(bytes);
            constants.IncreaseProgramCounter(bytes.Length);
        }
        else
        {
            throw new Exception("Methods are not supported");
            bytecodeBuffer.AddRange(Visit(context.methodDeclaration()));
        }

        return empty;
    }

    public override byte[] VisitOpcode([NotNull] HellAsm_Parser.OpcodeContext context)
    {
        try
        {
            List<byte> outputBuffer = [];

            var opcode = context.Identifier().GetText().AsOpcode();
            outputBuffer.Add((byte)opcode);

            var argCount = opcode.ArgumentCount();

            if (opcode.IsBranch())
            {
                if (context.argument() is not { } arg)
                    throw new Exception("No argument given for branch");

                if (arg.Identifier() is not { } label)
                    throw new Exception("Given branch argument is not a label");

                // Add a new unresolved branch
                constants.AddBranch(label.GetText(), bytecodeBuffer.Count + outputBuffer.Count);

                // Add null bytes for now
                outputBuffer.AddRange(BytecodeHelpers.EmptyByte(argCount));
            }
            else if (argCount is not 0)
            {
                var argument = context.argument() ?? throw new NoArgumentSuppliedException(opcode);
                var arguments = Visit(argument);

                if (argCount != arguments.Length)
                    Array.Resize(ref arguments, argCount);

                outputBuffer.AddRange(arguments);
            }

            return [..outputBuffer];
        }
        catch (Exception ex)
        {
            ReportError(ex, context);
        }

        return empty;
    }

    public override byte[] VisitLabel([NotNull] HellAsm_Parser.LabelContext context)
    {
        try
        {
            // Remove the colon
            var label = context.GetText()[..^1];

            constants.AddLabelDefinition(label, constants.GetCounter());
        }
        catch (Exception ex)
        {
            ReportError(ex, context);
        }

        return empty;
    }

    public override byte[] VisitArgument([NotNull] HellAsm_Parser.ArgumentContext context)
    {
        if (context.StringConstant() is { } stringLiteral)
        {
            // Remove the double quotes and add it to the constants list
            int index = constants.GetIndexForString(stringLiteral.GetText()[1..^1]);

            // Return index as byte[]
            return BitConverter.GetBytes(index);
        }
        else if (context.FastConstant() is { } fastConstant)
        {
            string fastText = fastConstant.GetText()[1..^1].Replace("_", string.Empty);

            byte[] outputBytes = empty;

            try
            {
                if (fastText.Contains('.'))
                {
                    // Floating
                    if (!float.TryParse(fastText, out float f32))
                        throw new InvalidFastConstantException(fastText);

                    outputBytes = BitConverter.GetBytes(f32);
                }
                else if (fastText.StartsWith('-'))
                {
                    // Signed
                    if (!int.TryParse(fastText, out int i32))
                        throw new InvalidFastConstantException(fastText);

                    outputBytes = BitConverter.GetBytes(i32);
                }
                else
                {
                    // Unsigned
                    if (!uint.TryParse(fastText, out uint ui32))
                        throw new InvalidFastConstantException(fastText);

                    outputBytes = BitConverter.GetBytes(ui32);
                }
            }
            catch (Exception ex)
            {
                ReportError(ex, context);
            }

            return outputBytes;
        }
        else if (context.IntegerConstant() is { } numberLiteral)
        {
            string numberText = numberLiteral.GetText();

            int index = constants.GetIndexForNumber(numberText);
            return BitConverter.GetBytes(index);
        }
        else if (context.FloatingConstant() is { } doubleLiteral)
        {
            throw new NotImplementedException($"The decimal datatype has not been implemented: {doubleLiteral.GetText()}");
        }
        else if (context.Identifier() is { } methodIdentifier)
        {
            throw new NotImplementedException($"The method datatype has not been implemented: {methodIdentifier.GetText()}");
        }

        throw new NotImplementedException("The wanted datatype has not been implemented yet.");
    }

    // public override object VisitMethodDeclaration([NotNull] HellAsm_Parser.MethodDeclarationContext context)
    // {
    //     var methodIdentifier = context.Identifier();
    // 
    //     try
    //     {
    //         string methodName = methodIdentifier.GetText();
    //         int signature = constants.SetMethodSignature(methodName);
    // 
    //         List<byte> blockBuffer = new()
    //         {
    //             signature
    //         };
    // 
    //         foreach (var line in context.line())
    //             blockBuffer.Add((byte)Visit(line)!);
    // 
    //         return blockBuffer.ToArray();
    //     }
    //     catch (Exception ex)
    //     {
    //         ReportError(ex, context);
    //     }
    // }
    // 
    // public override object VisitMethodReference([NotNull] HellAsm_Parser.MethodReferenceContext context)
    // {
    //     try
    //     {
    //         var methodReference = constants.GetMethodSignature(context.Identifier().GetText());
    // 
    //         return BitConverter.GetBytes(methodReference);
    //     }
    //     catch (Exception ex)
    //     {
    //         ReportError(ex, context);
    //     }
    // 
    //     return null;
    // 
    //     // foreach (var arg in context.argumentList().Identifier())
    //     // {
    //     // 
    //     // }
    // }
}
