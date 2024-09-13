namespace HellScriptShared.Bytecode;

internal class HellFunction
{
    /// <summary>
    /// The UTF-16 string name associated with the function
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// The number of objects from the HellRuntime.Frames to use as arguments
    /// </summary>
    public readonly short ParameterCount;

    /// <summary>
    /// Optional, but used for type signature checking when passing parameters to a stack frame
    /// </summary>
    // public readonly short[] ParameterTypes;

    /// <summary>
    /// The bytecode index where the program counter should go when calling this function
    /// </summary>
    public readonly int BytecodePosition;

    public HellFunction(string name, short parameterCount, /*short[] parameterTypes,*/ int bytecodePosition)
    {
        Name = name;
        ParameterCount = parameterCount;
        // ParameterTypes = parameterTypes;
        BytecodePosition = bytecodePosition;
    }

    public void SerializeToStream(BinaryWriter bw)
    {
        // Location
        bw.Write(BytecodePosition);

        // Name
        bw.Write(Name);

        // Arg count
        bw.Write(ParameterCount);

        // Types
        // bw.Write((byte)ParameterTypes.Length);
        // foreach (var type in ParameterTypes)
        //     bw.Write(type);
    }

    public static HellFunction[] GetFunctionData(BinaryReader stream)
    {
        int tableLen = stream.ReadInt32();
        HellFunction[] functions = new HellFunction[tableLen];

        for (int i = 0; i < tableLen; ++i)
        {
            // Location
            int functionLocation = stream.ReadInt32();

            // Name
            string name = stream.ReadString();

            // Arg count
            short argCount = stream.ReadInt16();

            // Types
            // byte typeCount = stream.ReadByte();
            // short[] paramTypes = new short[typeCount];
            // for (int x = 0; x < typeCount; ++x)
            //     paramTypes[x] = stream.ReadInt16();

            functions[i] = new HellFunction(name, argCount, /*paramTypes,*/ functionLocation);
        }

        return functions;
    }

    public override string ToString()
    {
        return $"[ {Name}, {ParameterCount}, {BytecodePosition} ]";
    }
}
