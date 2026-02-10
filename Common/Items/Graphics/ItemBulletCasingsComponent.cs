using Series.Core.Items;
using Terraria.GameContent;

namespace Series.Common.Items.Graphics;

public sealed class ItemBulletCasingsComponent : ItemComponent
{
    /// <summary>
    ///     Gets the type of the casing to spawn. Defaults to <c>-1</c>, which means no casings will be
    ///     spawned.
    /// </summary>
    public int CasingType { get; private set; } = -1;

    /// <summary>
    ///     Gets the amount of casings to spawn. Defaults to <c>0</c>, which means no casings will be
    ///     spawned.
    /// </summary>
    public int CasingAmount { get; private set; }

    public override bool? UseItem(Item item, Player player)
    {
        if (!Enabled || CasingType < 0 || CasingAmount <= 0)
        {
            return base.UseItem(item, player);
        }

        var texture = TextureAssets.Gore[CasingType].Value;
        var position = player.Center + new Vector2(12f * player.direction, -6f);

        if (player.direction == -1)
        {
            position -= texture.Size() / 2f + new Vector2(12f, 0f);
        }

        for (var i = 0; i < CasingAmount; i++)
        {
            var velocity = new Vector2(Main.rand.NextFloat(0.75f, 1f) * -player.direction, -Main.rand.NextFloat(1f, 1.5f)) + player.velocity * 0.5f;

            Gore.NewGore(player.GetSource_ItemUse(item), position, velocity, CasingType);
        }

        return null;
    }

    /// <summary>
    /// </summary>
    /// <param name="casingType"></param>
    /// <param name="casingAmount"></param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="casingType" /> or <paramref name="casingAmount" /> is less than or
    ///     equal to zero.
    /// </exception>
    public void Set(int casingType, int casingAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(casingType, nameof(casingType));

        CasingType = casingType;

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(casingAmount, nameof(casingAmount));

        CasingAmount = casingAmount;
    }
}