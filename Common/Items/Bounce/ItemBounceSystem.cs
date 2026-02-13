using System.IO;
using Series.Core.Items;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Series.Common.Items.Bounce;

public sealed class ItemBounceSystem : GlobalProjectile
{
    public bool Enabled { get; private set; }
    
    /// <summary>
    ///     Gets the amount of times the projectile can bounce.
    /// </summary>
    public int BounceAmount { get; private set; }
    
    /// <summary>
    ///     Gets the multiplier applied to the projectile's velocity after bouncing.
    /// </summary>
    public float BounceMultiplier { get; private set; }
    
    public override bool InstancePerEntity { get; } = true;

    public override GlobalProjectile Clone(Projectile from, Projectile to)
    {
        var original = base.Clone(from, to);

        if (original is not ItemBounceSystem clone)
        {
            return original;
        }

        clone.Enabled = Enabled;
        
        clone.BounceAmount = BounceAmount;
        clone.BounceMultiplier = BounceMultiplier;

        return clone;
    }
    
    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        base.OnSpawn(projectile, source);
        
        if (source is not EntitySource_ItemUse_WithAmmo use || !use.Item.TryGetComponent(out ItemBounceData component))
        {
            return;
        }

        Enabled = true;

        BounceAmount = component.BounceAmount;
        BounceMultiplier = component.BounceMultiplier;
    }
    
    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
        base.SendExtraAI(projectile, bitWriter, binaryWriter);

        if (!Enabled)
        {
            return;
        }
        
        binaryWriter.Write(Enabled);
        
        binaryWriter.Write(BounceAmount);
        binaryWriter.Write(BounceMultiplier);
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
    {
        base.ReceiveExtraAI(projectile, bitReader, binaryReader);

        if (!Enabled)
        {
            return;
        }

        Enabled = binaryReader.ReadBoolean();
        
        BounceAmount = binaryReader.ReadInt32();
        BounceMultiplier = binaryReader.ReadSingle();
    }

    public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
    {
        if (!Enabled || BounceAmount <= 0)
        {
            return true;
        }
        
        BounceAmount--;
        
        projectile.velocity = -projectile.velocity * BounceMultiplier;

        return false;
    }
}