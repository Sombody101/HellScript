namespace HellScriptRuntime.Runtime.BaseTypes;

internal class HellFunction
{
    public readonly string FunctionName;

    /// <summary>
    /// The number of objects from the <see cref="HellRuntime.Frames"/> to use as arguments
    /// </summary>
    public readonly uint ParameterCount;

    /// <summary>
    /// The bytecode index where the program counter should go when calling this function
    /// </summary>
    public readonly uint BytecodePosition;

    public HellFunction(string name, uint parameterCount, uint bytecodePosition)
    {
        FunctionName = name;
        ParameterCount = parameterCount;
        BytecodePosition = bytecodePosition;
    }
}
