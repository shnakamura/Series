using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Common.Shooting;

public sealed class ItemBounceGlobalProjectile : GlobalProjectile
{
    public int BounceAmount { get; private set; }
    
    public override bool InstancePerEntity { get; } = true;

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        base.OnSpawn(projectile, source);
        
        if (source is not EntitySource_ItemUse_WithAmmo use || use.Item?.IsAir == true || !use.Item.TryGet(out ItemBounceDataComponent component))
        {
            return;
        }

        BounceAmount = component.BounceAmount;
    }

    public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
    {
        if (BounceAmount <= 0)
        {
            return true;
        }
        
        BounceAmount--;
        
        projectile.velocity = -projectile.velocity * 0.75f;

        return false;
    }
}