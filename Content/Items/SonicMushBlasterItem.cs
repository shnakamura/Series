using CalamityAmmo.Projectiles;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Shooting;
using Series.Common.Shooting.Modifiers;
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

        Item.GetComponent<ItemDebuffDataComponent>().Add(BuffID.Slimed, SLIMED_DEBUFF_DURATION);

        Item.GetComponent<ItemShootComponent>()
            .AddShootModifier(new MuzzleOffsetModifier(25f))
            .AddShootModifier(new TypeConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_PearlBullet>()));
        
        Item.GetComponent<ItemBurstShootingComponent>().Set(2);
        Item.GetComponent<ItemIntervalShootingComponent>().Set(5, ModContent.ProjectileType<FungiOrb>());
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