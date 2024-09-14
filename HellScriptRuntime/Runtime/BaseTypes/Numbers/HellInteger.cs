using HellScriptShared.Bytecode;
using System.Numerics;

namespace HellScriptRuntime.Runtime.BaseTypes.Numbers;

internal readonly struct HellInteger : IHellNumber
{
    public readonly object Value => SmallValue;

    public TypeSignature TypeSignature => TypeSignature.Int;

    public Type? Type => typeof(ulong);

    public readonly ulong SmallValue;
    public readonly bool Negative;

    public HellInteger(ulong value, bool negative)
    {
        SmallValue = value;
        Negative = negative;
    }

    public HellInteger(int value)
    {
        SmallValue = (ulong)+value;
        Negative = value < 0;
    }

    public IHellType Clone()
    {
        return this;
    }

    public int CompareTo(IHellType? other)
    {
        if (other is HellInteger otherInt)
            // Compare based on value and negativity
            return SmallValue.CompareTo(otherInt.SmallValue) * (Negative ? -1 : 1);

        throw new ArgumentException("Cannot compare HellInteger with a different type.");
    }

    public bool Equals(IHellType? other)
    {
        if (other is HellInteger otherInt)
            return SmallValue == otherInt.SmallValue && Negative == otherInt.Negative;

        return false;
    }

    public static TypeCode GetTypeCode()
    {
        return TypeCode.Int32;
    }

    public int ToInt32()
    {
        if (Negative)
            return -(int)SmallValue;

        return (int)SmallValue;
    }

    public long ToInt64()
    {
        if (Negative)
            return -(long)SmallValue;

        return (long)SmallValue;
    }

    #region Operators

    public static bool operator ==(HellInteger a, HellInteger b)
    {
        return a.Negative == b.Negative && a.SmallValue == b.SmallValue;
    }

    public static bool operator !=(HellInteger a, HellInteger b)
    {
        return a.Negative != b.Negative || a.SmallValue != b.SmallValue;
    }

    public static IHellNumber operator +(HellInteger a, HellInteger b)
    {
        if (!WillOverflow(a.SmallValue, b.SmallValue, a.Negative, b.Negative))
        {
            HellBigInteger _a = (HellBigInteger)a;
            HellBigInteger _b = (HellBigInteger)b;

            return _a + _b;
        }

        ulong resultValue = a.SmallValue + b.SmallValue;
        bool resultNegative = (a.Negative && !b.Negative) || (!a.Negative && b.Negative);

        return new HellInteger(resultValue, resultNegative);
    }

    public static IHellNumber operator +(HellInteger a, HellBigInteger b)
    {
        BigInteger _a = (BigInteger)a;

        return new HellBigInteger(_a + b.BigValue);
    }

    public static IHellNumber operator -(HellInteger a, HellInteger b)
    {
        int bInt = b.Negative ? -1 : 1;
        ulong result = a.SmallValue - (ulong)bInt;
        bool isNegative = (bInt == 1 && result > a.SmallValue) || (bInt == -1 && result < a.SmallValue);

        return new HellInteger(result, isNegative);
    }

    public static IHellNumber operator -(HellInteger a, HellBigInteger b)
    {
        BigInteger _a = (BigInteger)a;

        return new HellBigInteger(_a - b.BigValue);
    }

    public static bool operator >(HellInteger a, HellInteger b)
    {
        if (a.Negative != b.Negative)
        {
            return !a.Negative; // If one is negative and the other is not, the negative one is less
        }

        return a.SmallValue > b.SmallValue;
    }

    public static bool operator >(HellInteger a, HellBigInteger b)
    {
        BigInteger _a = (BigInteger)a;

        return _a > b.BigValue;
    }

    public static bool operator <(HellInteger a, HellInteger b)
    {
        if (a.Negative != b.Negative)
        {
            return a.Negative;
        }

        return a.SmallValue < b.SmallValue;
    }

    public static bool operator <(HellInteger a, HellBigInteger b)
    {
        BigInteger _a = (BigInteger)a;

        return _a < b.BigValue;
    }

    public static implicit operator BigInteger(HellInteger a)
    {
        BigInteger newVal = new(a.SmallValue);

        if (a.Negative)
            newVal = BigInteger.Negate(newVal);

        return newVal;
    }

    #endregion Operators

    private static bool WillOverflow(ulong a, ulong b, bool aIsNegative, bool bIsNegative)
    {
        bool overflow = (a >> 63) + (b >> 63) <= (a + b) >> 63;

        if (aIsNegative && bIsNegative)
        {
            overflow = !overflow;
        }
        else if (aIsNegative || bIsNegative)
        {
            overflow = false;
        }

        return overflow;
    }
}