#define PRINT_OPCODES

using HellScriptRuntime.Bytecode;
using HellScriptRuntime.Runtime.BaseTypes;
using HellScriptRuntime.Runtime.BaseTypes.Operators;
using HellScriptShared.Bytecode;
using System.Numerics;

namespace HellScriptRuntime.Runtime;

internal class HellRuntime
{
    public static void StartNewRuntime(string[] args, BytecodeLoader loader)
    {
        var runtime = new HellRuntime(args, loader);
    }

    private readonly BytecodeLoader bytecodeLoader;

    public HellRuntime(string[] args, BytecodeLoader _bytecodeLoader)
    {
        bytecodeLoader = _bytecodeLoader;

        Frames = new();

        var globalFrame = new StackFrame("<global>", null);

        var array = new HellArray();

        for (int i = 0; i < args.Length; ++i)
            array.SetValue(new HellInteger(i), new HellString(args[i]));

        globalFrame.AddSymbol(0, array);

        Frames.Push(globalFrame);
    }

    public Stack<StackFrame> Frames;

    public StackFrame GlobalFrame => Frames.Last();
    public StackFrame CurrentFrame => Frames.First();

    public IHellType? ExecuteBytecode(int position)
    {
        bytecodeLoader.JumpTo(position);

        StackFrame frame = CurrentFrame;
        Stack<IHellType?> stack = frame.DataStack;

        while (true)
        {
            Opcode currentOpcode = bytecodeLoader.ReadOpcode();

#if PRINT_OPCODES
            Console.WriteLine($"{bytecodeLoader.BytecodeIndex.ToString() + ':',-5} 0x{(byte)currentOpcode,-3} ({currentOpcode})");
#endif

            switch (currentOpcode)
            {
                case Opcode.NOP:
                    {
                        // No operation
                        Console.WriteLine(CurrentFrame.DataStack.First().Value);
                    }
                    continue;

                case Opcode.LOAD_STR:
                    {
                        // Load the string table address
                        int address = bytecodeLoader.LoadIntArg();
                        string constantString = bytecodeLoader.GetString(address);
                        HellString hellString = new(constantString);

                        stack.Push(hellString);
                    }
                    break;

                case Opcode.LOAD_NUM:
                    {
                        // Load the double table address
                        int address = bytecodeLoader.LoadIntArg();
                        BigInteger bigInt = bytecodeLoader.GetBigInt(address);
                        HellInteger hellNum = new(bigInt);

                        stack.Push(hellNum);
                    }
                    break;

                case Opcode.LOAD_FAST:
                    {
                        // Load the arg and place it on the stack
                        int num = bytecodeLoader.LoadIntArg();
                        BigInteger bigFast = new(num);
                        HellInteger hellInteger = new(bigFast);

                        stack.Push(hellInteger);
                    }
                    break;

                case Opcode.LOAD_NULL:
                    {
                        stack.Push(null!);
                    }
                    break;

                case Opcode.POP:
                    {
                        _ = stack.Pop();
                    }
                    break;

                case Opcode.LOAD_ELEMENT:
                    {
                        var index = stack.Pop();
                        var array = stack.First();

                        if (array is not HellArray arr)
                        {
                            throw new InvalidOperationException($"Cannot index type {array.HellName()}");
                        }

                        var result = arr.GetValueWithKey(index);

                        stack.Push(result);
                    }
                    break;

                case Opcode.CREATE_ARRAY:
                    {
                        HellArray array = new();
                        stack.Push(array);
                    }
                    break;

                case Opcode.STORE_ELEMENT:
                    {
                        var value = stack.Pop();
                        var index = stack.Pop();
                        var array = stack.First();

                        if (array is not HellArray arr)
                        {
                            throw new InvalidOperationException($"Cannot index type {array.HellName()} (It is not an array)");
                        }

                        arr.SetValue(index, value);
                    }
                    break;

                case Opcode.DUP:
                    {
                        IHellType? lastValue = stack.First();

                        if (lastValue is not null)
                            lastValue = lastValue.Clone();

                        stack.Push(lastValue);
                    }
                    break;

                // Return X value(s) from the stack
                case Opcode.RETURN_VALUE:
                    {
                        int valueCount = bytecodeLoader.ReadByte();

                        if (valueCount > 1)
                        {
                            List<IHellType> buffer = [];

                            for (int l = stack.Count - 1; l > valueCount; --l)
                                buffer.Add(stack.Pop());

                            return default;//  buffer.ToArray();
                        }
                        else
                            return stack.Pop();
                    }

                case Opcode.RETURN_VOID:
                    return null;

                // Exits the program, using the 
                case Opcode.EXIT:
                    {
                        IHellType? lastStack = stack.Count > 0
                            ? stack.Pop()
                            : null;

                        Program.Exit(lastStack);
                    }
                    break;

                /* Arithmetic */

                case Opcode.BINARY_ADD:
                    {
                        IHellType? value1 = stack.Pop();
                        IHellType? value2 = stack.Pop();

                        IHellType result = Mathable.Add(value1, value2);

                        stack.Push(result);
                    }
                    break;

                case Opcode.BINARY_SUB:
                    {
                        IHellType? value1 = stack.Pop();
                        IHellType? value2 = stack.Pop();

                        IHellType result = Mathable.Subtract(value1, value2);

                        stack.Push(result);
                    }
                    break;

                /* Branching */

                case Opcode.JMP:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_NULL:
                    {
                        int address = bytecodeLoader.LoadIntArg();

                        if (stack.First() is null)
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_EQ:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (items[0] == items[1])
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_NE:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (items[0] != items[1])
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_GT:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (Mathable.GreaterThan(items[0], items[1]))
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_LT:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (Mathable.GreaterThan(items[1], items[0]))
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_GTEQ:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (items[0] == items[1] || Mathable.GreaterThan(items[0], items[1]))
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_LTEQ:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (items[0] == items[1] || !Mathable.GreaterThan(items[0], items[1]))
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                /* Method operations */

                case Opcode.CALL_FUNC:
                    {
                        // Setup the new call stack frame
                        int methodIndex = bytecodeLoader.LoadIntArg();
                        HellFunction method = bytecodeLoader.DefinedFunctions[methodIndex];

                        StackFrame methodFrame = new(functionBeingCalled: method);
                        Frames.Push(methodFrame);

                        // Start the method
                        int returnAddress = bytecodeLoader.BytecodeIndex;
                        ExecuteBytecode(method.BytecodePosition);
                        bytecodeLoader.JumpTo(returnAddress);

                        Frames.Pop();
                    }
                    break;

                case Opcode.LOAD_LOCAL:
                    {
                        short address = bytecodeLoader.LoadShortArg();
                        frame.GetSymbol(address, out IHellType? value);

                        stack.Push(value);
                    }
                    break;

                case Opcode.STORE_LOCAL:
                    {
                        short address = bytecodeLoader.LoadShortArg();
                        frame.SetOrAddSymbol(address, stack.Pop());
                    }
                    break;

                case Opcode.LOAD_GLOBAL_ARG:
                    break;

                case Opcode.STORE_GLOBAL_ARG:
                    break;
            }
        }
    }
}
