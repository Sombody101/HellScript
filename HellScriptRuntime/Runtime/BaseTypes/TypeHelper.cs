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
}
