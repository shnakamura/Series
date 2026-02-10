using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Items.Buffs;
using Series.Common.Items.Guns;
using Series.Common.Items.Shooting;
using Series.Common.Items.Shooting.Patterns;
using Series.Core.Items;

namespace Series.Content.Items;

public class BeeBlasterItem : GunItemActor
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

        Item.SetWeaponValues(36, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 44;
        Item.height = 36;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 35f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Orange;

        Item.EnableComponent<ItemBuffComponent>().AddBuff(ModContent.BuffType<GalvanicCorrosion>(), GALVANIC_CORROSION_DEBUFF_DURATION);

        Item.EnableComponent<ItemBurstShootComponent>().SetBursts(2);

        Item.EnableComponent<ItemShootComponent>()
            .AddShootModifier(new MuzzleOffsetModifier(25f))
            .AddShootModifier(new TypeConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_BloodBullet>()))
            .AddShootPattern(new IntervalShootPattern(7).AddShootModifier(new TypeModifier(ProjectileID.Bee)))
            .AddShootPattern(new IntervalShootPattern(7).AddShootModifier(new TypeModifier(ProjectileID.Bee)))
            .AddShootPattern(new IntervalShootPattern(7).AddShootModifier(new TypeModifier(ProjectileID.Bee)));
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<RadioactiveFlyerBlasterItem>()
            .AddIngredient(ItemID.BeeGun)
            .AddIngredient(ItemID.BeeWax, 11)
            .AddTile(TileID.Anvils)
            .Register();
    }
}