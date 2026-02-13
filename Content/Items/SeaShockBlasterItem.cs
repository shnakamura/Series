using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Items.Buffs;
using Series.Common.Items.Bursts;
using Series.Common.Items.Guns;
using Series.Common.Items.Minions;
using Series.Content.Projectiles;
using Series.Core.Items;
using Terraria.DataStructures;
using ThoriumMod.Items.NPCItems;

namespace Series.Content.Items;

public class SeaShockBlasterItem : GunItemActor
{
    /// <summary>
    ///     The duration of the <see cref="GalvanicCorrosion" /> debuff applied by projectiles shot by this
    ///     item, in frames.
    /// </summary>
    public const int GALVANIC_CORROSION_DEBUFF_DURATION = 3 * 60;

    public const int FUNGAL_ROUND_SHOOT_INTERVAL = 5;
    
    public const int BURST_AMOUNT = 2;

    public int Counter { get; private set; }

    public override ModItem Clone(Item newEntity)
    {
        var original = base.Clone(newEntity);

        if (original is not SeaShockBlasterItem clone)
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

        Item.shootSpeed = 20f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.EnableComponent<ItemBurstData>().SetBursts(BURST_AMOUNT);

        Item.EnableComponent<ItemMinionData>().AddMinion<VultureProjectile>();
        
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

        type = ModContent.ProjectileType<_PearlBullet>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
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
            .AddIngredient<SonicMushBlasterItem>()
            .AddIngredient<MarineLauncher>()
            .AddIngredient<SeaRemains>(8)
            .AddIngredient<SeaPrism>(20)
            .AddTile(TileID.Anvils)
            .Register();
    }
}