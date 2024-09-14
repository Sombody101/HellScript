using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes.Collection;

/// <summary>
/// Essentially a <see langword="readonly"/> <see cref="HellArray"/>
/// </summary>
internal sealed class HellStruct : IHellType
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

    public bool Equals(IHellType? other)
    {
        return other is not null && TypeSignature == other.TypeSignature;
    }

    public int CompareTo(IHellType? other)
    {
        throw new NotImplementedException();
    }
}