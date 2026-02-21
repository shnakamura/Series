using Series.Common.Projectiles.Minions;
using Terraria.GameContent;

namespace Series.Content.Projectiles;

public class SpaceshipProjectile : MinionProjectileActor
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        ProjectileID.Sets.TrailingMode[Type] = 2;
        ProjectileID.Sets.TrailCacheLength[Type] = 4;
    }
    
    public override void SetDefaults()
    {
        base.SetDefaults();

        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.netImportant = true;

        Projectile.width = 64;
        Projectile.height = 64;

        Projectile.penetrate = -1;
    }

    public override void AI()
    {
        base.AI();

        UpdateVisuals();
        UpdateMovement();
    }

    protected virtual void UpdateVisuals()
    {
        Projectile.rotation = Projectile.velocity.X * 0.1f;
    }

    protected virtual void UpdateMovement()
    {
        ref var timer = ref Projectile.ai[0];
        
        timer++;

        var offset = new Vector2(-Owner.direction * 24f, -128f);
        var wave = new Vector2(MathF.Sin(timer * 0.075f + Projectile.whoAmI), 0f) * 64f;

        var position = Active ? Owner.Center + offset + wave : Owner.Center - new Vector2(0f, 64f * 16f);

        var difference = position - Projectile.Center;
        var distance = difference.Length();

        if (distance > 0.001f)
        {
            difference /= distance;
        }

        var follow = MathHelper.Clamp(distance * 0.06f, 3f, 12f);
        var velocity = difference * follow;

        var perpendicular = new Vector2(-difference.Y, difference.X);
        velocity += perpendicular * MathF.Sin(timer * 0.08f) * 1.5f;

        var steering = velocity - Projectile.velocity;
        
        const float force = 0.35f;
        
        steering = Vector2.Clamp(steering, -Vector2.One * force, Vector2.One * force);

        Projectile.velocity += steering;
        Projectile.velocity *= 0.99f;

        const float speed = 1.2f;
        
        if (Projectile.velocity.LengthSquared() >= speed * speed)
        {
            return;
        }
        
        Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * speed;
    }
    
    public override bool PreDraw(ref Color lightColor)
    {
        DrawAfterimage(in lightColor);
        DrawProjectile(in lightColor);

        return false;
    }

    protected virtual void DrawAfterimage(in Color lightColor)
    {
        var length = ProjectileID.Sets.TrailCacheLength[Type];
        
        for (var i = 0; i < length; i++)
        {
            var texture = TextureAssets.Projectile[Type].Value;

            var position = Projectile.oldPos[i] + Projectile.Size / 2f + new Vector2(0f, Projectile.gfxOffY) - Main.screenPosition;

            var progress = 1f - i / (float)length;
            var color = Projectile.GetAlpha(lightColor) * progress;

            var rotation = Projectile.oldRot[i];

            var origin = texture.Size() / 2f;

            var effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.EntitySpriteDraw(texture, position, null, color, rotation, origin, 1f, effects);
        }
    }

    protected virtual void DrawProjectile(in Color lightColor)
    {
        var texture = TextureAssets.Projectile[Type].Value;

        var position = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

        var color = Projectile.GetAlpha(lightColor);

        var rotation = Projectile.rotation;

        var origin = texture.Size() / 2f;

        var effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        Main.EntitySpriteDraw(texture, position, null, color, rotation, origin, 1f, effects);
    }
}