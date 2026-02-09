using CalamityAmmo.Projectiles;
using CalamityMod.Items.Materials;
using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Shooting;
using Series.Common.Shooting.Modifiers;
using Series.Core.Items;

namespace Series.Content.Items;

public class PearlescentBlasterItem : GunItemActor
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

        Item.SetWeaponValues(36, 2.5f, 20);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 44;
        Item.height = 34;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 25f;

        Item.rare = ItemRarityID.Green;

        Item.Get<ItemDebuffDataComponent>().Add(BuffID.Slimed, SLIMED_DEBUFF_DURATION);

        Item.Get<ItemShootComponent>()
            .AddModifier(new MuzzleOffsetModifier(25f))
            .AddModifier(new ConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_PearlBullet>()));
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<SlimedShinobiBlasterItem>()
            .AddIngredient<PearlShard>(12)
            .AddTile(TileID.Anvils)
            .Register();
    }
}