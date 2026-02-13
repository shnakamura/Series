using System.Collections.Generic;
using Series.Core.Items;

namespace Series.Common.Items.Minions;

public sealed class ItemMinionData : ItemComponent
{
    public List<IItemMinionSpawnData> Minions { get; private set; } = new();

    public override GlobalItem Clone(Item from, Item to)
    {
        var original = base.Clone(from, to);

        if (original is not ItemMinionData clone)
        {
            return original;
        }

        clone.Minions = Minions;

        return clone;
    }
}