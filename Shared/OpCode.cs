namespace HellScriptShared.Bytecode;

/* 
 * This is a shared folder for both HellScriptRuntime and HellScriptCompiler.
 * Editing it in one project will change it for the other.
 */

// a*xZZ = inline bytecode argument ZZ bits wide

public enum Opcode : byte
{
    NOP = 0,

    /// <summary>
    /// (IHellType)(new HellString(StringConstants[(<see cref='int'/>)(a*x32)])) -> Stack
    /// </summary>
    LOAD_STR,

    /// <summary>
    /// (IHellType)(new BigInteger(IntegerConstants[(<see cref='int'/>)(a*x32)])) -> Stack
    /// </summary>
    LOAD_NUM,

    /// <summary>
    /// (IHellType)(new HellInteger((<see cref='int'/>)(a*x32)) -> Stack
    /// </summary>
    LOAD_FAST,

    /// <summary>
    /// (IHellType)(new HellNumber((<see cref='float'/>)(a*x32)) -> Stack
    /// </summary>
    LOAD_FAST_FLOAT,

    /// <summary>
    /// <see langword="null"/> -> Stack
    /// </summary>
    LOAD_NULL,

    /// <summary>
    /// Stack -> _
    /// </summary>
    POP,

    /// <summary>
    /// (IHellType)(new HellArray()) -> Stack
    /// </summary>
    CREATE_ARRAY,

    /// <summary>
    /// Stack[B][A] -> Stack
    /// </summary>
    LOAD_ELEMENT,

    /// <summary>
    /// Stack[A] -> Stack[C][B]
    /// </summary>
    STORE_ELEMENT,

    /// <summary>
    /// Stack[A].Clone() -> Stack
    /// </summary>
    DUP,

    /// <summary>
    /// 
    /// </summary>
    CALL_FUNC,

    HELLCALL,

    /// <summary>
    /// (void) -> Caller
    /// </summary>
    RETURN_VOID,

    /// <summary>
    /// Stack[(<see cref='int'/>)(a*x16)] -> Caller
    /// </summary>
    RETURN_VALUE,

    /// <summary>
    /// Stack[A]? -> Program.Exit(IHellType?);
    /// </summary>
    EXIT,

    /* Arithmetic */

    /// <summary>
    /// Stack[A] + Stack[B] -> Stack
    /// </summary>
    BINARY_ADD,

    /// <summary>
    /// Stack[A] - Stack[B] -> Stack
    /// </summary>
    BINARY_SUB,

    /// <summary>
    /// Stack[A] * Stack[B] -> Stack
    /// </summary>
    BINARY_MULT,

    /// <summary>
    /// Stack[A] / Stack[B] -> Stack
    /// </summary>
    BINARY_DIVIDE,

    /// <summary>
    /// Stack[A] % Stack[B] -> Stack
    /// </summary>
    BINARY_MODULO,

    /// <summary>
    /// Stack[A] -> -[Stack[S]] -> Stack
    /// </summary>
    BINARY_NEGATE,

    /* Jump operations */

    /// <summary>
    /// (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP,

    /// <summary>
    /// if (Stack[A] == NULL) then (<see cref='int'/>)(a*x32) (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP_NULL,

    /// <summary>
    /// if (Stack[A] == Stack[B]) then (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP_EQ,

    /// <summary>
    /// if (Stack[A] != Stack[B]) then (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP_NE,

    /// <summary>
    /// if (Stack[A] > Stack[B]) then (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP_GT,

    /// <summary>
    /// if (Stack[A].Type &lt; Stack[B]) then (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP_LT,

    /// <summary>
    /// if (Stack[A] >= Stack[B]) then (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP_GTEQ,

    /// <summary>
    /// if (Stack[A] &lt;= Stack[B]) then (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP_LTEQ,

    /// <summary>
    /// if (Stack[A].Type == Stack[B].Type) then (<see cref='int'/>)(a*x32) -> PC
    /// </summary>
    JMP_TE,

    /* Method operations */

    /// <summary>
    /// StackFrame(A) -> Stack
    /// </summary>
    LOAD_LOCAL,

    /// <summary>
    /// Stack -> StackFrame(A)
    /// </summary>
    STORE_LOCAL,

    /// <summary>
    /// StackFrame(0, A) -> Stack
    /// </summary>
    LOAD_GLOBAL_ARG,

    /// <summary>
    /// Stack -> StackFrame(0, A)
    /// </summary>
    STORE_GLOBAL_ARG,

    /* Structures */

    /// <summary>
    /// (IHellType)(new HellStruct(MetaStructures[(<see cref='int'/>)(a*x32)])) -> Stack
    /// </summary>
    NEW_INSTANCE,

    /// <summary>
    /// Stack[A].[(<see cref='int'/>)(a*x32)] -> Stack
    /// </summary>
    LOAD_FIELD,

    /// <summary>
    /// Stack[A] -> Stack[B].[(<see cref='int'/>)(a*x32)]
    /// </summary>
    STORE_FIELD,
}