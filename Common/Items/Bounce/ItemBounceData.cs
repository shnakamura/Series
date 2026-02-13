using System.IO;
using Series.Core.Items;

namespace Series.Common.Items.Bounce;

public sealed class ItemBounceData : ItemComponent
{
    /// <summary>
    ///     Gets the amount of times a projectile shot from this item can bounce.
    /// </summary>
    public int BounceAmount { get; private set; }

    /// <summary>
    ///     Gets the multiplier applied to the velocity of the projectile shot from this item after bouncing.
    /// </summary>
    public float BounceMultiplier { get; private set; } = 0.75f;

    public override GlobalItem Clone(Item from, Item to)
    {
        var original = base.Clone(from, to);

        if (original is not ItemBounceData clone)
        {
            return original;
        }

        clone.BounceAmount = BounceAmount;
        clone.BounceMultiplier = BounceMultiplier;

        return clone;
    }

    public override void NetSend(Item item, BinaryWriter writer)
    {
        base.NetSend(item, writer);
        
        writer.Write(BounceAmount);
        writer.Write(BounceMultiplier);
    }

    public override void NetReceive(Item item, BinaryReader reader)
    {
        base.NetReceive(item, reader);
        
        BounceAmount = reader.ReadInt32();
        BounceMultiplier = reader.ReadSingle();
    }

    ///     Sets the amount of times a projectile shot from this item can bounce.
    /// </summary>
    /// <param name="bounceAmount">The amount of times a projectile shot from this item can bounce.</param>
    /// <returns></returns>
    public ItemBounceData SetBounces(int bounceAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bounceAmount, nameof(bounceAmount));
        
        BounceAmount = bounceAmount;

        return this;
    }
    
    /// <summary>
    ///     Sets the multiplier applied to the velocity of the projectile shot from this item after bouncing.
    /// </summary>
    /// <param name="bounceMultiplier">The multiplier applied to the velocity of the projectile shot from this item after bouncing.</param>
    /// <returns></returns>
    public ItemBounceData SetMultiplier(float bounceMultiplier)
    {
        BounceMultiplier = bounceMultiplier;

        return this;
    }
}