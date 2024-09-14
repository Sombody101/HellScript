using HellScriptShared.Bytecode;

namespace HellScriptRuntime.Runtime.BaseTypes;

internal interface IHellType : IEquatable<IHellType>
{
    public object Value { get; }

    public TypeSignature TypeSignature { get; }

    public Type? Type { get; }

    public IHellType Clone();
}
