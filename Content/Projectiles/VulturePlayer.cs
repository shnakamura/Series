using Series.Content.Items;
using Series.Utilities;

namespace Series.Content.Projectiles;

public sealed class VulturePlayer : ModPlayer
{
    public static int[] ItemTypes =>
    [
        ModContent.ItemType<SandWingBlasterItem>(),
        ModContent.ItemType<SlimedShinobiBlasterItem>(),
        ModContent.ItemType<PearlescentBlasterItem>(),
        ModContent.ItemType<DoubleDarkBlasterItem>(),
        ModContent.ItemType<SonicMushBlasterItem>(),
        ModContent.ItemType<SeaShockBlasterItem>()
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
        
        if (!active || ProjectileUtilities.Exists<VultureProjectile>())
        {
            return;
        }
        
        Projectile.NewProjectile(Player.GetSource_ItemUse(Player.HeldItem), Player.Center, Vector2.Zero, ModContent.ProjectileType<VultureProjectile>(), 0, 0, Player.whoAmI);
    }
}