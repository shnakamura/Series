using Series.Common.Guns;
using Series.Common.Shooting;
using Series.Core.Items;
using ThoriumMod.Items.BossTheGrandThunderBird;
using ThoriumMod.Items.Sandstone;

namespace Series.Content.Items;

public class SandWingBlasterItem : GunItemActor
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.DamageType = DamageClass.Ranged;

        Item.SetWeaponValues(28, 2f, 8);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 44;
        Item.height = 38;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 50;
        Item.useAnimation = 50;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 25f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.EnableComponent<ItemShootComponent>().AddShootModifier(new MuzzleOffsetModifier(25f));
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<TheKBlasterItem>()
            .AddIngredient<StormHatchlingStaff>()
            .AddIngredient<SandstoneIngot>(7)
            .AddTile(TileID.Anvils)
            .Register();
    }
}