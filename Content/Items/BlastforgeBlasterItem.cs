using CalamityAmmo.Projectiles;
using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Shooting;
using Series.Common.Shooting.Modifiers;
using Series.Core.Items;
using SOTS.Items.AbandonedVillage;
using SOTS.Items.Earth;
using SOTS.Projectiles.AbandonedVillage;
using ThoriumMod.Buffs;

namespace Series.Content.Items;

public class BlastforgeBlasterItem : GunItemActor
{
    /// <summary>
    ///     The duration of the <see cref="GraniteSurge" /> debuff applied by projectiles shot by this
    ///     item, in frames.
    /// </summary>
    public const int GRANITE_SURGE_DEBUFF_DURATION = 3 * 60;

    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.DamageType = DamageClass.Ranged;

        Item.SetWeaponValues(33, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 48;
        Item.height = 38;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 40f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Orange;

        Item.GetComponent<ItemDebuffDataComponent>().Add(ModContent.BuffType<GraniteSurge>(), GRANITE_SURGE_DEBUFF_DURATION);

        Item.GetComponent<ItemBurstShootingComponent>().Set(3);
        Item.GetComponent<ItemBounceDataComponent>().Set(2);
        
        Item.GetComponent<ItemShootComponent>()
            .AddShootModifier(new MuzzleOffsetModifier(25f))
            .AddShootModifier(new TypeConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_BloodBullet>()));
        
        Item.GetComponent<ItemIntervalShootingComponent>().Set(6, ModContent.ProjectileType<ExcavatorRocket>());
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<SuperSlimedBlasterItem>()
            .AddIngredient<ExcavatorBreastplate>()
            .AddIngredient(ItemID.ApprenticeScarf)
            .AddIngredient<VibrantBar>(12)
            .AddTile(TileID.Anvils)
            .Register();
    }
}