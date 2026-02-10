using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Common.Items.Bounce;

public sealed class ItemBounceGlobalProjectile : GlobalProjectile
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

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        base.OnSpawn(projectile, source);
        
        if (source is not EntitySource_ItemUse_WithAmmo use || !use.Item.TryGetComponent(out ItemBounceComponent component))
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