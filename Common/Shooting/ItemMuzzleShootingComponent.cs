using Series.Core.Items;

namespace Series.Common.Shooting;

/// <summary>
///     Handles offsetting the spawn position of projectiles shot by an item so that they originate
///     from the muzzle of the weapon.
/// </summary>
/// <remarks>
///     The offset is applied in the direction of the shot via <see cref="ModifyShootStats" />. If an
///     obstruction exists between the original spawn position and the offset position, the offset is
///     not applied.
/// </remarks>
public sealed class ItemMuzzleShootingComponent : ItemComponent
{
    /// <summary>
    ///     Gets the distance, in pixels, to offset the projectile spawn position from the item's position.
    /// </summary>
    public float MuzzleOffset { get; private set; }

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);

        if (!Enabled)
        {
            return;
        }

        var offset = Vector2.Normalize(velocity) * MuzzleOffset;

        if (!Collision.CanHit(position, 0, 0, position + offset, 0, 0))
        {
            return;
        }

        position += offset;
    }

    /// <summary>
    ///     Sets the distance, in pixels, to offset the projectile spawn position from the item's position.
    /// </summary>
    /// <param name="muzzleOffset">
    ///     The distance, in pixels, to offset the projectile spawn position from the
    ///     item's position.
    /// </param>
    public void Set(float muzzleOffset)
    {
        MuzzleOffset = muzzleOffset;
    }
}