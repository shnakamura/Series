using System.Collections.Generic;
using Series.Core.Items;

namespace Series.Common.Debuffs;

public sealed class ItemDebuffDataComponent : ItemComponent
{
    public List<ItemDebuffData> DebuffData { get; private set; } = new();

    public override void Unload()
    {
        base.Unload();

        DebuffData?.Clear();
        DebuffData = null;
    }

    /// <summary>
    ///     
    /// </summary>
    /// <param name="debuffType"></param>
    /// <param name="debuffDuration"></param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="debuffType" /> or <paramref name="debuffDuration" /> is less than zero.
    /// </exception>
    public void Add(int debuffType, int debuffDuration)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(debuffType, nameof(debuffType));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(debuffDuration, nameof(debuffDuration));

        DebuffData.Add(new ItemDebuffData(debuffType, debuffDuration));
    }
}