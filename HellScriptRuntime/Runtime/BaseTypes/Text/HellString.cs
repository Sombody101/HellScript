using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes.Text;

// Size 8
internal class HellString : IHellType
{
    public object Value => StringValue;

    public TypeSignature TypeSignature => TypeSignature.String;

    public Type? Type => typeof(string);

    public readonly string? StringValue;

    public HellString(string? value)
    {
        StringValue = value;
    }

    public IHellType Clone()
    {
        return new HellString(new string(StringValue));
    }

    public int CompareTo(IHellType? other)
    {
        return StringValue?.CompareTo(other) ?? 0;
    }

    public bool Equals(IHellType? other)
    {
        return other?.Equals(this) ?? false;
    }
}
