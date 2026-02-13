using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Items.Buffs;
using Series.Common.Items.Bursts;
using Series.Common.Items.Guns;
using Series.Content.Projectiles;
using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Content.Items;

public class RadioactiveFlyerBlasterItem : GunItemActor
{
    /// <summary>
    ///     The duration of the <see cref="GalvanicCorrosion" /> debuff applied by projectiles shot by this
    ///     item, in frames.
    /// </summary>
    public const int GALVANIC_CORROSION_DEBUFF_DURATION = 3 * 60;

    public const int FUNGAL_ROUND_SHOOT_INTERVAL = 5;

    public int Counter { get; private set; }

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

        Item.shootSpeed = 25f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Orange;

        Item.EnableComponent<ItemBurstData>().SetBursts(2);

        Item.EnableComponent<ItemBuffData>().AddBuff(ModContent.BuffType<GalvanicCorrosion>(), GALVANIC_CORROSION_DEBUFF_DURATION);
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

        if (type != ProjectileID.Bullet)
        {
            return;
        }

        type = ModContent.ProjectileType<_BloodBullet>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var leftPosition = position + new Vector2(-(8f * 16f), -(32f * 16f));
        var leftVelocity = leftPosition.DirectionTo(Main.MouseWorld) * velocity.Length();

        Projectile.NewProjectile
        (
            source,
            leftPosition,
            leftVelocity,
            ModContent.ProjectileType<AcidFeatherProjectile>(),
            damage,
            knockback,
            player.whoAmI
        );

        var rightPosition = position + new Vector2(4f * 16f, -(32f * 16f));
        var rightVelocity = rightPosition.DirectionTo(Main.MouseWorld) * velocity.Length();

        Projectile.NewProjectile
        (
            source,
            rightPosition,
            rightVelocity,
            ModContent.ProjectileType<AcidFeatherProjectile>(),
            damage,
            knockback,
            player.whoAmI
        );

        Counter++;

        if (Counter < FUNGAL_ROUND_SHOOT_INTERVAL)
        {
            return true;
        }

        var projectile = Projectile.NewProjectileDirect
        (
            source,
            position,
            velocity,
            ModContent.ProjectileType<FungiOrb>(),
            damage,
            knockback,
            player.whoAmI
        );

        projectile.friendly = true;
        projectile.hostile = false;

        Counter = 0;

        return true;
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<BloodBlasterItem>()
            .AddIngredient<FeatherCrown>()
            .AddIngredient<SulphuricScale>(6)
            .AddIngredient(ItemID.SunplateBlock, 5)
            .AddTile(TileID.Anvils)
            .Register();
    }
}