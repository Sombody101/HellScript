namespace HellScriptRuntime.Runtime.BaseTypes;

internal class HellFunction
{
    /// <summary>
    /// The UTF-16 string name associated with the function
    /// </summary>
    public readonly string FunctionName;

    /// <summary>
    /// The number of objects from the <see cref="HellRuntime.Frames"/> to use as arguments
    /// </summary>
    public readonly int ParameterCount;

    /// <summary>
    /// The bytecode index where the program counter should go when calling this function
    /// </summary>
    public readonly int BytecodePosition;

    public HellFunction(string name, int parameterCount, int bytecodePosition)
    {
        FunctionName = name;
        ParameterCount = parameterCount;
        BytecodePosition = bytecodePosition;
    }
}
