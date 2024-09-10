using HellScriptRuntime.Runtime.BaseTypes;
using System.Runtime.InteropServices;

namespace HellScriptRuntime.Runtime;

internal class StackFrame
{
    private readonly Dictionary<int, IHellType?> symbols;

    public readonly string FrameName;

    /// <summary>
    /// A stack for all data used in the frame
    /// </summary>
    public readonly Stack<IHellType?> DataStack;

    public HellFunction? FunctionBeingCalled
    {
        get;
        init;
    }

    public StackFrame([Optional] string frameName, [Optional] HellFunction? functionBeingCalled)
    {
        symbols = [];
        DataStack = [];

        FunctionBeingCalled = functionBeingCalled;

        FrameName = frameName ?? "scoped-frame::".CreateFrameId();
    }

    /// <summary>
    /// Create a new <see cref="IHellType"/> in this current context
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public bool AddSymbol(int id, IHellType? value)
    {
        // Return false if the variable already exists
        if (symbols.ContainsKey(id))
        {
            return false;
        }

        // Add the variable
        symbols.Add(id, value);
        return true;
    }

    /// <summary>
    /// Set a pre-existing <see cref="IHellType"/> with a new value
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <exception cref="HUndefinedVariableAssignmentException"></exception>
    public bool SetSymbol(int id, IHellType? value)
    {
        // Return false if the variable doesn't already exist
        if (!symbols.ContainsKey(id))
        {
            return false;
        }

        // Set the variable
        symbols[id] = value;
        return true;
    }

    public void SetOrAddSymbol(int id, IHellType? value)
    {
        symbols[id] = value;
    }

    /// <summary>
    /// Fetch a pre-existing <see cref="IHellType"/> from the current context
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool GetSymbol(int id, out IHellType? outValue)
    {
        if (symbols.TryGetValue(id, out IHellType? value))
        {
            outValue = value;
            return true;
        }

        // Failed to find the symbol
        outValue = null;
        return false;
    }
}
