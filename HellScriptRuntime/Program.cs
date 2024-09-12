using HellScriptRuntime.Bytecode;
using HellScriptRuntime.Runtime;
using HellScriptRuntime.Runtime.BaseTypes;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using StackFrame = HellScriptRuntime.Runtime.StackFrame;

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

        Stack<StackFrame>? frames = null;

        try
        {
            var byteLoader = new BytecodeLoader(input);

            var runtime = new HellRuntime(args, byteLoader);
            frames = runtime.Frames;

            // Star the bytecode at 0 (Entry)
            IHellType? returnedObject = runtime.ExecuteBytecode(0);

            Exit(returnedObject);
        }
        catch (Exception ex)
        {
            HandleUnhandledException(ex, frames);
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

    [DoesNotReturn]
    private static void HandleUnhandledException(Exception ex, Stack<StackFrame>? frames = null)
    {
        switch (ex)
        {
            case OutOfMemoryException om:
                Console.WriteLine($"Out of memory: {om.Message}");
                break;

            case StackOverflowException so:
                Console.WriteLine($"Stack overflow: {so.Message}");
                break;

            default:
                Console.WriteLine(ex.Message);
                break;
        }

        if (frames is not null)
            Console.WriteLine(HellException.CreateStackTrace(frames));

        Environment.Exit(-1);
    }
}
