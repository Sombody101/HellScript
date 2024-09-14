using System.Numerics;

namespace HellScriptRuntime.Runtime.BaseTypes.Numbers;

internal interface IHellNumber : IHellType
{
    public int ToInt32();

    public long ToInt64();

    public string? ToString();

    #region Number Initialization

    public static IHellNumber GetInteger(ulong number, bool negative)
    {
        return new HellInteger(number, negative);
    }

    public static IHellNumber GetInteger(BigInteger number)
    {
        return new HellBigInteger(number);
    }

    public static IHellNumber GetNumber()
    {
        return new HellBigNumber();
    }

    #endregion Number Initialization

    #region Mathables

    /*
     * Add
     * Subtract
     * Multiply
     * Divide
     * Modulo
     */

    public static IHellNumber Add(IHellNumber left, IHellNumber right)
    {
        if (left is HellInteger && right is HellInteger)
        {
            return (HellInteger)left + (HellInteger)right;
        }
        else if (left is HellInteger && right is HellBigInteger)
        {
            return (HellInteger)left + (HellBigInteger)right;
        }
        else if (left is HellBigInteger && right is HellInteger)
        {
            return (HellBigInteger)left + (HellInteger)right;
        }
        else if (left is HellBigInteger && right is HellBigInteger)
        {
            return (HellBigInteger)left + (HellBigInteger)right;
        }

        throw new NotImplementedException($"Not able to add values of type {left.HellName()} and {right.HellName()}");
    }

    public static IHellNumber Subtract(IHellNumber left, IHellNumber right)
    {
        if (left is HellInteger && right is HellInteger)
        {
            return (HellInteger)left - (HellInteger)right;
        }
        else if (left is HellInteger && right is HellBigInteger)
        {
            return (HellInteger)left - (HellBigInteger)right;
        }
        else if (left is HellBigInteger && right is HellInteger)
        {
            return (HellBigInteger)left - (HellInteger)right;
        }
        else if (left is HellBigInteger && right is HellBigInteger)
        {
            return (HellBigInteger)left - (HellBigInteger)right;
        }

        throw new NotImplementedException($"Not able to subtract values of type {left.HellName()} and {right.HellName()}");
    }

    // public static IHellNumber Subtract(IHellNumber a, IHellNumber b);
    // public static IHellNumber Multiply(IHellNumber a, IHellNumber b);
    // public static IHellNumber Divide(IHellNumber a, IHellNumber b);
    // public static IHellNumber Modulo(IHellNumber a, IHellNumber b);
    // public static IHellNumber Negate(IHellNumber a);

    public static bool GreaterThan(IHellNumber left, IHellNumber right)
    {
        if (left is HellInteger && right is HellInteger)
        {
            return (HellInteger)left > (HellInteger)right;
        }
        else if (left is HellInteger && right is HellBigInteger)
        {
            return (HellInteger)left > (HellBigInteger)right;
        }
        else if (left is HellBigInteger && right is HellInteger)
        {
            return (HellBigInteger)left > (HellInteger)right;
        }
        else if (left is HellBigInteger && right is HellBigInteger)
        {
            return (HellBigInteger)left > (HellBigInteger)right;
        }

        throw new NotImplementedException($"Not able to compare (GT) values of type {left.HellName()} and {right.HellName()}");
    }

    // This doesnt really speed things up as !GreaterThan() could also be used, but it was easy to setup. So...
    public static bool LessThan(IHellNumber left, IHellNumber right)
    {
        if (left is HellInteger && right is HellInteger)
        {
            return (HellInteger)left < (HellInteger)right;
        }
        else if (left is HellInteger && right is HellBigInteger)
        {
            return (HellInteger)left < (HellBigInteger)right;
        }
        else if (left is HellBigInteger && right is HellInteger)
        {
            return (HellBigInteger)left < (HellInteger)right;
        }
        else if (left is HellBigInteger && right is HellBigInteger)
        {
            return (HellBigInteger)left < (HellBigInteger)right;
        }

        throw new NotImplementedException($"Not able to compare (LT) values of type {left.HellName()} and {right.HellName()}");
    }

    #endregion
}
