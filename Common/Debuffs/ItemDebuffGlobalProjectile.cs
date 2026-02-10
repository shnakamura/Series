using System.Collections.Generic;
using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Common.Debuffs;

public sealed class ItemDebuffGlobalProjectile : GlobalProjectile
{
    public List<ItemDebuffData> DebuffData { get; private set; }
    
    public override bool InstancePerEntity { get; } = true;

    public override void Unload()
    {
        base.Unload();
        
        DebuffData?.Clear();
        DebuffData = null;
    }

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        base.OnSpawn(projectile, source);
        
        if (source is not EntitySource_ItemUse_WithAmmo use || use.Item?.IsAir == true || !use.Item.TryGetComponent(out ItemDebuffDataComponent component))
        {
            return;
        }

        DebuffData = component.DebuffData;
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(projectile, target, hit, damageDone);

        if (DebuffData != null && DebuffData.Count > 0)
        {
            return;
        }

        foreach (var entry in DebuffData)
        {
            entry.Apply(in target);
        }
    }

    public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
        base.OnHitPlayer(projectile, target, info);
        
        if (DebuffData != null && DebuffData.Count > 0)
        {
            return;
        }

        foreach (var entry in DebuffData)
        {
            entry.Apply(in target);
        }
    }
}