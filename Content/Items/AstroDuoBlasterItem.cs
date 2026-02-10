using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Recipes;
using Series.Common.Shooting;
using Series.Common.Shooting.Modifiers;
using Series.Core.Items;

namespace Series.Content.Items;

public class AstroDuoBlasterItem : GunItemActor
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

        Item.width = 46;
        Item.height = 34;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 35f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.EnableComponent<ItemDebuffDataComponent>().Add(ModContent.BuffType<GalvanicCorrosion>(), GALVANIC_CORROSION_DEBUFF_DURATION);

        Item.EnableComponent<ItemBurstShootingComponent>().Set(2);

        Item.EnableComponent<ItemShootComponent>()
            .AddShootModifier(new MuzzleOffsetModifier(25f))
            .AddShootModifier(new TypeConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_PearlBullet>()));
        
        Item.EnableComponent<ItemIntervalShootingComponent>().Set(5, ModContent.ProjectileType<FungiOrb>());
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<SeaShockBlasterItem>()
            .AddIngredient(ItemID.ObsidianShirt)
            .AddIngredient(ItemID.MeteoriteBar, 14)
            .AddIngredient(ItemID.FallenStar, 4)
            .AddRecipeGroup(RecipeGroupSystem.TungstenBar, 9)
            .AddTile(TileID.Anvils)
            .Register();
    }
}