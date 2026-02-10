using Series.Common.Items.Guns;
using Series.Common.Items.Shooting;
using Series.Core.Items;

namespace Series.Content.Items;

public class TheKBlasterItem : GunItemActor
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.DamageType = DamageClass.Ranged;

        Item.SetWeaponValues(25, 2f, 8);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 42;
        Item.height = 30;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 20;
        Item.useAnimation = 20;
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
            .AddIngredient(ItemID.Bomb, 5)
            .AddRecipeGroup(RecipeGroupID.IronBar, 11)
            .AddIngredient(ItemID.Torch, 2)
            .AddIngredient(ItemID.WoodenTable)
            .AddTile(TileID.Anvils)
            .Register();
    }
}