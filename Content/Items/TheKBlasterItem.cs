using Series.Common.Items.Guns;

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

        Item.shootSpeed = 15f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

        var offset = Vector2.Normalize(velocity) * 25f;

        if (!Collision.CanHit(position, 0, 0, position + offset, 0, 0))
        {
            return;
        }

        position += offset;
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