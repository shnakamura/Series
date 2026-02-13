namespace Series.Common.Items.Minions;

public readonly struct ItemMinionSpawnData : IItemMinionSpawnData
{
    public int Type { get; }
    
    public ItemMinionSpawnData(int type)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(type, nameof(type));

        Type = type;
    }
}

public readonly struct ItemMinionSpawnData<T> : IItemMinionSpawnData where T : ModProjectile
{
    public int Type => ModContent.ProjectileType<T>();
}