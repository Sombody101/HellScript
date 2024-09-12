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

    public static HellAsmGenerator StartProgramParse(HellAsm_Parser parser, BinaryWriter bw)
    {
        var sw = Stopwatch.StartNew();

        // Parse program
        var visitor = new HellAsmGenerator();
        visitor.Visit(parser.program());

        // Resolve labels
        visitor.constants.FinalizeLabelReferences(visitor);
        visitor.constants.FinalizeMethodReferences(visitor);

        // Add an exit op if there isn't one (or the runtime will keep going and give an index exception)
        if (visitor.bytecodeBuffer[^1] != (byte)Opcode.EXIT)
            visitor.bytecodeBuffer.Add((byte)Opcode.EXIT);

        sw.Stop();

        Console.WriteLine($"Compilation took {sw.ElapsedMilliseconds}ms");

        return visitor;
    }

    internal readonly List<byte> bytecodeBuffer;
    internal readonly List<ParseError> parseErrors;

    internal readonly ConstantsManager constants;

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

    internal sealed class ParseError
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

    internal sealed class ConstantsManager
    {
        internal readonly Dictionary<string, int> constantStrings;
        internal readonly Dictionary<BigInteger, int> constantNumbers;
        internal readonly List<MethodSignature> methodSignatures;
        private readonly List<MethodSignature> methodReferences;

        // Should only get to a height of 2 of the hell assembly was build correctly
        private readonly Stack<List<AwaitingBranch>> branches;
        private readonly Stack<List<AwaitingBranch>> labels;
        private readonly List<int> offsetCounter;

        internal ConstantsManager()
        {
            constantStrings = [];
            constantNumbers = [];
            methodSignatures = [];
            methodReferences = [];
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
            branches.First().Add(new(labelReference, globalPosition));
        }

        public void AddLabelDefinition(string labelName, int offset)
        {
            labels.First().Add(new(labelName, offset));
        }

        public void PushBranchContext()
        {
            labels.Push([]);
            branches.Push([]);
            offsetCounter.Add(0);
        }

        public void PopBranchContext()
        {
            labels.Pop();
            branches.Pop();
            offsetCounter.RemoveAt(offsetCounter.Count - 1);
        }

        public void IncreaseProgramCounter(int count = 1)
        {
            if (count is 0)
                return;

            // Increase all of the counters
            for (int i = 0; i < offsetCounter.Count; ++i)
                offsetCounter[i] += count;
        }

        public void IncreaseGlobalCounter(int count = 1)
        {
            if (count is 0)
                return;

            offsetCounter[0] += count;
        }

        public int GetCounter()
        {
            return offsetCounter[^1];
        }

        public int GetGlobalCounter()
        {
            return offsetCounter[0];
        }

        public int SetMethodSignature(string methodName, short argCount, int bytecodeOffset)
        {
            if (methodSignatures.Any(sig => sig.Name == methodName))
            {
                throw new Exception($"Method '{methodName}' has already been defined");
            }

            MethodSignature newSig = new()
            {
                Name = methodName,
                ArgCount = argCount,
                Location = bytecodeOffset
            };

            methodSignatures.Add(newSig);

            return methodSignatures.Count - 1;
        }

        public int GetMethodSignature(string methodName)
        {
            int count = methodSignatures.Count;
            for (int i = 0; i < count; ++i)
            {
                if (methodSignatures[i].Name == methodName)
                    return i;
            }

            throw new Exception($"Reference to undefined method '{methodName}'");
        }

        public void AddMethodReference(string methodReference, int globalPosition)
        {
            MethodSignature newSig = new()
            {
                Name = methodReference,
                Location = globalPosition
            };

            methodReferences.Add(newSig);
        }

        public void FinalizeLabelReferences(HellAsmGenerator generator)
        {
            // Get the last elements on the stack (to also resolve methods)
            var branches = this.branches.First();
            var labels = this.labels.First();

            var bytecode = generator.bytecodeBuffer;

            foreach (var label in labels)
            {
                var sbranches = branches.Where(branch => branch.Name == label.Name).ToArray();

                // No branches with the wanted label
                if (sbranches.Length is 0)
                    continue;

                byte[] data = BitConverter.GetBytes(label.Location);

                foreach (var branch in sbranches)
                {
                    // The label location is an offset (to work in methods and global scope), the branch location is absolute within the bytecode
                    bytecode.RemoveRange(branch.Location, 4);
                    bytecode.InsertRange(branch.Location, data);
                }
            }
        }

        public void FinalizeMethodReferences(HellAsmGenerator generator)
        {
            var bytecode = generator.bytecodeBuffer;

            foreach (var methodName in methodSignatures.Select(method => method.Name))
            {
                try
                {
                    var references = methodReferences.Where(reference => reference.Name == methodName).ToArray();

                    if (references.Length is 0)
                        continue;

                    byte[] data = BitConverter.GetBytes(GetMethodSignature(methodName));

                    foreach (var reference in references.Select(reference => reference.Location))
                    {
                        bytecode.RemoveRange(reference, 4);
                        bytecode.InsertRange(reference, data);
                    }
                }
                catch (Exception ex)
                {
                    generator.ReportError(ex);
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

            // Method table
            bw.Write(methodSignatures.Count);

            for (int i = 0; i < methodSignatures.Count; ++i)
            {
                var method = methodSignatures[i];

                // Index
                bw.Write(method.Location);

                // Method name (Length/Value)
                string name = method.Name;
                bw.Write(name.Length);
                bw.Write(Encoding.Unicode.GetBytes(name));

                // Method arguments
                bw.Write(method.ArgCount);
            }
        }
    }

    private sealed class AwaitingBranch
    {
        public AwaitingBranch(string name, int location)
        {
            Name = name;
            Location = location;
        }

        public readonly string Name;
        public readonly int Location;
    }

    internal sealed class MethodSignature
    {
        public string Name { get; init; } = string.Empty;
        public short ArgCount { get; init; }
        public int Location { get; init; }

        public override string ToString()
        {
            return $"[ {Name}, {ArgCount}, {Location} ]";
        }
    }

    public override byte[] VisitProgramLine([NotNull] HellAsm_Parser.ProgramLineContext context)
    {
        if (context.line() is { } line)
        {
            byte[] code = Visit(line);

            bytecodeBuffer.AddRange(code);
        }
        else
        {
            constants.PushBranchContext();
            byte[] method = Visit(context.methodDeclaration());

            bytecodeBuffer.AddRange(method);

            constants.FinalizeLabelReferences(this);
            constants.PopBranchContext();

            // constants.IncreaseProgramCounter(method.Length);
        }

        return empty;
    }

    public override byte[] VisitLine([NotNull] HellAsm_Parser.LineContext context)
    {
        byte[] bytes;

        if (context.opcode() is { } opcode)
        {
            bytes = Visit(opcode);
            constants.IncreaseProgramCounter(bytes.Length);
        }
        else
        {
            bytes = Visit(context.label());
        }

        return bytes;
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

                // Add a new unresolved branch (with an absolute location)
                constants.AddBranch(label.GetText(), constants.GetGlobalCounter() + 1);

                // Add null bytes for now
                outputBuffer.AddRange(BytecodeHelpers.EmptyByte(argCount));
            }
            else if (argCount is not 0)
            {
                var argument = context.argument()
                    ?? throw new NoArgumentSuppliedException(opcode);

                var arguments = Visit(argument);

                if (argCount != arguments.Length)
                    Array.Resize(ref arguments, argCount);

                outputBuffer.AddRange(arguments);
            }

            return [.. outputBuffer];
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
                    throw new Exception($"A fast integer cannot be negative: {fastText}");
                    // Signed
                    // if (!int.TryParse(fastText, out int i32))
                    //     throw new InvalidFastConstantException(fastText);
                    // 
                    // outputBytes = BitConverter.GetBytes(i32);
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
            // var methodReference = constants.GetMethodSignature(methodIdentifier.GetText());
            //return BitConverter.GetBytes(methodReference);

            constants.AddMethodReference(methodIdentifier.GetText(), constants.GetGlobalCounter() + 1);

            return BytecodeHelpers.EmptyByte(4);

            throw new NotImplementedException($"The method datatype has not been implemented: {methodIdentifier.GetText()}");
        }

        throw new NotImplementedException("The wanted datatype has not been implemented yet.");
    }

    public override byte[] VisitMethodDeclaration([NotNull] HellAsm_Parser.MethodDeclarationContext context)
    {
        var methodIdentifier = context.Identifier();

        try
        {
            string methodName = methodIdentifier.GetText();
            string argCountStr = context.IntegerConstant().GetText();

            if (!short.TryParse(argCountStr, out short argCount))
            {
                throw new Exception($"Invalid constant short argument count '{argCountStr}'");
            }

            int methodStartOffset = bytecodeBuffer.Count + 5;
            _ = constants.SetMethodSignature(methodName, argCount, methodStartOffset);

            List<byte> blockBuffer = [];

            constants.IncreaseGlobalCounter(5);

            foreach (var line in context.line())
            {
                byte[] bytes = Visit(line);
                blockBuffer.AddRange(bytes);
                //constants.IncreaseProgramCounter(bytes.Length);
            }

            // Add JMP operation to skip over the method (in sequential bytecode)
            List<byte> jmpOp = [(byte)Opcode.JMP];
            int jumpLocation = blockBuffer.Count + methodStartOffset;
            jmpOp.AddRange(BitConverter.GetBytes(jumpLocation));

            blockBuffer.InsertRange(0, [.. jmpOp]);

            return [.. blockBuffer];
        }
        catch (Exception ex)
        {
            ReportError(ex, context);
        }

        return empty;
    }
}
