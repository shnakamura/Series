using CalamityAmmo.Projectiles;
using Series.Common.Items.Buffs;
using Series.Common.Items.Bursts;
using Series.Common.Items.Guns;
using Series.Common.Items.Minions;
using Series.Common.Recipes;
using Series.Content.Projectiles;
using Series.Core.Items;

namespace Series.Content.Items;

public class DoubleDarkBlasterItem : GunItemActor
{
    /// <summary>
    ///     The duration of the <see cref="BuffID.Slimed" /> debuff applied by projectiles shot by this
    ///     item, in frames.
    /// </summary>
    public const int SLIMED_DEBUFF_DURATION = 3 * 60;
    
    public const int BURST_AMOUNT = 2;

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

        Item.shootSpeed = 20f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.EnableComponent<ItemBurstData>().SetBursts(BURST_AMOUNT);
        
        Item.EnableComponent<ItemMinionData>().AddMinion<VultureProjectile>();
        
        Item.EnableComponent<ItemBuffData>().AddBuff(BuffID.Slimed, SLIMED_DEBUFF_DURATION);
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