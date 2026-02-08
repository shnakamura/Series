using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Common.Shooting;

/// <summary>
///     Handles shooting a projectile from an item at a specified interval while the item is being
///     used.
/// </summary>
/// <remarks>
///     The new projectile is shot via <see cref="Shoot" /> every time the internal shot counter
///     exceeds the configured shoot interval.
/// </remarks>
public sealed class ItemIntervalShootingComponent : ItemComponent
{
    /// <summary>
    ///     Gets the number of shots between each interval-triggered projectile.
    /// </summary>
    public int ShootInterval { get; private set; }

    /// <summary>
    ///     Gets the type of the projectile to shoot when the interval condition is met. Defaults to
    ///     <c>-1</c>, which means no projectile will be shot.
    /// </summary>
    public int ShootType { get; private set; } = -1;

    /// <summary>
    ///     Gets the amount of projectiles to shoot when the interval condition is met. Defaults to
    ///     <c>1</c>.
    /// </summary>
    public int ShootAmount { get; private set; } = 1;

    /// <summary>
    ///     Gets the current number of shots counted since the last interval-triggered projectile was shot.
    /// </summary>
    public int ShootCounter { get; private set; }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!Enabled)
        {
            return true;
        }

        ShootCounter++;

        if (ShootCounter < ShootInterval)
        {
            return true;
        }
        
        for (var i = 0; i < ShootAmount; i++)
        {
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, ShootType, damage, knockback, player.whoAmI);

            projectile.hostile = false;
            projectile.friendly = true;
        }

        ShootCounter = 0;

        return true;
    }

    /// <summary>
    ///     Sets the interval-based shooting behavior.
    /// </summary>
    /// <param name="shootInterval">The number of shots between each interval-triggered projectile.</param>
    /// <param name="shootType">The type of the projectile to shoot when the interval condition is met.</param>
    /// <param name="shootAmount">The amount of projectiles to shoot when the interval condition is met.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="shootInterval" /> or <paramref name="shootType" /> or
    ///     <paramref name="shootAmount" /> is less than orequal to zero.
    /// </exception>
    public void Set(int shootInterval, int shootType, int shootAmount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(shootInterval, nameof(shootInterval));

        ShootInterval = shootInterval;

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(shootType, nameof(shootType));

        ShootType = shootType;

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(shootAmount, nameof(shootAmount));

        ShootAmount = shootAmount;
    }
}