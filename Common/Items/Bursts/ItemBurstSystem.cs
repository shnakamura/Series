using Series.Core.Items;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Series.Common.Items.Bursts;

public sealed class ItemBurstSystem : GlobalItem
{
    public override void SetDefaults(Item entity)
    {
        base.SetDefaults(entity);

        if (!entity.TryGetComponent(out ItemBurstData data))
        {
            return;
        }

        var amount = data.Amount;

        if (data.Amount < 0)
        {
            return;
        }

        var target = entity.useTime / amount;

        target = Math.Max(1, target);

        entity.useTime = target;
        entity.useAnimation = target * amount;

        entity.consumeAmmoOnLastShotOnly = true;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (item.TryGetComponent(out ItemBurstData data) && data.PlaySound && player.itemAnimation != player.itemAnimationMax && item.UseSound.HasValue)
        {
            SoundEngine.PlaySound(item.UseSound, position);
        }

        return true;
    }
}