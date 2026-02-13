namespace Series.Common.Items.Buffs;

public readonly struct ItemBuffApplicationData : IItemBuffApplicationData
{
    public int Type { get; }
    
    public int Duration { get; }

    public ItemBuffApplicationData(int type, int duration)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(type, nameof(type));
        
        Type = type;
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));
        
        Duration = duration;
    }
}

public readonly struct ItemBuffApplicationData<T> : IItemBuffApplicationData where T : ModBuff
{
    public int Type => ModContent.BuffType<T>();

    public int Duration { get; }

    public ItemBuffApplicationData(int duration)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));
        
        Duration = duration;
    }
}