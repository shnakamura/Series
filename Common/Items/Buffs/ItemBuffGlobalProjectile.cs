using System.Collections.Generic;
using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Common.Items.Buffs;

public sealed class ItemBuffGlobalProjectile : GlobalProjectile
{
    /// <summary>
    ///     Gets the list of buffs to be applied by the projectile.
    /// </summary>
    public List<ItemBuffData> Buffs { get; private set; }
    
    public override bool InstancePerEntity { get; } = true;

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        base.OnSpawn(projectile, source);
        
        if (source is not EntitySource_ItemUse_WithAmmo use || use.Item?.IsAir == true || !use.Item.TryGetComponent(out ItemBuffComponent component))
        {
            return;
        }

        Buffs = component.Buffs;
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(projectile, target, hit, damageDone);

        if (Buffs != null && Buffs.Count > 0)
        {
            return;
        }

        foreach (var entry in Buffs)
        {
            target.AddBuff(entry.Type, entry.Duration);
        }
    }

    public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
        base.OnHitPlayer(projectile, target, info);
        
        if (Buffs != null && Buffs.Count > 0)
        {
            return;
        }

        foreach (var entry in Buffs)
        {
            target.AddBuff(entry.Type, entry.Duration);
        }
    }
}