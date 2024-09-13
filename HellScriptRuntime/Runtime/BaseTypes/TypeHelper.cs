using HellScriptRuntime.Runtime.BaseTypes.Numerics;
using System.Numerics;

namespace HellScriptRuntime.Runtime.BaseTypes;

internal static class TypeHelper
{
    public static bool IsNumber(this IHellType? value)
    {
        return value is not null
            && value.Value is BigInteger or BigDecimal;
    }

    public static bool IsSignedNumber(this IHellType? value)
    {
        return value is not null
            && value.Value is BigInteger;
    }

    public static bool IsFloatingNumber(this IHellType? value)
    {
        return value is not null
            && value.Value is BigDecimal;
    }

    public static bool TypesMatch(this IHellType? a, IHellType? b)
    {
        if (a is null != b is null)
            return false;

        if (a is null) // b is be null if a is null
            return true;

        return a.TypeSignature == b!.TypeSignature;
    }

    public static string HellName(this IHellType? value)
    {
        if (value is not null)
        {
            if (value.Type is null)
                return $"{value.GetType().Name}:NULL";

            return value.Type.Name;
        }

        return "NULL";
    }

    public static string CreateFrameId(this string prefix)
        => $"{prefix}{Random.Shared.Next():x000000}";

    public static T As<T>(this IHellType? value) // where T : class, new()
    {
        try
        {
            if (value is null)
                throw new Exception($"Attempted to cast null to {typeof(T).Name}");

            return (T)value;
        }
        catch (InvalidCastException cex)
        {
            throw new InvalidCastException($"Attempted to cast IHellType:{HellName(value)} to {typeof(T).Name} ");
        }
    }
}
