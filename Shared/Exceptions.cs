
using HellScriptShared.Bytecode;

namespace HellScriptShared.Exceptions;

public class InvalidOpcodeException : Exception
{
    public InvalidOpcodeException(byte code)
        : base($"Invalid opcode 0x{code:x00}") 
    { }

    public InvalidOpcodeException(string code)
        : base($"Invalid opcode '{code}'") 
    { }

    public InvalidOpcodeException(byte code, int gloablIndex)
        : base($"Encountered invalid opcode 0x{code:x00} at program index 0x{gloablIndex:x00000000}")
    { }
}

public class NoArgumentSuppliedException : Exception
{
    public NoArgumentSuppliedException(byte code, int argCount)
        : base($"No argument supplied for opcode 0x{code:x00} when {argCount} were required")
    { }

    public NoArgumentSuppliedException(Opcode code)
        : base($"No argument supplied for opcode {code}")
    { }
}

public class InvalidArgumentSizeException : Exception
{
    public InvalidArgumentSizeException(Opcode code, int givenSize, int wantedSize)
        : base($"{givenSize} arguments supplied for opcode {code} when {wantedSize} were needed")
    { }
}

public class InvalidArgumentException<T> : Exception
{
    public InvalidArgumentException(string value)
        : base($"Invalid argument '{value}' when expecting one of type {typeof(T).Name}") 
    { }
}

public class InvalidFastConstantException : Exception
{
    public InvalidFastConstantException(string value)
        : base($"The value '{value}' cannot be used as a fast constant (Max 4 bytes)")
    { }
}