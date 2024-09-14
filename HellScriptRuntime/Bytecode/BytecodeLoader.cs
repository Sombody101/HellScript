#define BUFFER_ALL // Overrides any other option

// At some point I plan on making this load sections of the file (to reduce memory usage)
// For now, use BUFFER_ALL (otherwise, it will load selected size and never load more of the file)
// #define BUFFER_128k
// #define BUFFER_256k
// #define BUFFER_512k

// #define BUFFER_1m
// #define BUFFER_5m
// #define BUFFER_10m
// #define BUFFER_20m
// #define BUFFER_30m

using HellScriptRuntime.Exceptions;
using HellScriptShared.Bytecode;
using HellScriptShared.Exceptions;
using System.Numerics;
using System.Text;

namespace HellScriptRuntime.Bytecode;

internal sealed class BytecodeLoader : IDisposable
{
    private const int bufferSize =
#if BUFFER_ALL
        -1;
#else
        1024 *
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

#endif

    private readonly BinaryReader? binaryReader;
    private readonly byte[] bytecode;

    /// <summary>
    /// Keeps track of where we are in the program (like the cursor of a text editor)
    /// </summary>
    private int currentBytecodeIndex = 0;

    public int BytecodeIndex => currentBytecodeIndex;

    // Preloaded constants
    private readonly string[] StringConstants;
    private readonly BigInteger[] IntegerConstants;
    // private readonly double[] FloatingConstants;

    public readonly HellStructMetadata[] MetaStructures;
    public readonly HellFunction[] DefinedFunctions;

    public BytecodeLoader(string filePath)
    {
        /*
         * Binary files are loaded in this order:
         *      Strings (UTF-8)
         *      BigIntegers
         *      BigDecimals
         *      Methods table (the method bodies are still in the bytecode, this just points to them and gives the param count)
         *      Structure table (structures are just a template for a readonly array)
         */

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Failed to find the script file '{filePath}'");
        }

        binaryReader = new(File.OpenRead(filePath), Encoding.UTF8);

        // The "magic number" is not UTF encoded (starts and ends with binary, granted it's just a tab character)
        string loadedMagic = Encoding.ASCII.GetString(binaryReader.ReadBytes(BytecodeHelpers.MagicNumber.Length));
        if (loadedMagic != BytecodeHelpers.MagicNumber)
        {
            // This isn't a hellscript binary file
            throw new InvalidScriptTypeException(filePath);
        }

        /* Load string constants */

        // Get the number of strings to load
        int tableLen = binaryReader.ReadInt32();
        StringConstants = new string[tableLen];

        for (int i = 0; i < tableLen; ++i)
        {
            StringConstants[i] = binaryReader.ReadString();
        }

        /* Load numerical constants */
        tableLen = binaryReader.ReadInt32();
        IntegerConstants = new BigInteger[tableLen];

        for (int i = 0; i < tableLen; ++i)
        {
            int bigLength = binaryReader.ReadInt32();

            byte[] bytes = binaryReader.ReadBytes(bigLength);
            IntegerConstants[i] = new BigInteger(bytes);
        }

        /* Load functions table */
        // Use the shared deserialization method
        DefinedFunctions = HellFunction.GetFunctionData(binaryReader);

        /* Load structure templates */
        MetaStructures = HellStructMetadata.ReadAllFromStream(binaryReader);

        // Load the rest of the bytecode (the actual instructions and methods)
        bytecode =
#if BUFFER_ALL
            binaryReader.ReadBytes((int)binaryReader.BaseStream.Length - (int)binaryReader.BaseStream.Position);
#else
            // At some point I plan on making this load sections of the file (to reduce memory usage)
            binaryReader.ReadBytes(bufferSize);
#endif

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

    /// <summary>
    /// Loads 4 bytes as an int (for opcode arguments)
    /// </summary>
    /// <returns></returns>
    public int LoadIntArg()
    {
        var addressBytes = bytecode.AsSpan()[currentBytecodeIndex..(currentBytecodeIndex + 4)];
        int arg = BitConverter.ToInt32(addressBytes);
        currentBytecodeIndex += 4;
        return arg;
    }

    /// <summary>
    /// Loads 2 bytes as a short (for opcode arguments)
    /// </summary>
    /// <returns></returns>
    public short LoadShortArg()
    {
        short arg = BitConverter.ToInt16(bytecode.AsSpan()[currentBytecodeIndex..(currentBytecodeIndex + 2)]);
        currentBytecodeIndex += 2;
        return arg;
    }

    public void Dispose()
    {
        binaryReader?.Dispose();
        GC.SuppressFinalize(this);
    }
}
