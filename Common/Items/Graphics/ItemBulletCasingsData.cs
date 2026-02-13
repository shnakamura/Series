using Series.Core.Items;
using Terraria.GameContent;

namespace Series.Common.Items.Graphics;

public sealed class ItemBulletCasingsData : ItemComponent
{
    /// <summary>
    ///     Gets the type of the casing to spawn. Defaults to <c>-1</c>, which means no casings will be
    ///     spawned.
    /// </summary>
    public int CasingType { get; private set; } = -1;

    /// <summary>
    ///     Gets the amount of casings to spawn. Defaults to <c>0</c>, which means no casings will be
    ///     spawned.
    /// </summary>
    public int CasingAmount { get; private set; }

    public override GlobalItem Clone(Item from, Item to)
    {
        var original = base.Clone(from, to);
        
        if (original is not ItemBulletCasingsData clone)
        {
            return original;
        }
        
        clone.CasingType = CasingType;
        clone.CasingAmount = CasingAmount;

        return clone;
    }

    public ItemBulletCasingsData SetCasingType(int casingType)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(casingType, nameof(casingType));

        CasingType = casingType;

        return this;
    }
    
    public ItemBulletCasingsData SetCasingType<T>() where T : ModGore
    {
        CasingType = ModContent.GoreType<T>();

        return this;
    }
    
    public ItemBulletCasingsData SetCasingAmount(int casingAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(casingAmount, nameof(casingAmount));

        CasingAmount = casingAmount;

        return this;
    }
}