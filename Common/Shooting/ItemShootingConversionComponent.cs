using Series.Core.Items;

namespace Series.Common.Shooting;

/// <summary>
///     Handles converting the type of the projectile shot by an item from one type to another.
/// </summary>
/// <remarks>
///     The conversion is applied via <see cref="ModifyShootStats" />. If the projectile type being
///     shot does not match the specified original type, the conversion is not applied.
/// </remarks>
public sealed class ItemShootingConversionComponent : ItemComponent
{
    /// <summary>
    ///     Gets the projectile type to convert from. Defaults to <c>-1</c>, which means no conversion will
    ///     occur.
    /// </summary>
    public int OldProjectileType { get; private set; } = -1;

    /// <summary>
    ///     Gets the projectile type to convert to. Defaults to <c>-1</c>, which means no conversion will
    ///     occur.
    /// </summary>
    public int NewProjectileType { get; private set; } = -1;

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);

        if (!Enabled || OldProjectileType < 0 || NewProjectileType < 0 || type != OldProjectileType)
        {
            return;
        }

        type = NewProjectileType;
    }

    /// <summary>
    ///     Sets the projectile type conversion to apply when the item shoots a projectile.
    /// </summary>
    /// <param name="oldProjectileType">The projectile type that must be shot for the conversion to occur.</param>
    /// <param name="newProjectileType">
    ///     The projectile type to substitute when
    ///     <paramref name="oldProjectileType" /> is shot.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="oldProjectileType" /> or <paramref name="newProjectileType" /> is
    ///     less than zero.
    /// </exception>
    public void Set(int oldProjectileType, int newProjectileType)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(oldProjectileType, nameof(oldProjectileType));

        OldProjectileType = oldProjectileType;

        ArgumentOutOfRangeException.ThrowIfNegative(newProjectileType, nameof(newProjectileType));

        NewProjectileType = newProjectileType;
    }
}