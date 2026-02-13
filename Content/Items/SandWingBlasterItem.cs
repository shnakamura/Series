using Series.Common.Items.Guns;
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
            .AddIngredient<TheKBlasterItem>()
            .AddIngredient<StormHatchlingStaff>()
            .AddIngredient<SandstoneIngot>(7)
            .AddTile(TileID.Anvils)
            .Register();
    }
}