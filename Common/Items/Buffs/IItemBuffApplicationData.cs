namespace Series.Common.Items.Buffs;

public interface IItemBuffApplicationData
{
    /// <summary>
    ///     Gets the type of the buff to apply.
    /// </summary>
    int Type { get; }
    
    /// <summary>
    ///     Gets the duration of the buff to apply, in frames.
    /// </summary>
    int Duration { get; }
}