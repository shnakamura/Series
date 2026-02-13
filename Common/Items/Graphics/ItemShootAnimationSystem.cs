using Series.Core.Items;

namespace Series.Common.Items.Graphics;

public sealed class ItemShootAnimationSystem : GlobalItem
{
     public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
    {
        base.UseStyle(item, player, heldItemFrame);

        if (!item.HasComponent<ItemShootAnimationData>())
        {
            return;
        }

        var direction = Math.Sign(Main.MouseWorld.X - player.Center.X);

        player.ChangeDir(direction);

        var rotation = player.compositeFrontArm.rotation + MathHelper.PiOver2 * player.gravDir;

        var position = player.MountedCenter + rotation.ToRotationVector2() * 7f;

        var size = item.Size;
        var origin = new Vector2(-15f, 1f);

        origin.X *= player.direction;
        origin.Y *= player.gravDir;

        player.itemRotation = rotation;

        if (player.direction < 0)
        {
            player.itemRotation += MathHelper.Pi;
        }

        var consistentCenterAnchor = player.itemRotation.ToRotationVector2() * (size.X / -2f - 10f) * player.direction;
        var consistentAnchor = consistentCenterAnchor - origin.RotatedBy(player.itemRotation);

        var offset = size / 2f;

        var location = position - offset + consistentAnchor;

        var frame = player.bodyFrame.Y / player.bodyFrame.Height;

        if ((frame > 6 && frame < 10) || (frame > 13 && frame < 17))
        {
            location -= new Vector2(0f, 2f);
        }

        player.itemLocation = location + new Vector2(size.X / 2f, 0);
    }

    public override void UseItemFrame(Item item, Player player)
    {
        base.UseItemFrame(item, player);

        if (!item.HasComponent<ItemShootAnimationData>())
        {
            return;
        }

        var direction = Math.Sign(Main.MouseWorld.X - player.Center.X);

        player.ChangeDir(direction);

        var progress = 1f - player.itemTime / (float)player.itemTimeMax;
        var rotation = (player.Center - Main.MouseWorld).ToRotation() * player.gravDir + MathHelper.PiOver2;

        if (progress < 0.4f)
        {
            rotation += -0.45f * MathF.Pow((0.4f - progress) / 0.4f, 2f) * player.direction;
        }

        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation);
    }
}