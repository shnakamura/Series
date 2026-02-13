namespace Series.Common.Items.Minions;

public readonly struct ItemMinionSpawnData : IItemMinionSpawnData
{
    public int Type { get; }
}

public readonly struct ItemMinionSpawnData<T> : IItemMinionSpawnData where T : ModProjectile
{
    public int Type => ModContent.ProjectileType<T>();
}