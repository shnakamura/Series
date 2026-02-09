using Series.Common.Debuffs;
using Series.Common.Guns;
using Series.Common.Shooting;
using Series.Common.Shooting.Modifiers;
using Series.Core.Items;

namespace Series.Content.Items;

public class SlimedShinobiBlasterItem : GunItemActor
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

        Item.SetWeaponValues(31, 2.5f, 20);

        Item.noMelee = true;
        Item.autoReuse = true;

        Item.width = 48;
        Item.height = 32;

        Item.UseSound = SoundID.Item40;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;

        Item.shootSpeed = 25f;
        Item.shoot = ProjectileID.Bullet;

        Item.rare = ItemRarityID.Green;

        Item.Get<ItemShootComponent>().AddModifier(new MuzzleOffsetModifier(25f));
        
        Item.Get<ItemDebuffDataComponent>().Add(BuffID.Slimed, SLIMED_DEBUFF_DURATION);
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<SandWingBlasterItem>()
            .AddIngredient(ItemID.NinjaHood)
            .AddIngredient(ItemID.NinjaShirt)
            .AddIngredient(ItemID.NinjaPants)
            .AddTile(TileID.Anvils)
            .Register();
    }
}