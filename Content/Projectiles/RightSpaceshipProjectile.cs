using Series.Content.Items;
using Terraria.Audio;
using Terraria.GameContent;

namespace Series.Content.Projectiles;

public class RightSpaceshipProjectile : ModProjectile
{
    public static int[] ItemTypes =>
    [
        ModContent.ItemType<RadioactiveFlyerBlasterItem>(),
        ModContent.ItemType<SuperSlimedBlasterItem>(),
        ModContent.ItemType<BloodBlasterItem>(),
        ModContent.ItemType<BeeBlasterItem>(),
        ModContent.ItemType<BuriedBlasterItem>(),
        ModContent.ItemType<AstroDuoBlasterItem>(),
        ModContent.ItemType<BonerBlasterItem>(),
        ModContent.ItemType<BlastforgeBlasterItem>(),
    ];

    /// <summary>
    ///     Gets the <see cref="Player" /> instance that owns the projectile. Shorthand for
    ///     <c>Main.player[Projectile.owner]</c>.
    /// </summary>
    private Player Owner => Main.player[Projectile.owner];

    public override void SetDefaults()
    {
        base.SetDefaults();

        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.netImportant = true;

        Projectile.width = 64;
        Projectile.height = 64;

        Projectile.alpha = byte.MaxValue;

        Projectile.penetrate = -1;
    }

    public override void AI()
    {
        base.AI();

        Projectile.timeLeft = 2;

        UpdateState();

        UpdateMovement();
        UpdateShooting();

        var direction = Math.Sign(Projectile.velocity.X);

        Projectile.direction = direction;
        Projectile.spriteDirection = direction;
        
        Projectile.rotation = Projectile.velocity.X * 0.1f;
    }

    private bool ShouldBeActive()
    {
        var active = false;

        foreach (var itemType in ItemTypes)
        {
            if (Owner.HeldItem?.IsAir == false && Owner.HeldItem.type == itemType)
            {
                active = true;
            }
        }

        return active;
    }

    private void UpdateState()
    {
        const int step = 5;

        if (ShouldBeActive() && Projectile.alpha > byte.MinValue)
        {
            Projectile.alpha -= step;
        }
        else if (!ShouldBeActive())
        {
            Projectile.alpha += step;

            if (Projectile.alpha < byte.MaxValue)
            {
                return;
            }

            Projectile.Kill();
        }

        Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, byte.MinValue, byte.MaxValue);
    }

    private void UpdateMovement()
    {
        ref var timer = ref Projectile.ai[0];

        timer++;

        var position = Owner.Center + new Vector2(64f, -96f);

        var difference = position - Projectile.Center;
        var distance = difference.Length();

        if (distance < 32f)
        {
            Projectile.velocity *= 0.98f;
        }
        else
        {
            var direction = difference / distance;

            var speed = MathHelper.Clamp(distance * 0.08f, 2f, 10f);

            var velocity = direction * speed;
            var steering = velocity - Projectile.velocity;

            var force = 0.25f;

            steering = Vector2.Clamp(steering, -Vector2.One * force, Vector2.One * force);

            Projectile.velocity += steering;
            Projectile.velocity *= 0.98f;
        }

        if (distance <= 32f * 16f)
        {
            return;
        }

        Projectile.Center = Owner.Center;

        Projectile.netUpdate = true;
    }

    private void UpdateShooting()
    {
        var item = Owner.HeldItem;

        if (Owner.HeldItem == null || Owner.HeldItem.IsAir)
        {
            return;
        }

        ref var timer = ref Projectile.ai[1];

        timer++;

        if (timer < item.useTime || !Owner.controlUseItem)
        {
            return;
        }
        
        var position = Projectile.Center + new Vector2(Projectile.spriteDirection * 32f, 16f);
        var velocity = Projectile.DirectionTo(Main.MouseWorld) * item.shootSpeed;

        var type = item.shoot;

        var damage = Owner.GetWeaponDamage(item);
        var knockback = Owner.GetWeaponKnockback(item);

        var unused = Vector2.Zero;

        ItemLoader.ModifyShootStats(item, Owner, ref unused, ref velocity, ref type, ref damage, ref knockback);

        ItemLoader.Shoot(item, Owner, null, position, velocity, type, damage, knockback);

        Projectile.NewProjectile
        (
            Projectile.GetSource_FromAI(),
            position,
            velocity,
            type,
            damage,
            knockback,
            Owner.whoAmI
        );

        SoundEngine.PlaySound(in item.UseSound, Projectile.Center);
        
        timer = 0f;

        Projectile.netUpdate = true;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        DrawProjectile(in lightColor);

        return false;
    }

    public override void PostDraw(Color lightColor)
    {
        base.PostDraw(lightColor);

        DrawGun(in lightColor);
    }

    private void DrawProjectile(in Color lightColor)
    {
        var texture = TextureAssets.Projectile[Type].Value;

        var position = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

        var color = Projectile.GetAlpha(lightColor);

        var rotation = Projectile.rotation;

        var origin = texture.Size() / 2f;

        var effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        Main.EntitySpriteDraw(texture, position, null, color, rotation, origin, 1f, effects);
    }

    private void DrawGun(in Color lightColor)
    {
        var item = Owner.HeldItem;

        if (item == null || item.IsAir)
        {
            return;
        }

        Main.instance.LoadItem(item.type);
        
        var texture = TextureAssets.Item[item.type].Value;

        var anchor = Projectile.Center + new Vector2(Projectile.spriteDirection * 32f, 16f);

        var position = anchor - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

        var color = Projectile.GetAlpha(lightColor);

        var progress = 1f - Owner.itemTime / (float)Owner.itemTimeMax;

        var rotation = (Owner.Center - Main.MouseWorld).ToRotation() * Owner.gravDir + MathHelper.PiOver2;

        if (progress < 0.4f)
        {
            rotation += -0.45f * MathF.Pow((0.4f - progress) / 0.4f, 2f) * Projectile.direction;
        }

        var origin = texture.Size() / 2f;

        var effects = SpriteEffects.None;

        if (Projectile.spriteDirection == -1)
        {
            effects |= SpriteEffects.FlipHorizontally;
        }

        if (Owner.gravDir == -1)
        {
            effects |= SpriteEffects.FlipVertically;
        }

        if (Projectile.spriteDirection == 1)
        {
            rotation += MathHelper.PiOver2;
        }
        else
        {
            rotation -= MathHelper.PiOver2;
        }

        Main.EntitySpriteDraw
        (
            texture,
            position,
            null,
            color,
            Projectile.rotation / 2f + rotation,
            origin,
            1f,
            effects
        );
    }
}