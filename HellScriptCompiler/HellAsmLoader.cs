using Antlr4.Runtime;

namespace HellScriptCompiler;

internal static class HellAsmLoader
{
    public static HellAsm_Parser LoadHellAsm(string hellAsmPath)
    {
        if (Directory.Exists(hellAsmPath))
        {
            Console.WriteLine("The given path leads to a directory.");
            Environment.Exit(1);

            // At some point, add support for ".hsproj" files (like .csproj for HellScript)
        }

        if (!File.Exists(hellAsmPath))
        {
            Console.WriteLine($"Failed to locate the given file '{hellAsmPath}'");
            Environment.Exit(1);
        }

        HellAsm_Lexer lexer = new(new AntlrInputStream(new StreamReader(hellAsmPath)));
        HellAsm_Parser parser = new(new CommonTokenStream(lexer));

        var listener_lexer = new ErrorListener<int>();
        var listener_parser = new ErrorListener<IToken>();

        lexer.RemoveErrorListeners();
        parser.RemoveErrorListeners();
        lexer.AddErrorListener(listener_lexer);
        parser.AddErrorListener(listener_parser);

        if (listener_lexer.had_error || listener_parser.had_error)
            Console.WriteLine("error in parse.");
        else
            Console.WriteLine("parse completed.");

        return parser;
    }
}
