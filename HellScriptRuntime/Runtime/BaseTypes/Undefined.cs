using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes;

internal readonly struct Undefined : IHellType
{
    public static readonly Undefined Null = new();

    public object Value => Null;

    public TypeSignature TypeSignature => TypeSignature.Undefined;

    public Type? Type => typeof(Undefined);

    public IHellType Clone()
    {
        return this;
    }

    public bool Equals(IHellType? other)
    {
        return other is Undefined;
    }
}
