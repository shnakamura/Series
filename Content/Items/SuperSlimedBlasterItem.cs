using CalamityAmmo.Projectiles;
using CalamityMod.Items.Materials;
using Series.Common.Items.Bounce;
using Series.Common.Items.Buffs;
using Series.Common.Items.Bursts;
using Series.Common.Items.Guns;
using Series.Content.Projectiles;
using Series.Core.Items;
using Terraria.DataStructures;
using ThoriumMod.Buffs;

namespace Series.Content.Items;

public class SuperSlimedBlasterItem : GunItemActor
{
    /// <summary>
    ///     The duration of the <see cref="GraniteSurge" /> debuff applied by projectiles shot by this
    ///     item, in frames.
    /// </summary>
    public const int GRANITE_SURGE_DEBUFF_DURATION = 3 * 60;

    public const int BEE_SHOOT_INTERVAL = 7;
    
    public const int BEE_SHOOT_AMOUNT = 3;

    public int Counter { get; private set; }
    
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
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 25f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Orange;
        
        Item.EnableComponent<ItemBounceData>().SetBounces(2);
        Item.EnableComponent<ItemBurstData>().SetBursts(3);
        
        Item.EnableComponent<ItemBuffData>().AddBuff(ModContent.BuffType<GraniteSurge>(), GRANITE_SURGE_DEBUFF_DURATION);
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

        var offset = Vector2.Normalize(velocity) * 25f;

        if (Collision.CanHit(position, 0, 0, position + offset, 0, 0))
        {
            position += offset;
        }

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

        if (Counter < BEE_SHOOT_INTERVAL)
        {
            return true;
        }

        for (var i = 0; i < BEE_SHOOT_AMOUNT; i++)
        {
            var beeVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 0.4f);

            var projectile = Projectile.NewProjectileDirect
            (
                source,
                position,
                beeVelocity,
                ProjectileID.Bee,
                damage,
                knockback,
                player.whoAmI
            );

            projectile.hostile = false;
            projectile.friendly = true;
        }

        Counter = 0;

        return true;
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