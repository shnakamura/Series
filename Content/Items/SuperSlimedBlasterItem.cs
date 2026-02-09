using CalamityAmmo.Projectiles;
using CalamityMod.Items.Materials;
using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Shooting;
using Series.Common.Shooting.Modifiers;
using Series.Core.Items;
using ThoriumMod.Buffs;

namespace Series.Content.Items;

public class SuperSlimedBlasterItem : GunItemActor
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

        Item.SetWeaponValues(30, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 42;
        Item.height = 34;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 35f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Orange;

        Item.Get<ItemDebuffDataComponent>().Add(ModContent.BuffType<GraniteSurge>(), GRANITE_SURGE_DEBUFF_DURATION);

        Item.Get<ItemShootComponent>()
            .AddModifier(new MuzzleOffsetModifier(25f))
            .AddModifier(new ConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_BloodBullet>()));
        
        Item.Get<ItemBurstShootingComponent>().Set(3);
        Item.Get<ItemBounceDataComponent>().Set(2);
        Item.Get<ItemIntervalShootingComponent>().Set(7, ProjectileID.Bee, 3);
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<BuriedBlasterItem>()
            .AddIngredient<PurifiedGel>(12)
            .AddIngredient<BlightedGel>(12)
            .AddTile(TileID.Anvils)
            .Register();
    }
}