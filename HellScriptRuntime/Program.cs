using HellScriptRuntime.Bytecode;
using HellScriptRuntime.Runtime;
using HellScriptRuntime.Runtime.BaseTypes;
using HellScriptRuntime.Runtime.BaseTypes.Numbers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using StackFrame = HellScriptRuntime.Runtime.StackFrame;

namespace HellScriptRuntime;

internal static class Program
{
    private const string inputFile = "../../../../test/test1";

    private static Stopwatch? sw;

    static void Main(string[] args)
    {
        if (args.Length is 0)
            args = [inputFile];

        string input = args[0];

        sw = Stopwatch.StartNew();

        Stack<StackFrame>? frames = null;

        try
        {
            // Load the bytecode
            var byteLoader = new BytecodeLoader(input);

            var runtime = new HellRuntime(args, byteLoader);
            frames = runtime.Frames;

            // Start the bytecode at 0 (Entry)
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

        int code = exitCode.As<IHellNumber>().ToInt32();

        Environment.Exit(code);
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

            case InvalidCastException ce:
                Console.WriteLine($"Invalid values going from stack to operation: {ce.Message}");
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
