using System.Collections.Generic;
using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Common.Items.Shooting;

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
    
    /// <summary>
    ///     Gets the list of shoot patterns to apply when shooting a projectile.
    /// </summary>
    private List<ItemShootPattern> Patterns { get; } = new();

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!Enabled)
        {
            return true;
        }
        
        var context = new ItemShootContext(source, item, player, position, velocity, type, damage, knockback);
        
        ApplyModifiers(ref context, player);
        ApplyShootPatterns(ref context, player);

        return false;
    }

    /// <summary>
    ///     Adds a shoot context modifier to be applied before shooting a projectile.
    /// </summary>
    /// <param name="modifier">The shoot context modifier to be applied before shooting a projectile.</param>
    /// <typeparam name="T">The type of the shoot context modifier to be applied before shooting a projectile.</typeparam>
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
    /// <typeparam name="T">The type of the projectile modifier to be applied after shooting a projectile.</typeparam>
    /// <returns></returns>
    public ItemShootComponent AddProjectileModifier<T>(T modifier) where T : IProjectileModifier
    {
        ProjectileModifiers.Add(modifier);

        return this;
    }
    
    /// <summary>
    ///     Adds a shoot pattern to be applied when shooting a projectile.
    /// </summary>
    /// <param name="pattern">The shoot pattern to be applied when shooting a projectile.</param>
    /// <typeparam name="T">The type of the shoot patterrn to be applied when shooting a projectile.</typeparam>
    /// <returns></returns>
    public ItemShootComponent AddShootPattern<T>(T pattern) where T : ItemShootPattern
    {
        Patterns.Add(pattern);

        return this;
    }
    
    /// <summary>
    ///     Applies the shoot context modifiers and projectile modifiers to the given shoot context and player.
    /// </summary>
    /// <param name="context">The <see cref="ItemShootContext"/> on which to apply the modifiers.</param>
    /// <param name="player">The <see cref="Player"/> on which to apply the modifiers.</param>
    private void ApplyModifiers(ref ItemShootContext context, Player player)
    {
        foreach (var modifier in ShootModifiers)
        {
            modifier.Modify(ref context);
        }

        var projectile = Projectile.NewProjectileDirect(context.Source, context.Position, context.Velocity, context.Type, context.Damage, context.Knockback, player.whoAmI);
        
        foreach (var modifier in ProjectileModifiers)
        {
            modifier.Modify(projectile);
        }
    }

    /// <summary>
    ///     Applies the shoot patterns to the given shoot context and player.
    /// </summary>
    /// <param name="context">The <see cref="ItemShootContext"/> on which to apply the modifiers.</param>
    /// <param name="player">The <see cref="Player"/> on which to apply the modifiers.</param>
    private void ApplyShootPatterns(ref ItemShootContext context, Player player)
    {
        foreach (var pattern in Patterns)
        {
            foreach (var modifier in pattern.ShootModifiers)
            {
                modifier.Modify(ref context);
            }

            var index = pattern.Shoot(in context);

            if (index == -1)
            {
                return;
            }

            var projectile = Main.projectile[index];
        
            foreach (var modifier in pattern.ProjectileModifiers)
            {
                modifier.Modify(projectile);
            }
        }
    }
}