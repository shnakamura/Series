using System.Collections.Generic;
using Series.Core.Items;

namespace Series.Common.Items.Buffs;

public sealed class ItemBuffComponent : ItemComponent
{
    /// <summary>
    ///     Gets the list of buffs to be applied by the item.
    /// </summary>
    public List<ItemBuffData> Buffs { get; } = new();

    /// <summary>
    ///     Gets the mode in which the buffs will be applied by the item.
    /// </summary>
    public ItemBuffMode Mode { get; private set; } = ItemBuffMode.All;

    /// <summary>
    ///     Adds a buff to be applied by the item.
    /// </summary>
    /// <param name="type">The type of the buff to be applied by the item.</param>
    /// <param name="duration">The duration of the buff to be applied by the item, in frames.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="type" /> or <paramref name="duration" /> is less than or equal to
    ///     zero.
    /// </exception>
    /// <returns></returns>
    public ItemBuffComponent AddBuff(int type, int duration)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(type, nameof(type));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));

        Buffs.Add(new ItemBuffData(type, duration));

        return this;
    }
    
    /// <summary>
    ///     Sets the mode in which the buffs will be applied by the item.
    /// </summary>
    /// <param name="mode">The mode in which the buffs will be applied by the item.</param>
    /// <returns></returns>
    public ItemBuffComponent SetMode(ItemBuffMode mode)
    {
        Mode = mode;

        return this;
    }
}