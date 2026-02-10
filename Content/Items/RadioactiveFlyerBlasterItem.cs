using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Shooting;
using Series.Common.Shooting.Modifiers;
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

        Item.GetComponent<ItemDebuffDataComponent>().Add(ModContent.BuffType<GalvanicCorrosion>(), GALVANIC_CORROSION_DEBUFF_DURATION);

        Item.GetComponent<ItemBurstShootingComponent>().Set(2);
        
        Item.GetComponent<ItemShootComponent>()
            .AddShootModifier(new MuzzleOffsetModifier(25f))
            .AddShootModifier(new TypeConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_BloodBullet>()));
        
        Item.GetComponent<ItemIntervalShootingComponent>().Set(5, ModContent.ProjectileType<FungiOrb>());
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