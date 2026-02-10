namespace Series.Common.Items.Shooting;

public sealed class TypeModifier : IShootContextModifier
{
    public int Type { get; }

    public TypeModifier(int type)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(type, nameof(type));

        Type = type;
    }
    
    void IShootContextModifier.Modify(ref ItemShootContext context)
    {
        context.Type = Type;
    }
}