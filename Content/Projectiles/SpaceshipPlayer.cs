using Series.Content.Items;
using Series.Utilities;

namespace Series.Content.Projectiles;

public sealed class SpaceshipPlayer : ModPlayer
{
    public static int[] ItemTypes =>
    [
        ModContent.ItemType<RadioactiveFlyerBlasterItem>(),
        ModContent.ItemType<SuperSlimedBlasterItem>(),
        ModContent.ItemType<BloodBlasterItem>(),
        ModContent.ItemType<BeeBlasterItem>(),
        ModContent.ItemType<BuriedBlasterItem>(),
        ModContent.ItemType<AstroDuoBlasterItem>(),
        ModContent.ItemType<BonerBlasterItem>(),
        ModContent.ItemType<BlastforgeBlasterItem>(),
    ];
    
    public override void PostUpdate()
    {
        base.PostUpdate();

        var active = false;
        
        foreach (var itemType in ItemTypes)
        {
            if (Player.HeldItem.type == itemType)
            {
                active = true;
            }
        }
        
        if (!active)
        {
            return;
        }
        
        if (!ProjectileUtilities.Exists<LeftSpaceshipProjectile>())
        {
            Projectile.NewProjectile(Player.GetSource_ItemUse(Player.HeldItem), Player.Center, Vector2.Zero, ModContent.ProjectileType<LeftSpaceshipProjectile>(), 0, 0, Player.whoAmI);
        }
        
        if (!ProjectileUtilities.Exists<RightSpaceshipProjectile>())
        {
            Projectile.NewProjectile(Player.GetSource_ItemUse(Player.HeldItem), Player.Center, Vector2.Zero, ModContent.ProjectileType<RightSpaceshipProjectile>(), 0, 0, Player.whoAmI);
        }
    }
}