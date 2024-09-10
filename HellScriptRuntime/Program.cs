using HellScriptRuntime.Bytecode;
using HellScriptRuntime.Runtime;
using HellScriptRuntime.Runtime.BaseTypes;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace HellScriptRuntime;

internal sealed class Program
{
    private const string inputFile = "../../../../test/test1";

    private static Stopwatch sw;

    static void Main(string[] args)
    {
        if (args.Length is 0)
            args = [inputFile];

        string input = args[0];

        sw = Stopwatch.StartNew();

        try
        {
            var byteLoader = new BytecodeLoader(input);

            var runtime = new HellRuntime(args, byteLoader);

            // Star the bytecode at 0 (Entry)
            IHellType returnedObject = runtime.ExecuteBytecode(0);

            Exit(returnedObject);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(-1);
        }
    }

    [DoesNotReturn]
    public static void Exit(IHellType? exitCode)
    {
        Console.WriteLine(sw.ElapsedMilliseconds);

        if (exitCode is null || !exitCode.IsNumber())
        {
            Environment.Exit(0);
        }

        Environment.Exit(exitCode.ToInt32(null));
    }
}
