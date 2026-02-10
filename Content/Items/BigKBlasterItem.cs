using Series.Common.Items.Guns;

namespace Series.Content.Items;

public class BigKBlasterItem : GunItemActor
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.DamageType = DamageClass.Ranged;

        Item.SetWeaponValues(145, 6.5f, 20);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 92;
        Item.height = 34;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 40f;
        Item.shoot = ProjectileID.Bullet;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(16f, 0f);
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<BlastforgeBlasterItem>()
            .AddIngredient(ItemID.RangerEmblem)
            .AddIngredient(ItemID.SoulofNight, 5)
            .AddIngredient(ItemID.SoulofLight, 5)
            .AddTile(TileID.Anvils)
            .Register();
    }
}