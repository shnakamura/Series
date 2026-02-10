using System.Collections.Generic;
using Series.Common.Shooting.Modifiers;
using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Common.Shooting;

public sealed class ItemShootComponent : ItemComponent
{
    /// <summary>
    ///     Gets the list of shoot context modifiers to apply before shooting a projectile.
    /// </summary>
    private List<IShootContextModifier> ShootModifiers { get; } = new();
    
    /// <summary>
    ///     Gets the list of projectile modifiers to apply after shooting a projectile.
    /// </summary>
    private List<IProjectileModifier> ProjectileModifiers { get; } = new();

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!Enabled)
        {
            return true;
        }
        
        var context = new ItemShootContext(source, item, player, position, velocity, type, damage, knockback);
        
        foreach (var modifier in ShootModifiers)
        {
            modifier.Modify(ref context);
        }

        var projectile = Projectile.NewProjectileDirect(context.Source, context.Position, context.Velocity, context.Type, context.Damage, context.Knockback, player.whoAmI);
        
        foreach (var modifier in ProjectileModifiers)
        {
            modifier.Modify(projectile);
        }

        return false;
    }
    
    /// <summary>
    ///     Adds a shoot context modifier to be applied before shooting a projectile.
    /// </summary>
    /// <param name="modifier">The shoot context modifier to be applied before shooting a projectile.</param>
    /// <returns></returns>
    public ItemShootComponent AddShootModifier<T>(T modifier) where T : IShootContextModifier
    {
        ShootModifiers.Add(modifier);

        return this;
    }

    /// <summary>
    ///     Adds a projectile modifier to be applied after shooting a projectile.
    /// </summary>
    /// <param name="modifier">The projectile modifier to be applied after shooting a projectile.</param>
    /// <returns></returns>
    public ItemShootComponent AddProjectileModifier<T>(T modifier) where T : IProjectileModifier
    {
        ProjectileModifiers.Add(modifier);

        return this;
    }
}