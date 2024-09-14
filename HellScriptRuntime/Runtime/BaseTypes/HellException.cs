using HellScriptShared.Bytecode;
using System.Text;

namespace HellScriptRuntime.Runtime.BaseTypes;

internal class HellException : Exception, IHellType
{
    public HellException()
    { }

    public object Value => this;

    public TypeSignature TypeSignature => TypeSignature.Exception;

    public Type? Type => typeof(HellException);

    internal static string CreateStackTrace(Stack<StackFrame> callStack)
    {
        StringBuilder sb = new();

        foreach (var frame in callStack)
        {
            var function = frame.FunctionBeingCalled;

            string functionName;
            if (function is null)
            {
                functionName = "main";
            }
            else
            {
                functionName = $"{function.Name} [0x{function.BytecodePosition}]";
            }

            sb.AppendLine($"at {frame.FrameName} -> {functionName}");
        }

        return sb.ToString();
    }

    public IHellType Clone()
    {
        throw new NotImplementedException();
    }

    public bool Equals(IHellType? other)
    {
        throw new NotImplementedException();
    }
}
