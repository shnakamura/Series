using Series.Core.Items;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Series.Common.Items.Bursts;

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
public sealed class ItemBurstData : ItemComponent
{
    /// <summary>
    ///     Gets the amount of shots to fire in each burst.
    /// </summary>
    public int Amount { get; private set; }

    /// <summary>
    ///     Gets or sets whether to play the item's use sound on each shot in the burst.
    /// </summary>
    public bool PlaySound { get; set; } = true;

    public override GlobalItem Clone(Item from, Item to)
    {
        var original = base.Clone(from, to);
        
        if (original is not ItemBurstData clone)
        {
            return original;
        }

        clone.Amount = Amount;
        clone.PlaySound = PlaySound;

        return clone;
    }

    /// <summary>
    ///     Sets the amount of shots to fire in each burst.
    /// </summary>
    /// <param name="amount">The amount of shots to fire in each burst.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="amount" /> is less than or equal to zero.
    /// </exception>
    public ItemBurstData SetBursts(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, nameof(amount));

        Amount = amount;

        return this;
    }
}