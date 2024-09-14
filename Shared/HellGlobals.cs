namespace HellScriptShared.Bytecode;

public static class HellGlobals
{
    public static readonly List<string> GlobalVariables = [
        "args"
    ];

    public static readonly List<string> InternalTypes = [
        "null",
        "string",
        "int",
        "number",
        "array"
    ];

    /// <summary>
    /// Gets the index for the wanted global variable
    /// </summary>
    /// <param name="variableName"></param>
    /// <returns></returns>
    public static int GetVariable(string variableName)
    {
        int globalsCount = GlobalVariables.Count;
        for (int i = 0; i < globalsCount; ++i)
        {
            if (variableName == GlobalVariables[i])
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Gets the index for the wanted internal type
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static int TypeFor(string typeName)
    {
        int typeCount = InternalTypes.Count;
        for (int i = 0; i < typeCount; ++i)
        {
            if (typeName == InternalTypes[i])
                return i;
        }

        return -1;
    }
}

/// <summary>
/// The same as <see cref="TypeCode"/>, but used for Hell (and will eventually allow for bytecode defined
/// structures to be recognized as their own types)
/// </summary>
enum TypeSignature
{
    Undefined,
    Null,
    String,
    Int,
    BigInt,
    Number,
    BigNumber,
    Array,
    Dictionary,
    Exception,

    MAX = 255
    // Anything higher is a bytecode defined structure
}