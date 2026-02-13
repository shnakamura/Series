namespace Series.Common.Items.Buffs;

public readonly struct ItemBuffEntry
{
    /// <summary>
    ///     Gets the type of the buff.
    /// </summary>
    public int Type { get; }
    
    /// <summary>
    ///     Gets the duration of the buff, in frames.
    /// </summary>
    public int Duration { get; }

    public ItemBuffEntry(int type, int duration)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(type, nameof(type));
        
        Type = type;
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));
        
        Duration = duration;
    }
}