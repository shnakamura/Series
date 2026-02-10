namespace Series.Common.Items.Buffs;

public readonly struct ItemBuffData
{
    /// <summary>
    ///     Gets the type of the buff.
    /// </summary>
    public int Type { get; }
    
    /// <summary>
    ///     Gets the duration of the buff, in frames.
    /// </summary>
    public int Duration { get; }

    public ItemBuffData(int type, int duration)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(type, nameof(type));
        
        Type = type;
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));
        
        Duration = duration;
    }
}