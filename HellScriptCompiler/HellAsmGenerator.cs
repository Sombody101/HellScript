using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using HellScriptShared.Bytecode;
using HellScriptShared.Exceptions;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;

namespace HellScriptCompiler;

internal sealed class HellAsmGenerator : HellAsm_ParserBaseVisitor<byte[]>
{
    private static readonly byte[] empty = [];
    private static readonly byte[] emptyQ = [0, 0, 0, 0];

    public static HellAsmGenerator StartProgramParse(HellAsm_Parser parser)
    {
        var sw = Stopwatch.StartNew();

        // Parse program
        var visitor = new HellAsmGenerator();
        visitor.Visit(parser.program());

        // Resolve labels
        visitor.constants.FinalizeLabelReferences(visitor);
        visitor.constants.FinalizeMethodReferences(visitor);
        visitor.constants.FinalizeStructureReferences(visitor);

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

        // Method data
        internal readonly List<HellFunction> methodSignatures;
        private readonly List<HellFunction> methodReferences;
        private readonly List<string> methodLocals;

        internal readonly List<HellStructMetadata> structMetadata;
        private readonly List<AwaitingBranch> structReferences;

        // Should only get to a height of 2 of the hell assembly was build correctly
        private readonly Stack<List<AwaitingBranch>> branches;
        private readonly Stack<List<AwaitingBranch>> labels;
        private readonly List<int> offsetCounter;

        internal ConstantsManager()
        {
            branches = [];
            labels = [];
            offsetCounter = [0];

            constantStrings = [];
            constantNumbers = [];

            methodSignatures = [];
            methodReferences = [];
            methodLocals = [];

            structMetadata = [];
            structReferences = [];

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
            if (methodSignatures.Exists(sig => sig.Name == methodName))
            {
                throw new Exception($"Method '{methodName}' has already been defined");
            }

            HellFunction newSig = new(methodName, argCount, bytecodeOffset);

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
            HellFunction newSig = new(methodReference, -1, globalPosition);
            methodReferences.Add(newSig);
        }

        public void SetMethodLocals(string[] names)
        {
            methodLocals.AddRange(names);
        }

        public int GetMethodLocal(string name)
        {
            for (int i = 0; i < methodLocals.Count; ++i)
            {
                if (name == methodLocals[i])
                    return i;
            }

            throw new Exception($"Reference to undefined local '{name}'");
        }

        public void ClearMethodLocals()
        {
            methodLocals.Clear();
        }

        public void AddStructureMetadata(string name, string[] memberNames)
        {
            if (structMetadata.Exists(smd => smd.Name == name))
                throw new Exception($"Duplicate structure definition '{name}'");

            structMetadata.Add(new()
            {
                Name = name,
                MemberCount = memberNames.Length,
                MemberNames = memberNames
            });
        }

        public void AddStructReference(string name, int position)
        {
            structReferences.Add(new(name, position));
        }

        public int GetStructReference(string name)
        {
            string[] segments = name.Split("::");

            if (segments.Length > 2)
                throw new Exception("Structure references can only target direct children.");

            int structIndex = structMetadata.Count;
            for (int i = 0; i < structIndex; ++i)
            {
                if (structMetadata[i].Name == segments[0])
                {
                    structIndex = i;
                    break;
                }
            }

            if (structIndex == structMetadata.Count)
                throw new Exception($"Reference to undefined struct '{segments[0]}'");

            if (segments.Length is 1)
                return structIndex;

            var wantedStruct = structMetadata[structIndex].MemberNames;
            for (int i = 0; i < wantedStruct.Length; ++i)
                if (wantedStruct[i] == segments[1])
                    return i;

            throw new Exception($"Reference to undefined struct member '{segments[1]}'");
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

                    foreach (var reference in references.Select(reference => reference.BytecodePosition))
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

        public void FinalizeStructureReferences(HellAsmGenerator generator)
        {
            var bytecode = generator.bytecodeBuffer;

            foreach (var @struct in structMetadata.Select(strct => strct.Name))
            {
                var references = structReferences.Where(refs => refs.Name.StartsWith(@struct));

                if (references.Count() is 0)
                    continue;

                foreach (var refs in references)
                {
                    int structIndex = GetStructReference(refs.Name);
                    byte[] data = BitConverter.GetBytes(structIndex);

                    bytecode.RemoveRange(refs.Location, 4);
                    bytecode.InsertRange(refs.Location, data);
                }
            }
        }

        public void DumpConstantValues(BinaryWriter bw)
        {
            // Specify how many strings there are
            bw.Write(constantStrings.Count);

            foreach (string str in constantStrings.Keys)
            {
                bw.Write(str);
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
                methodSignatures[i].SerializeToStream(bw);
            }

            // Structures
            bw.Write(structMetadata.Count);

            for (int i = 0; i < structMetadata.Count; ++i)
            {
                structMetadata[i].SerializeToStream(bw);
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

    public override byte[] VisitProgramLine([NotNull] HellAsm_Parser.ProgramLineContext context)
    {
        if (context.line() is { } line)
        {
            byte[] code = Visit(line);

            bytecodeBuffer.AddRange(code);
        }
        else if (context.methodDeclaration() is { } methodDeclaration)
        {
            constants.PushBranchContext();
            byte[] method = Visit(methodDeclaration);

            bytecodeBuffer.AddRange(method);

            constants.FinalizeLabelReferences(this);
            constants.PopBranchContext();
        }
        else
        {
            var structDeclaration = context.structDeclaration();
            string structName = structDeclaration.Identifier().GetText();

            var members = structDeclaration.fieldDeclaration();
            List<string> memberNames = new(members.Length);
            foreach (var local in members)
            {
                string localName = local.Identifier().GetText();

                if (memberNames.Contains(localName))
                    ReportError(new Exception($"Duplicate field '{localName}' in struct {structName}"), local);

                memberNames.Add(localName);
            }

            constants.AddStructureMetadata(structName, [.. memberNames]);
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
                outputBuffer.AddRange(emptyQ);
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
        if (context.LocalReference() is { } localRef)
        {
            string localName = localRef.GetText()[1..];
            int local;

            try
            {
                local = constants.GetMethodLocal(localName);
            }
            catch (Exception ex)
            {
                try
                {
                    local = HellGlobals.GetVariable(localName);
                }
                catch
                {
                    ReportError(ex, context);
                    return empty;
                }
            }

            return BitConverter.GetBytes(local);
        }
        else if (context.StructReference() is { } structRef)
        {
            string structName = structRef.GetText()[1..];
            constants.AddStructReference(structName, constants.GetGlobalCounter() + 1);

            return emptyQ;
        }
        else if (context.StringConstant() is { } stringLiteral)
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
                else // if (fastText.StartsWith('-'))
                {
                    // Signed
                    if (!int.TryParse(fastText, out int i32))
                        throw new InvalidFastConstantException(fastText);

                    outputBytes = BitConverter.GetBytes(i32);
                }
                // else
                // {
                //     throw new Exception($"<IGNORE>: A fast integer cannot be negative: {fastText}");
                // 
                //     // Unsigned
                //     if (!uint.TryParse(fastText, out uint ui32))
                //         throw new InvalidFastConstantException(fastText);
                // 
                //     outputBytes = BitConverter.GetBytes(ui32);
                // }

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

            return emptyQ;

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
            string[] locals = context.fieldDeclaration().Select(fld => fld.Identifier().GetText()).ToArray();

            constants.SetMethodLocals(locals);

            int methodStartOffset = bytecodeBuffer.Count + 5;
            _ = constants.SetMethodSignature(methodName, (short)locals.Length, methodStartOffset);

            List<byte> blockBuffer = [];

            constants.IncreaseGlobalCounter(5);

            var lines = context.line();

            if (lines.Length is 0)
                ReportError(new Exception("Method bodies cannot be empty (must at least have RET)"), context);

            foreach (var line in lines)
            {
                byte[] bytes = Visit(line);
                blockBuffer.AddRange(bytes);
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
