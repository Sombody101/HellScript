namespace HellScriptCompiler;

internal static class Program
{
    private const string magicNumber = "\0\0hellscript\0\0";
    private const string outputPath = "../../../../test/test1";
    private const string inputPath = "../../../../test/test_source.txt";

    static void Main(string[] args)
    {
        if (args.Length is 0)
            args = ["../../../../test/test.hasm"];

        if (args.Length is 0)
        {
            Console.WriteLine("No hell assembly file path supplied.");
            Environment.Exit(1);
        }

        string hellAssembly = args[0];
        var loadedProgram = HellAsmLoader.LoadHellAsm(hellAssembly);

        if (File.Exists(outputPath))
        {
            Console.WriteLine("Clearing old binary");
            File.Delete(outputPath);
        }

        var outputStream = new BinaryWriter(File.OpenWrite(outputPath));

        HellAsmGenerator.StartProgramParse(loadedProgram, outputStream);
    }
}
