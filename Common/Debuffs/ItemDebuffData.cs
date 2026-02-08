namespace Series.Common.Debuffs;

public readonly struct ItemDebuffData
{
    /// <summary>
    ///     Gets the type of the debuff.
    /// </summary>
    public int DebuffType { get; }
    
    /// <summary>
    ///     Gets the duration of the debuff, in frames.
    /// </summary>
    public int DebuffDuration { get; }

    public ItemDebuffData(int debuffType, int debuffDuration)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(debuffType, nameof(debuffType));
        
        DebuffType = debuffType;
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(debuffDuration, nameof(debuffDuration));
        
        DebuffDuration = debuffDuration;
    }

    /// <summary>
    ///     Applies the debuff to the given <see cref="NPC"/> instance.
    /// </summary>
    /// <param name="npc">The <see cref="NPC"/> instance to apply the debuff onto.</param>
    public void Apply(in NPC npc)
    {
        npc.AddBuff(DebuffType, DebuffDuration);
    }
    
    /// <summary>
    ///     Applies the debuff to the given <see cref="Player"/> instance.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> instance to apply the debuff onto.</param>
    public void Apply(in Player player)
    {
        player.AddBuff(DebuffType, DebuffDuration);
    }
}