using HellScriptShared.Exceptions;

namespace HellScriptShared.Bytecode;

internal static class BytecodeHelpers
{
    public const string MagicNumber = "\x9hellscript\x9";

    private const int BITS_32 = 4;
    private const int BITS_24 = 3;
    private const int BITS_16 = 2;
    private const int BITS_8 = 1;
    private const int BITS_NONE = 0;

    private static readonly Dictionary<string, string> opcodeNames =
        new()
        {
            { "nop", nameof(Opcode.NOP) },
            { "ldstr", nameof(Opcode.LOAD_STR) },
            { "ldnum", nameof(Opcode.LOAD_NUM) },
            { "ldfast", nameof(Opcode.LOAD_FAST) },
            { "ldfastf", nameof(Opcode.LOAD_FAST_FLOAT) },
            { "ldnull", nameof(Opcode.LOAD_NULL) },
            { "pop", nameof(Opcode.POP) },

            { "newarr", nameof(Opcode.CREATE_ARRAY) },
            { "ldelm", nameof(Opcode.LOAD_ELEMENT) },
            { "stelm", nameof(Opcode.STORE_ELEMENT) },

            { "stloc", nameof(Opcode.STORE_LOCAL) },
            { "ldloc", nameof(Opcode.LOAD_LOCAL) },

            { "add", nameof(Opcode.BINARY_ADD) },
            { "sub", nameof(Opcode.BINARY_SUB) },
            { "mult", nameof(Opcode.BINARY_MULT) },
            { "div", nameof(Opcode.BINARY_DIVIDE) },

            { "jmp", nameof(Opcode.JMP) },
            { "jeq", nameof(Opcode.JMP_EQ) },
            { "jlt", nameof(Opcode.JMP_LT) },
            { "jgt", nameof(Opcode.JMP_GT) },

            { "call", nameof(Opcode.CALL_FUNC) },
            { "hellcall", nameof(Opcode.HELLCALL) },

            { "ret", nameof(Opcode.RETURN_VOID) },
            { "retc", nameof(Opcode.RETURN_VALUE) },
            { "exit", nameof(Opcode.EXIT) },
        };

    /// <summary>
    /// Returns the number of bytes that should be loaded as <see cref="Opcode"/> arguments
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static int ArgumentCount(this Opcode code)
    {
        return code switch
        {
            // One byte
            Opcode.RETURN_VALUE => BITS_8,

            // Two bytes
            Opcode.LOAD_LOCAL
                or Opcode.STORE_LOCAL => BITS_16,

            // Three bytes

            #region Four bytes

            // Loaders
            Opcode.LOAD_STR
                or Opcode.LOAD_NUM
                or Opcode.LOAD_FAST
                or Opcode.LOAD_FAST_FLOAT

            // Branches (Not really needed; Can be hard coded)
                or (>= Opcode.JMP and <= Opcode.JMP_LTEQ)

            // Method calls
                or Opcode.CALL_FUNC
                or Opcode.HELLCALL
                    => BITS_32,

            #endregion Four bytes

            Opcode.DEFINE_METHOD => 8, // 8 bytes are required for info

            // No arguments
            _ => BITS_NONE,
        };
    }

    public static Opcode AsOpcode(this byte code)
    {
        if (!Enum.IsDefined(typeof(Opcode), code))
            throw new InvalidOpcodeException(code);

        return (Opcode)code;
    }

    public static bool AsOpcode(this byte code, out Opcode opcode)
    {
        opcode = (Opcode)code;
        
        if (!Enum.IsDefined(typeof(Opcode), code))
            return false;

        return true;
    }

    public static Opcode AsOpcode(this string code)
    {
        // Replace the code with the opcode if the short version was used
        if (opcodeNames.TryGetValue(code.ToLower(), out string? longCode))
            code = longCode;

        if (Enum.TryParse(code, true, out Opcode opcode))
            return opcode;

        throw new InvalidOpcodeException(code);
    }

    public static bool IsBranch(this Opcode code)
    {
        return code >= Opcode.JMP && code <= Opcode.JMP_LTEQ;
    }

    public static byte[] EmptyByte(int length)
    {
        byte[] array = new byte[length];
        for (int i = 0; i < length; ++i)
            array[i] = 0;

        return array;
    }
}
