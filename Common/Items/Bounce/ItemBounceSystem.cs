using System.IO;
using Series.Core.Items;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Series.Common.Items.Bounce;

public sealed class ItemBounceSystem : GlobalProjectile
{
    /// <summary>
    ///     Gets the amount of times the projectile can bounce.
    /// </summary>
    public int Bounces { get; private set; }
    
    /// <summary>
    ///     Gets the multiplier applied to the projectile's velocity after bouncing.
    /// </summary>
    public float Multiplier { get; private set; }
    
    public override bool InstancePerEntity { get; } = true;

    public override GlobalProjectile Clone(Projectile from, Projectile to)
    {
        var original = base.Clone(from, to);

        if (original is not ItemBounceSystem clone)
        {
            return original;
        }

        clone.Bounces = Bounces;
        clone.Multiplier = Multiplier;

        return clone;
    }
    
    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
        base.SendExtraAI(projectile, bitWriter, binaryWriter);
        
        binaryWriter.Write(Bounces);
        binaryWriter.Write(Multiplier);
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
    {
        base.ReceiveExtraAI(projectile, bitReader, binaryReader);

        Bounces = binaryReader.Read7BitEncodedInt();
        Multiplier = binaryReader.ReadSingle();
    }

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        base.OnSpawn(projectile, source);
        
        if (source is not EntitySource_ItemUse_WithAmmo use || !use.Item.TryGetComponent(out ItemBounceData component))
        {
            return;
        }

        Bounces = component.Bounces;
        Multiplier = component.Multiplier;
    }

    public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
    {
        if (Bounces <= 0)
        {
            return true;
        }
        
        Bounces--;
        
        projectile.velocity = -projectile.velocity * Multiplier;

        return false;
    }
}