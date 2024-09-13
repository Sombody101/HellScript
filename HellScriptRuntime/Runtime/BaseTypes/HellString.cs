using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes;

// Size 8
internal class HellString : IHellType
{
    public HellString(string? value)
    {
        StringValue = value;
    }

    public object Value => StringValue;

    public string? StringValue;

    public TypeSignature TypeSignature => TypeSignature.String;

    public Type Type => typeof(string);

    public IHellType Clone()
    {
        return new HellString(StringValue);
    }

    // Interfaces
    #region Interface Implementations

    public override bool Equals(object? obj)
    {
        return obj is HellString str && Equals(str);
    }

    /* IComparable */

    public bool Equals(HellString other)
    {
        return string.Equals(StringValue, other.StringValue);
    }

    /* IConvertible */

    public TypeCode GetTypeCode()
    {
        return TypeCode.String;
    }

    public bool ToBoolean(IFormatProvider? provider)
    {
        return StringValue is not null;
    }

    public byte ToByte(IFormatProvider? provider)
    {
        if (byte.TryParse(StringValue, out byte b))
            return b;

        return default;
    }

    public char ToChar(IFormatProvider? provider)
    {
        if (StringValue is null || StringValue.Length is 0)
            return default;

        // Return the first character
        return StringValue[0];
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        return default;
    }

    public decimal ToDecimal(IFormatProvider? provider)
    {
        if (decimal.TryParse(StringValue, out decimal dcml))
            return dcml;

        return default;
    }

    public double ToDouble(IFormatProvider? provider)
    {
        if (double.TryParse(StringValue, out double dbl))
            return dbl;

        return default;
    }

    public short ToInt16(IFormatProvider? provider)
    {
        if (short.TryParse(StringValue, out short i16))
            return i16;

        return default;
    }

    public int ToInt32(IFormatProvider? provider)
    {
        if (int.TryParse(StringValue, out int i32))
            return i32;

        return default;
    }

    public long ToInt64(IFormatProvider? provider)
    {
        if (long.TryParse(StringValue, out long i64))
            return i64;

        return default;
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        if (sbyte.TryParse(StringValue, out sbyte sb))
            return sb;

        return default;
    }

    public float ToSingle(IFormatProvider? provider)
    {
        if (float.TryParse(StringValue, out float flt))
            return flt;

        return default;
    }

    public string ToString(IFormatProvider? provider)
    {
        return StringValue!;
    }

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        var converted = Convert.ChangeType(StringValue, conversionType);

        if (converted is null)
            return new object();

        return converted;
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        if (ushort.TryParse(StringValue, out ushort i16))
            return i16;

        return default;
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        if (uint.TryParse(StringValue, out uint i32))
            return i32;

        return default;
    }

    public ulong ToUInt64(IFormatProvider? provider)
    {
        if (ulong.TryParse(StringValue, out ulong i64))
            return i64;

        return default;
    }

    public int CompareTo(IHellType? other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IHellType? other)
    {
        throw new NotImplementedException();
    }

    #endregion Interface Implementations

    // Operators
    #region Operators

    public static bool operator ==(HellString hstrA, HellString hstrB)
    {
        return string.Equals(hstrA.StringValue, hstrB.StringValue);
    }

    public static bool operator !=(HellString hstrA, HellString hstrB)
    {
        return !string.Equals(hstrA.StringValue, hstrB.StringValue);
    }

    #endregion Operators
}
