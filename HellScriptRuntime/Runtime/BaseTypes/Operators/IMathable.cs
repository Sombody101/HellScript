using HellScriptRuntime.Runtime.BaseTypes.Numerics;
using System.Numerics;

namespace HellScriptRuntime.Runtime.BaseTypes.Operators;

internal static class Mathable
{
    public static IHellType Add(IHellType? _a, IHellType? _b)
    {
        if (!_a.IsNumber() || !_b.IsNumber())
            throw new InvalidOperationException($"Cannot add values of type '{_a?.Type?.Name}' and '{_b?.Type?.Name}'");

        object a = _a.Value;
        object b = _b.Value;

        if (a is BigInteger bi1 && b is BigInteger bi2)
        {
            return new HellInteger(bi1 + bi2);
        }
        else if (a is BigDecimal bd1 && b is BigDecimal bd2)
        {
            return new HellNumber(bd1 + bd2);
        }
        else if (a is BigInteger bi3 && b is BigDecimal bd3)
        {
            return new HellNumber(bi3 + bd3);
        }
        else if (a is BigDecimal bd4 && b is BigInteger bi4)
        {
            return new HellNumber(bd4 + bi4);
        }

        throw new InvalidOperationException("The values failed to add, invalid types");
    }

    public static IHellType Subtract(IHellType? _a, IHellType? _b)
    {
        if (!_a.IsNumber() || !_b.IsNumber())
            throw new InvalidOperationException($"Cannot add values of type '{_a.Type?.Name}' and '{_b.Type?.Name}'");

        object a = _a.Value;
        object b = _b.Value;

        if (a is BigInteger bi1 && b is BigInteger bi2)
        {
            return new HellInteger(bi1 - bi2);
        }
        else if (a is BigDecimal bd1 && b is BigDecimal bd2)
        {
            return new HellNumber(bd1 - bd2);
        }
        else if (a is BigInteger bi3 && b is BigDecimal bd3)
        {
            return new HellNumber(bi3 - bd3);
        }
        else if (a is BigDecimal bd4 && b is BigInteger bi4)
        {
            return new HellNumber(bd4 - bi4);
        }

        throw new InvalidOperationException("The values failed to add, invalid types");
    }

    public static bool GreaterThan(IHellType? _a, IHellType? _b)
    {
        if (!_a.IsNumber() || !_b.IsNumber())
            throw new InvalidOperationException($"Cannot add values of type '{_a?.Type?.Name}' and '{_b?.Type?.Name}'");

        object a = _a!.Value;
        object b = _b!.Value;

        if (a is BigInteger bi1 && b is BigInteger bi2)
        {
            return bi1 > bi2;
        }
        else if (a is BigDecimal bd1 && b is BigDecimal bd2)
        {
            return bd1 > bd2;
        }
        else if (a is BigInteger bi3 && b is BigDecimal bd3)
        {
            return bi3 > bd3;
        }
        else if (a is BigDecimal bd4 && b is BigInteger bi4)
        {
            return bd4 > bi4;
        }

        throw new InvalidOperationException("The values failed to add, invalid types");
    }
}