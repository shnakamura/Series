using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Shooting;
using Series.Core.Items;

namespace Series.Content.Items;

public class BonerBlasterItem : GunItemActor
{
    /// <summary>
    ///     The duration of the <see cref="GalvanicCorrosion" /> debuff applied by projectiles shot by this
    ///     item, in frames.
    /// </summary>
    public const int GALVANIC_CORROSION_DEBUFF_DURATION = 3 * 60;

    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.DamageType = DamageClass.Ranged;

        Item.SetWeaponValues(21, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 44;
        Item.height = 36;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 35f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Orange;

        Item.Get<ItemDebuffDataComponent>().Add(ModContent.BuffType<GalvanicCorrosion>(), GALVANIC_CORROSION_DEBUFF_DURATION);

        Item.Get<ItemBurstShootingComponent>().Set(3);
        Item.Get<ItemMuzzleShootingComponent>().Set(25f);
        Item.Get<ItemIntervalShootingComponent>().Set(7, ProjectileID.Bee, 3);
        Item.Get<ItemShootingConversionComponent>().Set(ProjectileID.Bullet, ModContent.ProjectileType<_BloodBullet>());
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<BeeBlasterItem>()
            .AddIngredient(ItemID.Bone, 27)
            .AddIngredient(ItemID.HellstoneBar, 8)
            .AddIngredient(ItemID.Timer5Second)
            .AddIngredient(ItemID.Minishark)
            .AddTile(TileID.Anvils)
            .Register();
    }
}