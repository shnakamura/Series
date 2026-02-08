using Series.Core.Items;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Series.Common.Guns;

/// <summary>
///     Handles modifying an item's use time and use animation so that it shoots in bursts of a
///     specified number of shots.
/// </summary>
/// <remarks>
///     The recalculated <see cref="Item.useTime" /> and <see cref="Item.useAnimation" /> are applied
///     via <see cref="SetDefaults" /> by dividing the original use time by the configured burst size
///     and snapping the use animation to an exact multiple of the new use time. This preserves the
///     original firing cadence while allowing multiple shots to be emitted per use without producing
///     extra or fractional shots.
/// </remarks>
public sealed class ItemBurstShootingComponent : ItemComponent
{
    /// <summary>
    ///     Gets the amount of shots to fire in each burst.
    /// </summary>
    public int BurstShotsAmount { get; private set; }
    
    /// <summary>
    ///     Gets whether to play the item's use sound on each shot in the burst.
    /// </summary>
    public bool PlaySound { get; private set; }

    public override void SetDefaults(Item entity)
    {
        base.SetDefaults(entity);

        if (!Enabled)
        {
            return;
        }

        var target = entity.useTime / BurstShotsAmount;

        target = Math.Max(1, target);

        entity.useTime = target;
        entity.useAnimation = target * BurstShotsAmount;

        entity.consumeAmmoOnLastShotOnly = true;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Enabled && PlaySound && player.itemAnimation != player.itemAnimationMax && item.UseSound.HasValue)
        {
            SoundEngine.PlaySound(item.UseSound, position);
        }

        return true;
    }

    /// <summary>
    ///     Sets the amount of shots to fire in each burst.
    /// </summary>
    /// <param name="burstShotsAmount">The amount of shots to fire in each burst.</param>
    /// <param name="playSound">Whether to play the item's use sound on each shot in the burst.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="burstShotsAmount" /> is less than or equal to zero.
    /// </exception>
    public void Set(int burstShotsAmount, bool playSound = true)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(burstShotsAmount, nameof(burstShotsAmount));

        BurstShotsAmount = burstShotsAmount;

        PlaySound = playSound;
    }
}