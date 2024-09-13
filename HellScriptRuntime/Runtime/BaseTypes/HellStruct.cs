using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes;

/// <summary>
/// Essentially a <see langword="readonly"/> <see cref="HellArray"/>
/// </summary>
internal class HellStruct : IHellType
{
    private readonly HellStructMetadata structMetadata;

    public object Value => StructureMembers;

    private readonly int structType;
    public TypeSignature TypeSignature => (TypeSignature)structType;

    public Type? Type => throw new NotImplementedException();

    public readonly IHellType?[] StructureMembers;

    public HellStruct(HellStructMetadata typeData)
    {
        StructureMembers = new IHellType?[typeData.MemberCount];

        structMetadata = typeData;
    }

    public IHellType Clone()
    {
        var newStruct = new HellStruct(structMetadata);

        int memberLen = structMetadata.MemberCount;
        var newMembers = newStruct.StructureMembers;
        for (int i = 0; i < memberLen; ++i)
        {
            newMembers[i] = StructureMembers[i]?.Clone();
        }

        return newStruct;
    }

    public void SetItem(int index, IHellType? value)
    {
        StructureMembers[index] = value;
    }

    public IHellType? GetItem(int index)
    {
        return StructureMembers[index];
    }

    public int CompareTo(IHellType? other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IHellType? other)
    {
        return other is not null && TypeSignature == other.TypeSignature;
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
}