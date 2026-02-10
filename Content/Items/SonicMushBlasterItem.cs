using CalamityAmmo.Projectiles;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Items.Buffs;
using Series.Common.Items.Guns;
using Series.Common.Items.Shooting;
using Series.Common.Items.Shooting.Patterns;
using Series.Core.Items;

namespace Series.Content.Items;

public class SonicMushBlasterItem : GunItemActor
{
    /// <summary>
    ///     The duration of the <see cref="BuffID.Slimed" /> debuff applied by projectiles shot by this
    ///     item, in frames.
    /// </summary>
    public const int SLIMED_DEBUFF_DURATION = 3 * 60;

    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.DamageType = DamageClass.Ranged;

        Item.SetWeaponValues(32, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 46;
        Item.height = 34;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 30f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.EnableComponent<ItemBuffComponent>().AddBuff(BuffID.Slimed, SLIMED_DEBUFF_DURATION);

        Item.EnableComponent<ItemShootComponent>()
            .AddShootModifier(new MuzzleOffsetModifier(25f))
            .AddShootModifier(new TypeConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_PearlBullet>()))
            .AddShootPattern(new IntervalShootPattern(5).AddShootModifier(new TypeModifier(ModContent.ProjectileType<FungiOrb>())));

        Item.EnableComponent<ItemBurstShootComponent>().SetBursts(2);
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<DoubleDarkBlasterItem>()
            .AddIngredient<Fungicide>()
            .AddIngredient(ItemID.GlowingMushroom, 20)
            .AddIngredient<EnergyCore>()
            .AddTile(TileID.Anvils)
            .Register();
    }
}