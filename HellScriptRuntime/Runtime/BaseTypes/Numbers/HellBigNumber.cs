using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes.Numbers;

/// <summary>
/// Not implemented, do not use
/// </summary>
internal readonly struct HellBigNumber : IHellNumber
{
    public object Value => BigValue;

    public TypeSignature TypeSignature => TypeSignature.BigNumber;

    public Type? Type => typeof(decimal);

    public readonly decimal BigValue;

    public HellBigNumber(long value)
    {
        BigValue = value;
    }

    public HellBigNumber(HellBigInteger bigValue)
    {
        BigValue = bigValue.ToInt64();
    }

    public IHellType Clone()
    {
        return this;
    }

    public int CompareTo(IHellType? other)
    {
        return 0;
    }

    public bool Equals(IHellType? other)
    {
        return other is HellBigNumber && BigValue.Equals(other.Value);
    }

    public int ToInt32()
    {
        return (int)BigValue;
    }

    public long ToInt64()
    {
        return (long)BigValue;
    }
}
