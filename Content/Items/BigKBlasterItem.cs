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

        Item.shootSpeed = 30f;
        Item.shoot = ProjectileID.Bullet;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

        var offset = Vector2.Normalize(velocity) * 100f;

        if (!Collision.CanHit(position, 0, 0, position + offset, 0, 0))
        {
            return;
        }

        position += offset;
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