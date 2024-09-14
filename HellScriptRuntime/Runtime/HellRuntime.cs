//#define PRINT_OPCODES

using HellScriptRuntime.Bytecode;
using HellScriptRuntime.Runtime.BaseTypes;
using HellScriptRuntime.Runtime.BaseTypes.Collection;
using HellScriptRuntime.Runtime.BaseTypes.Numbers;
using HellScriptRuntime.Runtime.BaseTypes.Text;
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

        var array = new HellArray(args.Length);

        for (int i = 0; i < args.Length; ++i)
            array.SetElement(i, new HellString(args[i]));

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

#if PRINT_OPCODES && DEBUG
            Console.WriteLine($"{bytecodeLoader.BytecodeIndex.ToString() + ':',-5} 0x{(byte)currentOpcode,-3} ({currentOpcode})");
#endif

            switch (currentOpcode)
            {
                case Opcode.NOP:
                    {
                        // No operation
                        
                        Console.WriteLine(stack.First()?.Value);
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
                        HellBigInteger hellNum = new(bigInt);

                        stack.Push(hellNum);
                    }
                    break;

                case Opcode.LOAD_FAST:
                    {
                        // Load the arg and place it on the stack
                        int num = bytecodeLoader.LoadIntArg();
                        HellInteger hellInteger = new(num);

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

                case Opcode.CREATE_ARRAY:
                    {
                        int arrayType = bytecodeLoader.LoadIntArg();

                        if (arrayType is -2)
                        {
                            // The wanted array is a dictionary (No actual length given)
                            HellDictionary array = new();
                            stack.Push(array);
                        }
                        else if (arrayType is -1)
                        {
                            // Use the first item on the stack as a length
                            HellInteger length = stack.Pop().As<HellInteger>($"An array length must be smaller than 0 and larger than {long.MaxValue}");

                            HellArray newArray = new(length);
                            stack.Push(newArray);
                        }
                        else
                        {
                            // Use the value as a fast value
                            HellArray newArray = new(arrayType);
                            stack.Push(newArray);
                        }
                    }
                    break;

                case Opcode.LOAD_ELEMENT:
                    {
                        var index = stack.Pop();
                        var array = stack.First();

                        if (array.TypeSignature == TypeSignature.Array)
                        {
                            var result = ((HellArray)array).GetElement(index.As<HellInteger>().ToInt64());
                            stack.Push(result);
                        }
                        else if (array.TypeSignature == TypeSignature.Dictionary)
                        {
                            var result = ((HellDictionary)array).GetValueWithKey(index);
                            stack.Push(result);
                        }
                        else
                            throw new InvalidOperationException($"Cannot index type {array.HellName()}");
                    }
                    break;

                case Opcode.STORE_ELEMENT:
                    {
                        var value = stack.Pop();
                        var index = stack.Pop();
                        var array = stack.First();

                        if (array.TypeSignature == TypeSignature.Array)
                        {
                            ((HellArray)array).SetElement(index.As<HellInteger>("Type %S cannot be used to index type IHellType[].").ToInt64(), value);
                        }
                        else if (array.TypeSignature == TypeSignature.Dictionary)
                        {
                            ((HellDictionary)array).SetValue(index, value);
                        }
                        else
                            throw new InvalidOperationException($"Cannot index type {array.HellName()}");
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
                    return stack.Pop();

                case Opcode.RETURN_VOID:
                    return Undefined.Null;

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
                        IHellNumber value2 = stack.Pop().As<IHellNumber>();
                        IHellNumber value1 = stack.Pop().As<IHellNumber>();

                        IHellNumber result = IHellNumber.Add(value1, value2);

                        stack.Push(result);
                    }
                    break;

                case Opcode.BINARY_SUB:
                    {
                        IHellNumber value2 = stack.Pop().As<IHellNumber>();
                        IHellNumber value1 = stack.Pop().As<IHellNumber>();

                        IHellNumber result = IHellNumber.Subtract(value1, value2);

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

                        if (items[1] == items[0])
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_NE:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (items[1] != items[0])
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_GT:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (IHellNumber.GreaterThan(items[1].As<IHellNumber>(), items[0].As<IHellNumber>()))
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_LT:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        // Do the reverse of JMP_GT
                        if (IHellNumber.LessThan(items[1].As<IHellNumber>(), items[0].As<IHellNumber>()))
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_GTEQ:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        IHellNumber item1 = items[1].As<IHellNumber>();
                        IHellNumber item2 = items[0].As<IHellNumber>();

                        if (item1 == item2 || IHellNumber.GreaterThan(item1, item2))
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_LTEQ:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        IHellNumber item1 = items[1].As<IHellNumber>();
                        IHellNumber item2 = items[0].As<IHellNumber>();

                        if (item1 == item2 || !IHellNumber.GreaterThan(item1, item2))
                            bytecodeLoader.JumpTo(position + address);
                    }
                    break;

                case Opcode.JMP_TE:
                    {
                        int address = bytecodeLoader.LoadIntArg();
                        IHellType?[] items = stack.Take(2).ToArray();

                        if (TypeHelper.TypesMatch(items[1].As<IHellNumber>(), items[0].As<IHellNumber>()))
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

                        for (short i = (short)(method.ParameterCount - 1); i > -1; --i)
                            methodFrame.AddSymbol(i, stack.Pop());

                        Frames.Push(methodFrame);

                        // Start the method
                        int returnAddress = bytecodeLoader.BytecodeIndex;
                        IHellType? returnedValue = ExecuteBytecode(method.BytecodePosition);

                        bytecodeLoader.JumpTo(returnAddress);
                        Frames.Pop();

                        if (returnedValue is not Undefined)
                            stack.Push(returnedValue);
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

                /* Structure operations */

                case Opcode.NEW_INSTANCE:
                    {
                        int structIndex = bytecodeLoader.LoadIntArg();
                        var structTemplate = bytecodeLoader.MetaStructures[structIndex];

                        HellStruct newStruct = new(structTemplate);
                        stack.Push(newStruct);
                    }
                    break;

                case Opcode.LOAD_FIELD:
                    {
                        int structOffset = bytecodeLoader.LoadIntArg();

                        // Let it error out
                        var hStruct = stack.First().As<HellStruct>();

                        stack.Push(hStruct.GetItem(structOffset));
                    }
                    break;

                case Opcode.STORE_FIELD:
                    {
                        int structOffset = bytecodeLoader.LoadIntArg();

                        IHellType? item = stack.Pop();

                        // Let it error out
                        var hStruct = stack.First().As<HellStruct>();

                        hStruct.SetItem(structOffset, item);
                    }
                    break;
            }
        }
    }
}
