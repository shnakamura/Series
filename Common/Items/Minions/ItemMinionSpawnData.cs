namespace Series.Common.Items.Minions;

public readonly struct ItemMinionSpawnData : IItemMinionSpawnData
{
    public int Type { get; }
    
    public int Amount { get; }
    
    public ItemMinionSpawnData(int type, int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(type, nameof(type));

        Type = type;
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, nameof(amount));

        Amount = amount;
    }
}

public readonly struct ItemMinionSpawnData<T> : IItemMinionSpawnData where T : ModProjectile
{
    public int Type => ModContent.ProjectileType<T>();

    public int Amount { get; }
    
    public ItemMinionSpawnData(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, nameof(amount));

        Amount = amount;
    }
}