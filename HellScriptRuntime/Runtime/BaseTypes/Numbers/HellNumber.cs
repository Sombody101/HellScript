using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes.Numbers;

/// <summary>
/// Not implemented, do not use
/// </summary>
internal readonly struct HellNumber : IHellNumber
{
    public object Value => throw new NotImplementedException();

    public TypeSignature TypeSignature => throw new NotImplementedException();

    public Type? Type => throw new NotImplementedException();

    public IHellType Clone()
    {
        throw new NotImplementedException();
    }

    public int CompareTo(IHellType? other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IHellType? other)
    {
        throw new NotImplementedException();
    }

    public int ToInt32()
    {
        throw new NotImplementedException();
    }

    public long ToInt64()
    {
        throw new NotImplementedException();
    }
}
