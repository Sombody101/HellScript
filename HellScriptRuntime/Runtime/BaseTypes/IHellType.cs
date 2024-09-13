using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes;

internal interface IHellType : IComparable<IHellType>,
    IEquatable<IHellType>,
    IConvertible
{
    public object Value { get; }

    public TypeSignature TypeSignature { get; }

    public Type? Type { get; }

    public IHellType Clone();
}
