using System.Text;

namespace HellScriptRuntime.Runtime.BaseTypes;

public class HellException : Exception, IHellType
{
    public HellException()
    { }

    public object Value => this;

    public int TypeSignature => 0x08;

    public Type? Type => typeof(HellException);

    internal static string CreateStackTrace(Stack<StackFrame> callStack)
    {
        StringBuilder sb = new();

        foreach (var frame in callStack)
        {
            var function = frame.FunctionBeingCalled;

            string functionName;
            if (function is null)
            {
                functionName = "main";
            }
            else
            {
                functionName = $"{function.FunctionName} [0x{function.BytecodePosition}]";
            }

            sb.AppendLine($"at {frame.FrameName} -> {functionName}");
        }

        return sb.ToString();
    }

    public TypeCode GetTypeCode()
    {
        throw new NotImplementedException();
    }

    public bool ToBoolean(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public byte ToByte(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public char ToChar(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public decimal ToDecimal(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public double ToDouble(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public short ToInt16(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public int ToInt32(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public long ToInt64(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public float ToSingle(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public string ToString(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public ulong ToUInt64(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    IHellType IHellType.Clone()
    {
        throw new NotImplementedException();
    }

    int IComparable<HellInteger>.CompareTo(HellInteger? other)
    {
        throw new NotImplementedException();
    }

    bool IEquatable<HellInteger>.Equals(HellInteger? other)
    {
        throw new NotImplementedException();
    }
}
