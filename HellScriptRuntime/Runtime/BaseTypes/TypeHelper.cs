using HellScriptRuntime.Runtime.BaseTypes.Numbers;
using System.Numerics;
using System.Runtime.InteropServices;

namespace HellScriptRuntime.Runtime.BaseTypes;

internal static class TypeHelper
{
    public static bool IsNumber(this IHellType? value)
    {
        return value is IHellNumber;
    }

    public static bool IsSolidNumber(this IHellType? value)
    {
        return value is not null && value.Value is BigInteger;
    }

    // Floating numbers have not been fully implemented, so this should not be used anywhere yet
    public static bool IsFloatingNumber(this IHellType? value)
    {
        return value is not null;//&& value.Value is BigDecimal;
    }

    public static bool TypesMatch(this IHellType? a, IHellType? b)
    {
        if (a is null != b is null)
            return false;

        if (a is null) // b is be null if a is null
            return true;

        return a.TypeSignature == b!.TypeSignature;
    }

    /// <summary>
    /// Returns an adjusted type name (so type names are not polluted with .NET namespaces and classes)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string HellName(this IHellType? value)
    {
        if (value is not null)
        {
            if (value.Type is null)
            {
                // "HellInteger:NULL"
                return $"{value.GetType().Name}:NULL";
            }

            // "HellInteger"
            return value.Type.Name;
        }

        return "NULL";
    }

    /// <summary>
    /// Attempts to cast <paramref name="value"/> to <typeparamref name="T"/> and gives a more "adjusted" message if the
    /// cast fails.
    /// 
    /// <list type="bullet|number|table">
    ///    <item>
    ///        <term>%S</term>
    ///        <description>The type name for the source value (<paramref name="value"/>)</description>
    ///    </item>
    ///    <item>
    ///        <term>%T</term>
    ///        <description>The type name for the </description>
    ///    </item>
    /// </list>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public static T As<T>(this IHellType? value, [Optional] string errorMessage)
    {
        try
        {
            if (value is null)
            {
                throw new InvalidCastException($"Attempted to cast null to {typeof(T).Name}");
            }

            return (T)value;
        }
        catch (InvalidCastException)
        {
            if (errorMessage is not null)
            {
                throw new InvalidCastException(errorMessage
                    .Replace("%S", HellName(value))
                    .Replace("%T", typeof(T).Name));
            }

            throw new InvalidCastException($"Attempted to cast IHellType:{HellName(value)} to {typeof(T).Name}");
        }
    }
}
