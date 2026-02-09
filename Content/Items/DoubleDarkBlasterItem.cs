using CalamityAmmo.Projectiles;
using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Recipes;
using Series.Common.Shooting;
using Series.Common.Shooting.Modifiers;
using Series.Core.Items;

namespace Series.Content.Items;

public class DoubleDarkBlasterItem : GunItemActor
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

        Item.SetWeaponValues(28, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 44;
        Item.height = 36;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 30f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.Get<ItemDebuffDataComponent>().Add(BuffID.Slimed, SLIMED_DEBUFF_DURATION);

        Item.Get<ItemShootComponent>()
            .AddModifier(new MuzzleOffsetModifier(25f))
            .AddModifier(new ConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_PearlBullet>()));
        
        Item.Get<ItemBurstShootingComponent>().Set(2);
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<PearlescentBlasterItem>()
            .AddIngredient(ItemID.ObsidianHorseshoe)
            .AddRecipeGroup(RecipeGroupSystem.EvilBar, 2)
            .AddTile(TileID.Anvils)
            .Register();
    }
}