using HellScriptShared.Bytecode;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HellScriptRuntime.Runtime.BaseTypes.Numbers;

/// <summary>
/// A integer (signed or not). Essentially a wrapper for <see cref="BigInteger"/> 
/// (Size 16)
/// </summary>
internal readonly struct HellBigInteger : IHellNumber
{
    public HellBigInteger(BigInteger value)
    {
        BigValue = value;
    }

    public TypeSignature TypeSignature => TypeSignature.BigInt;

    public Type Type => typeof(BigInteger);

    public object Value => BigValue;

    public readonly BigInteger BigValue;

    public IHellType Clone()
    {
        return this;
    }

    public readonly override bool Equals([NotNullWhen(true)] object? obj)
    {
        return BigValue.Equals(obj);
    }

    public readonly bool Equals(IHellType? other)
    {
        return BigValue.Equals(other);
    }

    public readonly int CompareTo(IHellType? other)
    {
        return BigValue.CompareTo(other.As<HellBigInteger>().BigValue);
    }

    public readonly int CompareTo(HellBigInteger other)
    {
        return BigValue.CompareTo(other.BigValue);
    }

    public readonly override string ToString()
    {
        return BigValue.ToString();
    }

    public static bool operator ==(HellBigInteger a, HellBigInteger b)
    {
        return Equals(a.Value, b.Value);
    }

    public static bool operator !=(HellBigInteger a, HellBigInteger b)
    {
        return Equals(a.Value, b.Value);
    }

    public readonly override int GetHashCode()
    {
        return BigValue.GetHashCode();
    }

    public readonly int ToInt32()
    {
        return (int)BigValue;
    }

    public readonly long ToInt64()
    {
        return (long)BigValue;
    }

    #region Operators

    public static IHellNumber operator +(HellBigInteger a, HellBigInteger b)
    {
        BigInteger newVal = a.BigValue + b.BigValue;

        return new HellBigInteger(newVal);
    }

    public static IHellNumber operator +(HellBigInteger a, HellInteger b)
    {
        BigInteger newVal = a.BigValue + (BigInteger)b;
        return new HellBigInteger(newVal);
    }

    public static IHellNumber operator -(HellBigInteger a, HellBigInteger b)
    {
        return new HellBigInteger(a.BigValue - b.BigValue);
    }

    public static IHellNumber operator -(HellBigInteger a, HellInteger b)
    {
        return new HellBigInteger(a.BigValue - (BigInteger)b);
    }

    public static bool operator >(HellBigInteger a, HellBigInteger b)
    {
        return a.BigValue > b.BigValue;
    }

    public static bool operator >(HellBigInteger a, HellInteger b)
    {
        return a.BigValue > (BigInteger)b;
    }

    public static bool operator <(HellBigInteger a, HellBigInteger b)
    {
        return a.BigValue > b.BigValue;
    }

    public static bool operator <(HellBigInteger a, HellInteger b)
    {
        return a.BigValue < (BigInteger)b;
    }

    public static explicit operator HellBigInteger(HellInteger hint)
    {
        BigInteger bi = new(hint.SmallValue);

        if (hint.Negative)
            bi = BigInteger.Negate(bi);

        return new(bi);
    }

    #endregion Operators
}
