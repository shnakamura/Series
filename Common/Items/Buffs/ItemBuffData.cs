using System.Collections.Generic;
using Series.Core.Items;

namespace Series.Common.Items.Buffs;

public sealed class ItemBuffData : ItemComponent
{
    /// <summary>
    ///     Gets the list of buffs to be applied by the item.
    /// </summary>
    public List<ItemBuffEntry> Buffs { get; private set; } = new();

    /// <summary>
    ///     Gets the mode in which the buffs will be applied by the item.
    /// </summary>
    public ItemBuffMode Mode { get; private set; } = ItemBuffMode.All;

    public override GlobalItem Clone(Item from, Item to)
    {
        var original = base.Clone(from, to);
        
        if (original is not ItemBuffData clone)
        {
            return original;
        }

        clone.Buffs = Buffs;
        clone.Mode = Mode;
        
        return clone;
    }

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
    public ItemBuffData AddBuff(int type, int duration)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(type, nameof(type));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));

        Buffs.Add(new ItemBuffEntry(type, duration));

        return this;
    }
    
    /// <summary>
    ///     Sets the mode in which the buffs will be applied by the item.
    /// </summary>
    /// <param name="mode">The mode in which the buffs will be applied by the item.</param>
    /// <returns></returns>
    public ItemBuffData SetMode(ItemBuffMode mode)
    {
        Mode = mode;

        return this;
    }
}