using System.Collections.Generic;
using Series.Common.Shooting.Modifiers;
using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Common.Shooting;

public sealed class ItemShootComponent : ItemComponent
{
    /// <summary>
    ///     Gets the list of shoot context modifiers to apply when shooting a projectile.
    /// </summary>
    private List<IShootContextModifier> Modifiers { get; } = new();

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!Enabled)
        {
            return true;
        }
        
        var context = new ItemShootContext(source, item, player, position, velocity, type, damage, knockback);
        
        foreach (var modifier in Modifiers)
        {
            modifier.Modify(ref context);
        }

        Projectile.NewProjectile(context.Source, context.Position, context.Velocity, context.Type, context.Damage, context.Knockback, player.whoAmI);

        return false;
    }
    
    /// <summary>
    ///     Adds a shoot context modifier to be applied when shooting a projectile.
    /// </summary>
    /// <param name="modifier">The shoot context modifier to be applied when shooting a projectile.</param>
    /// <typeparam name="T">The type of the shoot context modifier to be applied when shooting a projectile.</typeparam>
    /// <returns></returns>
    public ItemShootComponent AddModifier<T>(T modifier) where T : IShootContextModifier
    {
        Modifiers.Add(modifier);

        return this;
    }
}