namespace Series.Common.Shooting.Modifiers;

public sealed class ConversionModifier(int OldProjectileType, int NewProjectileType) : IShootContextModifier
{
    void IShootContextModifier.Modify(ref ItemShootContext context)
    {
        if (context.Type != OldProjectileType)
        {
            return;
        }

        context.Type = NewProjectileType;
    }
}