// Anything under 128k is completely loaded
//#define BUFFER_128k
//#define BUFFER_256k
//#define BUFFER_512k

//#define BUFFER_1m
//#define BUFFER_5m
#define BUFFER_10m
// #define BUFFER_20m
// #define BUFFER_30m

using HellScriptRuntime.Exceptions;
using HellScriptShared.Bytecode;
using HellScriptShared.Exceptions;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;

namespace HellScriptRuntime.Bytecode;

internal class BytecodeLoader : IDisposable
{
    private const int bufferSize = 1024 *
#if BUFFER_128k
    128;
#elif BUFFER_256k
    256;
#elif BUFFER_512k
    512;
#elif BUFFER_1m
    1024;
#elif BUFFER_5m
    1024 * 5;
#elif BUFFER_10m
    1024 * 10;
#elif BUFFER_20m
    1024 * 20;
#elif BUFFER_30m
    1024 * 30;
#endif

    private BinaryReader? binaryReader;
    private byte[] bytecode;
    private int currentBytecodeIndex = 0;

    public int BytecodeIndex => currentBytecodeIndex;

    // Preloaded constants
    private readonly string[] StringConstants;
    private readonly BigInteger[] IntegerConstants;
    // private readonly double[] FloatingConstants;

    public BytecodeLoader(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Failed to find the script file '{filePath}'");
        }

        binaryReader = new(File.OpenRead(filePath));

        string loadedMagic = Encoding.Default.GetString(binaryReader.ReadBytes(BytecodeHelpers.MagicNumber.Length));
        if (loadedMagic != BytecodeHelpers.MagicNumber)
        {
            // This isn't a hellscript binary file
            throw new InvalidScriptTypeException(filePath);
        }

        /* Load string constants */

        int LoadInt()
        {
            byte[] data = binaryReader.ReadBytes(sizeof(int));
            return BitConverter.ToInt32(data);
        }

        // Get the number of strings to load
        int length = LoadInt();
        StringConstants = new string[length];

        for (int i = 0; i < length; ++i)
        {
            int stringLength = LoadInt();
            byte[] stringBytes = binaryReader.ReadBytes(stringLength * 2);

            StringConstants[i] = Encoding.Unicode.GetString(stringBytes);

        }

        /* Load numerical constants */
        length = LoadInt();
        IntegerConstants = new BigInteger[length];
        
        for (int i = 0; i < length; ++i) 
        {
            int bigLength = LoadInt();

            byte[] bytes = binaryReader.ReadBytes(bigLength);
            IntegerConstants[i] = new BigInteger(bytes);
        }

        bytecode = binaryReader.ReadBytes(bufferSize);

        if (binaryReader.BaseStream.Length < bufferSize)
        {
            // Close the stream as we loaded the entire file
            binaryReader.Dispose();
        }
    }

    public string GetString(int constantIndex)
    {
        if (constantIndex > StringConstants.Length
            || constantIndex < 0)
            throw new ArgumentOutOfRangeException($"No string constant at {constantIndex} in constants of {StringConstants.Length} strings");

        return StringConstants[constantIndex];
    }

    public BigInteger GetBigInt(int constantIndex)
    {
        if (constantIndex > IntegerConstants.Length
            || constantIndex < 0)
            throw new ArgumentOutOfRangeException($"No number constant at {constantIndex} in constants of {IntegerConstants.Length} integers");

        return IntegerConstants[constantIndex];
    }

    /// <summary>
    /// Gets the current <see cref="bytecode"/> byte without advancing the program counter
    /// </summary>
    public byte GetByte()
    {
        return bytecode[currentBytecodeIndex];
    }

    /// <summary>
    /// Reads the current <see cref="bytecode"/> byte and advances the program counter
    /// </summary>
    public byte ReadByte()
    {
        return bytecode[currentBytecodeIndex++];
    }

    /// <summary>
    /// Gets the current <see cref="bytecode"/> byte as an <see cref="Opcode"/> without advancing the program counter
    /// </summary>
    public Opcode GetOpcode()
    {
        return GetByte().AsOpcode();
    }

    /// <summary>
    /// Gets the current <see cref="bytecode"/> byte as an <see cref="Opcode"/> and advances the program counter
    /// </summary>
    public Opcode ReadOpcode()
    {
        if (!ReadByte().AsOpcode(out Opcode code))
            throw new InvalidOpcodeException((byte)code, BytecodeIndex);

        return code;
    }

    /// <summary>
    /// Sets the program counter to the specified <paramref name="position"/>
    /// </summary>
    /// <param name="position"></param>
    public void JumpTo(int position)
    {
        currentBytecodeIndex = position;
    }

    public int LoadIntArg()
    {
        var addressBytes = bytecode.AsSpan()[(currentBytecodeIndex)..(currentBytecodeIndex + 4)];
        int arg = BitConverter.ToInt32(addressBytes);
        currentBytecodeIndex += 4;
        return arg;
    }

    public short LoadShortArg()
    {
        short arg = BitConverter.ToInt16(bytecode.AsSpan()[currentBytecodeIndex..(currentBytecodeIndex + 2)]);
        currentBytecodeIndex += 2;
        return arg;
    }

    public void Dispose()
    {
        binaryReader?.Dispose();
    }
}
