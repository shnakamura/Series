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
    
    public ItemMinionData AddMinion(int type, int amount = 1)
    {
        Minions.Add(new ItemMinionSpawnData(type, amount));

        return this;
    }

    public ItemMinionData AddMinion<T>(int amount = 1) where T : ModProjectile
    {
        Minions.Add(new ItemMinionSpawnData<T>(amount));

        return this;
    }
    
    public bool HasMinion(int type)
    {
        foreach (var minion in Minions)
        {
            if (minion.Type == type)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool HasMinion<T>() where T : ModProjectile
    {
        foreach (var minion in Minions)
        {
            if (minion.Type == ModContent.ProjectileType<T>())
            {
                return true;
            }
        }

        return false;
    }
}