using System.Collections.Generic;

namespace Series.Common.Items.Shooting;

public abstract class ItemShootPattern
{
    /// <summary>
    ///     Gets the list of shoot context modifiers to apply before shooting a projectile.
    /// </summary>
    internal List<IShootContextModifier> ShootModifiers { get; } = new();
    
    /// <summary>
    ///     Gets the list of projectile modifiers to apply after shooting a projectile.
    /// </summary>
    internal List<IProjectileModifier> ProjectileModifiers { get; } = new();

    public abstract int Shoot(in ItemShootContext context);
    
    /// <summary>
    ///     Adds a shoot context modifier to be applied before shooting a projectile.
    /// </summary>
    /// <param name="modifier">The shoot context modifier to be applied before shooting a projectile.</param>
    /// <returns></returns>
    public ItemShootPattern AddShootModifier<T>(T modifier) where T : IShootContextModifier
    {
        ShootModifiers.Add(modifier);
        
        return this;
    }
    
    /// <summary>
    ///     Adds a projectile modifier to be applied after shooting a projectile.
    /// </summary>
    /// <param name="modifier">The projectile modifier to be applied after shooting a projectile.</param>
    /// <returns></returns>
    public ItemShootPattern AddProjectileModifier<T>(T modifier) where T : IProjectileModifier
    {
        ProjectileModifiers.Add(modifier);
        
        return this;
    }
}