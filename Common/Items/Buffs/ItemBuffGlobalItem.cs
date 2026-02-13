using Series.Core.Items;

namespace Series.Common.Items.Buffs;

public sealed class ItemBuffGlobalItem : GlobalItem
{
    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(item, player, target, hit, damageDone);
        
        if (!item.TryGetComponent(out ItemBuffData component) || (component.Mode & ItemBuffMode.Contact) == 0)
        {
            return;
        }
        
        foreach (var buff in component.Buffs)
        {
            target.AddBuff(buff.Type, buff.Duration);
        }
    }

    public override void OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo)
    {
        base.OnHitPvp(item, player, target, hurtInfo);
        
        if (!item.TryGetComponent(out ItemBuffData component) || (component.Mode & ItemBuffMode.Contact) == 0)
        {
            return;
        }
        
        foreach (var buff in component.Buffs)
        {
            target.AddBuff(buff.Type, buff.Duration);
        }
    }
}