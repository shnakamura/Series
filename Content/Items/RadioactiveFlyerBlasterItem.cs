using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Items.Buffs;
using Series.Common.Items.Guns;
using Series.Common.Items.Shooting;
using Series.Common.Items.Shooting.Patterns;
using Series.Core.Items;

namespace Series.Content.Items;

public class RadioactiveFlyerBlasterItem : GunItemActor
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

        Item.SetWeaponValues(30, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 48;
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
            .AddShootPattern(new IntervalShootPattern(5).AddShootModifier(new TypeModifier(ModContent.ProjectileType<FungiOrb>())));
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<BloodBlasterItem>()
            .AddIngredient<FeatherCrown>()
            .AddIngredient<ScionsCurio>()
            .AddIngredient(ItemID.SunplateBlock, 5)
            .AddTile(TileID.Anvils)
            .Register();
    }
}