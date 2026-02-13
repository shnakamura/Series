using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Ranged;
using Series.Common.Items.Buffs;
using Series.Common.Items.Guns;
using Series.Core.Items;
using Terraria.DataStructures;
using ThoriumMod.Items.BossViscount;
using ThoriumMod.Items.HealerItems;

namespace Series.Content.Items;

public class BloodBlasterItem : GunItemActor
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

        Item.SetWeaponValues(35, 2.5f, 10);

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.width = 46;
        Item.height = 34;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 25f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.EnableComponent<ItemBuffData>().AddBuff(ModContent.BuffType<GalvanicCorrosion>(), GALVANIC_CORROSION_DEBUFF_DURATION);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
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
            .AddIngredient<AstroDuoBlasterItem>()
            .AddIngredient<UnholyShards>(50)
            .AddIngredient<DraculaFang>(400)
            .AddTile(TileID.Anvils)
            .Register();
    }
}