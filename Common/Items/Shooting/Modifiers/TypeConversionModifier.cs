namespace Series.Common.Items.Shooting;

public sealed class TypeConversionModifier : IShootContextModifier
{
    /// <summary>
    ///     Gets the type of the projectile to be converted.
    /// </summary>
    public int OldProjectileType { get; }

    /// <summary>
    ///     Gets the type of the projectile to convert to.
    /// </summary>
    public int NewProjectileType { get; }

    public TypeConversionModifier(int oldProjectileType, int newProjectileType)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(oldProjectileType, nameof(oldProjectileType));
        
        OldProjectileType = oldProjectileType;
        
        ArgumentOutOfRangeException.ThrowIfNegative(newProjectileType, nameof(newProjectileType));
        
        NewProjectileType = newProjectileType;
    }

    void IShootContextModifier.Modify(ref ItemShootContext context)
    {
        if (context.Type != OldProjectileType)
        {
            return;
        }

        context.Type = NewProjectileType;
    }
}
