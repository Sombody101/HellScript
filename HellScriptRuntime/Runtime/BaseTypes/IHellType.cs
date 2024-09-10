namespace HellScriptRuntime.Runtime.BaseTypes;

internal interface IHellType : IComparable<HellInteger>,
    IEquatable<HellInteger>,
    IConvertible
{
    public object Value { get; }

    public int TypeSignature { get; }

    public Type? Type { get; }

    public IHellType Clone();
}
