using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using Terraria.Audio;

namespace Series.Content.Projectiles;

public class AcidFeatherProjectile : ModProjectile
{
    public const int IRRADIATED_BUFF_DURATION = 3 * 180;

    public override void SetDefaults()
    {
        base.SetDefaults();

        Projectile.friendly = true;
        
        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.alpha = 255;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(target, hit, damageDone);
        
        target.AddBuff(ModContent.BuffType<Irradiated>(), IRRADIATED_BUFF_DURATION);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        base.OnHitPlayer(target, info);
        
        target.AddBuff(ModContent.BuffType<Irradiated>(), IRRADIATED_BUFF_DURATION);
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

        return true;
    }

    public override void OnKill(int timeLeft)
    {
        base.OnKill(timeLeft);

        SoundEngine.PlaySound(in SoundID.Dig, Projectile.Center);
    }

    public override void AI()
    {
        base.AI();
        
        UpdateOpacity();
        
        Projectile.velocity.X *= 1.01f;
        Projectile.velocity.Y *= 1.025f;
        
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
    }

    private void UpdateOpacity()
    {
        const byte step = 10;

        if (Projectile.alpha > byte.MinValue)
        {
            Projectile.alpha -= step;
        }
        
        Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, byte.MinValue, byte.MaxValue);
    }
}