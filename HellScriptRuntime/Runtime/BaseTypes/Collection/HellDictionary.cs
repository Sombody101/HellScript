using HellScriptRuntime.Runtime.BaseTypes.Numbers;
using HellScriptRuntime.Runtime.BaseTypes.Text;
using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes.Collection;

internal sealed class HellDictionary : IHellType
{
    readonly Dictionary<IHellType, IHellType?> _array;

    public object Value => _array;

    public TypeSignature TypeSignature => TypeSignature.Dictionary;

    public Type? Type => typeof(Dictionary<IHellType, IHellType?>);

    public HellDictionary()
    {
        _array = [];
    }

    public HellDictionary(HellBigInteger maxValue)
    : this((int)maxValue.BigValue)
    { }

    public HellDictionary(HellBigNumber maxValue)
        : this((int)maxValue.BigValue)
    { }

    public HellDictionary(Dictionary<int, object?> array)
    {
        foreach (var pair in array)
        {
            var pValue = pair.Value;
            var value = pValue switch
            {
                string => new HellString((string)pValue),
                // TODO
                IHellType => pValue,
                null => null,
            };

            array.Add(pair.Key, value);
        }
    }

    public HellDictionary(Dictionary<IHellType, IHellType?> array)
    {
        _array = array;
    }

    public HellDictionary(int maxValues)
    {
        _array = new(maxValues);
    }

    public IHellType Clone()
    {
        Dictionary<IHellType, IHellType?> clone = new(_array);

        return new HellDictionary(clone);
    }

    /// <summary>
    /// Sets the value at <paramref name="key"/> to <paramref name="value"/>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException"></exception>
    public void SetValue(IHellType? key, IHellType? value)
    {
        if (key is null)
            throw new ArgumentException("Array index cannot be null");

        _array[key] = value;
    }

    /// <summary>
    /// Gets the value at <paramref name="key"/> (Does not delete the value)
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IHellType? GetValueWithKey(IHellType? key)
    {
        if (key is null)
            return null;

        _array.TryGetValue(key, out IHellType? value);

        return value;
    }

    public bool Equals(IHellType? other)
    {
        throw new NotImplementedException();
    }
}