using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using Series.Common.Items.Buffs;
using Series.Common.Items.Bursts;
using Series.Common.Items.Guns;
using Series.Common.Items.Minions;
using Series.Content.Projectiles;
using Series.Core.Items;
using Terraria.DataStructures;

namespace Series.Content.Items;

public class BeeBlasterItem : GunItemActor
{
    /// <summary>
    ///     The duration of the <see cref="GalvanicCorrosion" /> debuff applied by projectiles shot by this
    ///     item, in frames.
    /// </summary>
    public const int GALVANIC_CORROSION_DEBUFF_DURATION = 3 * 60;

    public const int BEE_SHOOT_INTERVAL = 7;
    
    public const int BEE_SHOOT_AMOUNT = 3;
    
    public const int BURST_AMOUNT = 2;

    public const int SPACESHIP_AMOUNT = 2;
    
    public int Counter { get; private set; }

    public override ModItem Clone(Item newEntity)
    {
        var original = base.Clone(newEntity);

        if (original is not BeeBlasterItem clone)
        {
            return original;
        }

        clone.Counter = Counter;

        return clone;
    }
    
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.DamageType = DamageClass.Ranged;

        Item.SetWeaponValues(36, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 44;
        Item.height = 36;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 20f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Orange;

        Item.EnableComponent<ItemBurstData>().SetBursts(BURST_AMOUNT);
        
        Item.EnableComponent<ItemMinionData>().AddMinion<SpaceshipProjectile>(SPACESHIP_AMOUNT);
        
        Item.EnableComponent<ItemBuffData>().AddBuff<GalvanicCorrosion>(GALVANIC_CORROSION_DEBUFF_DURATION);
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
            .AddIngredient<RadioactiveFlyerBlasterItem>()
            .AddIngredient(ItemID.BeeGun)
            .AddIngredient(ItemID.BeeWax, 11)
            .AddTile(TileID.Anvils)
            .Register();
    }
}