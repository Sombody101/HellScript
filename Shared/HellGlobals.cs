namespace HellScriptShared.Bytecode;

public static class HellGlobals
{
    private static readonly int globalsCount = GlobalVariables.Count;
    private static readonly int typeCount = InternalTypes.Count;

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

    public static int GetVariable(string variableName)
    {
        for (int i = 0; i < globalsCount; ++i)
            if (variableName == GlobalVariables[i])
                return i;

        return -1;
    }

    public static int TypeFor(string typeName)
    {
        for (int i = 0; i < typeCount; ++i)
            if (typeName == InternalTypes[i])
                return i;

        return -1;
    }
}

enum TypeSignature
{
    Undefined,
    Null,
    String,
    Int,
    Number,
    Array,
    Exception,

    // Anything higher is a bytecode defined structure
}