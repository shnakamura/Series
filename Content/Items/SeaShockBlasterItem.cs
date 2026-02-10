using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Items.Buffs;
using Series.Common.Items.Guns;
using Series.Common.Items.Shooting;
using Series.Core.Items;
using ThoriumMod.Items.NPCItems;

namespace Series.Content.Items;

public class SeaShockBlasterItem : GunItemActor
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

        Item.SetWeaponValues(32, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 44;
        Item.height = 32;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 30f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.EnableComponent<ItemBuffComponent>().AddBuff(ModContent.BuffType<GalvanicCorrosion>(), GALVANIC_CORROSION_DEBUFF_DURATION);

        Item.EnableComponent<ItemShootComponent>()
            .AddShootModifier(new MuzzleOffsetModifier(25f))
            .AddShootModifier(new TypeConversionModifier(ProjectileID.Bullet, ModContent.ProjectileType<_PearlBullet>()));
        
        Item.EnableComponent<ItemBurstShootComponent>().SetBursts(2);
        Item.EnableComponent<ItemIntervalShootingComponent>().Set(5, ModContent.ProjectileType<FungiOrb>());
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<SonicMushBlasterItem>()
            .AddIngredient<MarineLauncher>()
            .AddIngredient<SeaRemains>(8)
            .AddIngredient<SeaPrism>(20)
            .AddTile(TileID.Anvils)
            .Register();
    }
}