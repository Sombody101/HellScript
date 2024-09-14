using HellScriptRuntime.Runtime.BaseTypes.Numbers;
using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes.Collection;

internal sealed class HellArray : IHellType
{
    public object Value => _values;

    public TypeSignature TypeSignature => TypeSignature.Array;

    public Type? Type => typeof(IHellType[]);

    public readonly IHellType?[] _values;

    public HellArray(HellInteger length)
    {
        _values = new IHellType[length.ToInt64()];
    }

    public HellArray(long length)
    {
        _values = new IHellType[length];
    }

    public void SetElement(long index, IHellType? value)
    {
        _values[index] = value;
    }

    public IHellType? GetElement(long index)
    {
        return _values[index];
    }

    public IHellType Clone()
    {
        long length = _values.Length;
        var newArray = new HellArray(length);

        for (int i = 0; i < length; ++i)
            newArray._values[i] = _values[i]?.Clone();

        return newArray;
    }

    public bool Equals(IHellType? other)
    {
        throw new NotImplementedException();
    }
}
