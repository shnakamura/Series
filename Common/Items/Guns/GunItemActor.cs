using Series.Common.Items.Graphics;
using Series.Content.Gores;
using Series.Core.Items;

namespace Series.Common.Items.Guns;

public abstract class GunItemActor : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        
        Item.EnableComponent<ItemBulletCasingsData>()
            .SetCasingType<BulletCasingGore>()
            .SetCasingAmount(1);
        
        Item.EnableComponent<ItemShootAnimationData>();
    }
    
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(12f, 0f);
    }
}