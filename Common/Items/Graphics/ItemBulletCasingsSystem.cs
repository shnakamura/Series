using Series.Core.Items;
using Terraria.GameContent;

namespace Series.Common.Items.Graphics;

public sealed class ItemBulletCasingsSystem : GlobalItem
{
    public override bool? UseItem(Item item, Player player)
    {
        if (!item.TryGetComponent(out ItemBulletCasingsData data))
        {
            return null;
        }

        var type = data.CasingType;
        var amount = data.CasingAmount;
        
        if (type < 0 || amount <= 0)
        {
            return null;
        }

        var texture = TextureAssets.Gore[type].Value;
        var position = player.Center + new Vector2(12f * player.direction, -6f);

        if (player.direction == -1)
        {
            position -= texture.Size() / 2f + new Vector2(12f, 0f);
        }

        for (var i = 0; i < amount; i++)
        {
            var velocity = new Vector2(Main.rand.NextFloat(0.75f, 1f) * -player.direction, -Main.rand.NextFloat(1f, 1.5f)) + player.velocity * 0.5f;

            Gore.NewGore(player.GetSource_ItemUse(item), position, velocity, type);
        }

        return null;
    }
}