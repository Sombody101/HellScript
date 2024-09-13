using HellScriptShared.Bytecode;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HellScriptRuntime.Runtime.BaseTypes;

/// <summary>
/// A integer (signed or not). Essentially a wrapper for <see cref="BigInteger"/> 
/// (Size 16)
/// </summary>
internal class HellInteger : IHellType
{
    public HellInteger(BigInteger value)
    {
        BigValue = value;
    }

    public TypeSignature TypeSignature => TypeSignature.Int;

    public Type Type => typeof(long);

    public object Value => BigValue;

    public BigInteger BigValue;

    public IHellType Clone()
    {
        return new HellInteger(BigValue);
    }

    #region Interfaces

    /* IComparable */

    public int CompareTo(HellInteger other)
    {
        return BigValue.CompareTo(other.BigValue);
    }

    /* IEquitable */

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return BigValue.Equals(obj);
    }

    public bool Equals(HellInteger? other)
    {
        return BigValue.Equals(other);
    }

    /* IConvertible */

    public TypeCode GetTypeCode()
    {
        return TypeCode.Int64;
    }

    public bool ToBoolean(IFormatProvider? provider)
    {
        return BigValue.IsOne;
    }

    public byte ToByte(IFormatProvider? provider)
    {
        return (byte)BigValue;
    }

    public char ToChar(IFormatProvider? provider)
    {
        throw new InvalidOperationException("A long cannot be converted to a char");
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        return new DateTime((long)BigValue, DateTimeKind.Local);
    }

    public decimal ToDecimal(IFormatProvider? provider)
    {
        return Convert.ToDecimal(BigValue);
    }

    public double ToDouble(IFormatProvider? provider)
    {
        return Convert.ToDouble(BigValue);
    }

    public short ToInt16(IFormatProvider? provider)
    {
        return (short)BigValue;
    }

    public int ToInt32(IFormatProvider? provider)
    {
        return (int)BigValue;
    }

    public long ToInt64(IFormatProvider? provider)
    {
        return (long)BigValue;
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        return (sbyte)BigValue;
    }

    public float ToSingle(IFormatProvider? provider)
    {
        return Convert.ToSingle(BigValue);
    }

    public string ToString(IFormatProvider? provider)
    {
        return BigValue.ToString();
    }

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        var converted = Convert.ChangeType(BigValue, conversionType);

        if (converted is null)
            return new object();

        return converted;
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        return (ushort)BigValue;
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        return (uint)BigValue;
    }

    public ulong ToUInt64(IFormatProvider? provider)
    {
        return (ulong)BigValue;
    }

    public int CompareTo(IHellType? other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IHellType? other)
    {
        throw new NotImplementedException();
    }

    #endregion Interfaces

    #region Operators

    public static bool operator ==(HellInteger a, HellInteger b)
    {
        return Equals(a.Value, b.Value);
    }

    public static bool operator !=(HellInteger a, HellInteger b)
    {
        return Equals(a.Value, b.Value);
    }

    public static HellInteger operator +(HellInteger a, HellInteger b)
        => new(a.BigValue + b.BigValue);

    public static HellNumber operator +(HellInteger a, HellNumber b)
        => new(a.BigValue + b.BigValue);

    #endregion
}
