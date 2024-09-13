using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes;

internal class HellArray : IHellType
{
    readonly Dictionary<IHellType, IHellType?> _array;

    public object Value => _array;

    public TypeSignature TypeSignature => TypeSignature.Array;

    public Type? Type => typeof(Dictionary<IHellType, IHellType?>);

    public HellArray()
    {
        _array = [];
    }

    public HellArray(Dictionary<int, object?> array)
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

    public HellArray(HellInteger maxValue)
        : this(maxValue.ToInt32(null))
    { }

    public HellArray(HellNumber maxValue)
        : this(maxValue.ToInt32(null))
    { }

    public HellArray(Dictionary<IHellType, IHellType?> array)
    {
        _array = array;
    }

    public HellArray(int maxValues)
    {
        _array = new(maxValues);
    }

    public IHellType Clone()
    {
        Dictionary<IHellType, IHellType?> clone = new(_array);

        return new HellArray(clone);
    }

    public void SetValue(IHellType? key, IHellType? value)
    {
        if (key is null)
            throw new ArgumentException("Array index cannot be null");

        _array[key] = value;
    }

    public IHellType? GetValueWithKey(IHellType? key)
    {
        if (key is null)
            return null;

        _array.TryGetValue(key, out IHellType? value);

        return value;
    }

    public int CompareTo(HellInteger other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(HellInteger other)
    {
        throw new NotImplementedException();
    }

    public TypeCode GetTypeCode()
    {
        throw new NotImplementedException();
    }

    public bool ToBoolean(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public byte ToByte(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public char ToChar(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public decimal ToDecimal(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public double ToDouble(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public short ToInt16(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public int ToInt32(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public long ToInt64(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public float ToSingle(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public string ToString(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public ulong ToUInt64(IFormatProvider? provider)
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
}