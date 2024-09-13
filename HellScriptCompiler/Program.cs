using HellScriptShared.Bytecode;
using System.Text;

namespace HellScriptCompiler;

internal static class Program
{
    // private const string magicNumber = "\0\0hellscript\0\0";
    // private const string outputPath = "../../../../test/test1";
    // private const string inputPath = "../../../../test/test_source.txt";

    static void Main(string[] args)
    {
#if DEBUG
        args = ["../../../../test/test.hasm", "../../../../test/test1"];
#else
        if (args.Length is not 2)
        {
            Console.WriteLine("2 argument required: hellc <input file> <output file>");
        }
#endif

        string input = args[0];
        string output = args[1];

        if (args.Length is 0)
        {
            Console.WriteLine("No hell assembly file path supplied.");
            Environment.Exit(1);
        }

        if (HellAsmLoader.LoadHellAsm(input, out var loadedProgram))
        {
            Environment.Exit(-1);
        }

        // Compile the hasm file to a List<byte>
        HellAsmGenerator visitor = HellAsmGenerator.StartProgramParse(loadedProgram);

        Console.WriteLine($"Assembly errors: {visitor.parseErrors.Count}");
        HellAsmGenerator.ParseError.PrintErrors(visitor.parseErrors);

        if (visitor.parseErrors.Count > 0)
            return;

        Console.WriteLine($"Final bytecode length: {visitor.bytecodeBuffer.Count}");

        var constants = visitor.constants;

        Console.WriteLine($"\nStrings ({constants.constantStrings.Count}):");

        int count = 0;
        foreach (var str in constants.constantStrings)
        {
            Console.WriteLine($"{count++}: {str}");
        }

        Console.WriteLine($"\nNumbers ({visitor.constants.constantNumbers.Count}):");

        count = 0;
        foreach (var num in visitor.constants.constantNumbers)
        {
            Console.WriteLine($"{count++}: {num}");
        }

        Console.WriteLine($"\nMethods ({visitor.constants.methodSignatures.Count}):");

        count = 0;
        foreach (var method in visitor.constants.methodSignatures)
        {
            Console.WriteLine($"{count++}: {method}");
        }


        Console.WriteLine($"\nStructures ({visitor.constants.structMetadata.Count}):");

        count = 0;
        foreach (var structure in visitor.constants.structMetadata)
        {
            Console.WriteLine($"{count++}: {structure}");
        }

        List<byte> bytecode = visitor.bytecodeBuffer;
        int codeLen = bytecode.Count;
        Console.WriteLine($"\nDecompile ({codeLen}):");
        for (int i = 0; i < codeLen; ++i)
        {
            byte b = bytecode[i];

            try
            {
                Opcode op = b.AsOpcode();

                int argCount = BytecodeHelpers.ArgumentCount(op);

                int arg = -1;
                if (argCount > 0)
                {
                    var rng = bytecode.GetRange(i + 1, argCount).ToArray();
                    arg = argCount is 4
                        ? BitConverter.ToInt32(rng)
                        : BitConverter.ToInt16(rng);
                }

                Console.Write($"{i + (argCount is not 0
                    ? $"-{i + argCount}"
                    : string.Empty),-5} {op,-15} ");

                if (argCount is not 0)
                {
                    Console.Write(arg);
                    i += argCount;
                }

                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invalid opcode: {e.Message}\n{b}");
                break;
            }
        }

        if (File.Exists(output))
        {
            Console.WriteLine("\nClearing old binary");
            File.Delete(output);
        }

        var binWriter = new BinaryWriter(File.OpenWrite(output), Encoding.UTF8);

        Console.WriteLine("\nWriting binary to file...");

        /* Write it out to the file */

        // Magic number
        binWriter.Write(Encoding.ASCII.GetBytes(BytecodeHelpers.MagicNumber));

        // Push the constants, structure table, and methods table to the file
        visitor.constants.DumpConstantValues(binWriter);

        // Write the bytecode
        binWriter.Write([.. visitor.bytecodeBuffer]);

        Console.WriteLine($"Wrote {binWriter.BaseStream.Length} bytes");
    }
}
