using Series.Core.Items;

namespace Series.Common.Items.Bounce;

public sealed class ItemBounceDataComponent : ItemComponent
{
    public int BounceAmount { get; private set; }
    
    public void Set(int bounceAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bounceAmount, nameof(bounceAmount));
        
        BounceAmount = bounceAmount;
    }
}