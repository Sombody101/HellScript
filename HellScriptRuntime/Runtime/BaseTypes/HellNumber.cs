using HellScriptRuntime.Runtime.BaseTypes.Numerics;
using System.Diagnostics.CodeAnalysis;

namespace HellScriptRuntime.Runtime.BaseTypes;

/// <summary>
/// A floating point <see cref="System.Numerics.BigInteger"/>
/// Size 24
/// </summary>
internal struct HellNumber : IHellType
{
    public HellNumber(BigDecimal value)
    {
        BigValue = value;
    }

    public readonly int TypeSignature => 0x02;

    public readonly Type Type => typeof(long);

    public readonly object Value => BigValue;

    public BigDecimal BigValue;

    public IHellType Clone()
    {
        return new HellNumber(BigValue);
    }

    #region Interfaces

    /* IComparable */

    public readonly int CompareTo(HellNumber other)
    {
        return BigValue.CompareTo(other.BigValue);
    }

    /* IEquitable */

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return base.Equals(obj);
    }

    public bool Equals(HellNumber other)
    {
        throw new NotImplementedException();
    }

    /* IConvertible */

    public readonly TypeCode GetTypeCode()
    {
        return TypeCode.Int64;
    }

    public readonly bool ToBoolean(IFormatProvider? provider)
    {
        return default;
    }

    public readonly byte ToByte(IFormatProvider? provider)
    {
        return (byte)BigValue;
    }

    public char ToChar(IFormatProvider? provider)
    {
        throw new InvalidOperationException("A long cannot be converted to a char");
    }

    public readonly DateTime ToDateTime(IFormatProvider? provider)
    {
        return new DateTime((long)BigValue, DateTimeKind.Local);
    }

    public readonly decimal ToDecimal(IFormatProvider? provider)
    {
        return Convert.ToDecimal(BigValue);
    }

    public readonly double ToDouble(IFormatProvider? provider)
    {
        return Convert.ToDouble(BigValue);
    }

    public readonly short ToInt16(IFormatProvider? provider)
    {
        return (short)BigValue;
    }

    public readonly int ToInt32(IFormatProvider? provider)
    {
        return (int)BigValue;
    }

    public readonly long ToInt64(IFormatProvider? provider)
    {
        return (long)BigValue;
    }

    public readonly sbyte ToSByte(IFormatProvider? provider)
    {
        return (sbyte)BigValue;
    }

    public readonly float ToSingle(IFormatProvider? provider)
    {
        return Convert.ToSingle(BigValue);
    }

    public readonly string ToString(IFormatProvider? provider)
    {
        return BigValue.ToString();
    }

    public readonly object ToType(Type conversionType, IFormatProvider? provider)
    {
        var converted = Convert.ChangeType(BigValue, conversionType);

        if (converted is null)
            return new object();

        return converted;
    }

    public readonly ushort ToUInt16(IFormatProvider? provider)
    {
        return (ushort)BigValue;
    }

    public readonly uint ToUInt32(IFormatProvider? provider)
    {
        return (uint)BigValue;
    }

    public readonly ulong ToUInt64(IFormatProvider? provider)
    {
        return (ulong)BigValue;
    }

    public int CompareTo(HellInteger other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(HellInteger other)
    {
        throw new NotImplementedException();
    }

    #endregion Interfaces
}
