using Series.Common.Graphics;
using Series.Content.Gores;
using Series.Core.Items;

namespace Series.Common.Guns;

public abstract class GunItemActor : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.Enable<ItemShootAnimationComponent>();
        
        Item.Get<ItemBulletCasingsComponent>().Set(ModContent.GoreType<BulletCasingGore>(), 1);
    }
    
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(12f, 0f);
    }
}