using System.Collections.Generic;
using Series.Core.Items;

namespace Series.Common.Items.Buffs;

public sealed class ItemBuffData : ItemComponent
{
    /// <summary>
    ///     Gets the list of buffs to be applied by the item.
    /// </summary>
    public List<IItemBuffApplicationData> Buffs { get; private set; } = new();

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

    public ItemBuffData AddBuff(int type, int duration)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(type, nameof(type));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));

        Buffs.Add(new ItemBuffApplicationData(type, duration));

        return this;
    }
    
    public ItemBuffData AddBuff<T>(int duration) where T : ModBuff
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));

        Buffs.Add(new ItemBuffApplicationData<T>(duration));

        return this;
    }

    public ItemBuffData SetMode(ItemBuffMode mode)
    {
        Mode = mode;

        return this;
    }
}