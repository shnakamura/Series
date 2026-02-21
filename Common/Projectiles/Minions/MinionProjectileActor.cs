using System.IO;
using Series.Common.Items.Minions;
using Series.Core.Items;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Series.Common.Projectiles.Minions;

public abstract class MinionProjectileActor : ModProjectile
{
    protected virtual bool Active => !Owner.HeldItem.IsAir && Owner.HeldItem.TryGetComponent(out ItemMinionData data) && data.HasMinion(Type);
    
    /// <summary>
    ///     Gets the <see cref="Player" /> instance that owns the projectile. Shorthand for
    ///     <c>Main.player[Projectile.owner]</c>.
    /// </summary>
    protected Player Owner => Main.player[Projectile.owner];
    
    protected virtual Vector2 GunPosition { get; set; }

    public override void SetDefaults()
    {
        base.SetDefaults();
        
        Projectile.alpha = byte.MaxValue;
    }

    public override void OnSpawn(IEntitySource source)
    {
        base.OnSpawn(source);

        GunPosition = Projectile.Center;
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
        base.SendExtraAI(writer);
        
        writer.WriteVector2(GunPosition);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        base.ReceiveExtraAI(reader);

        GunPosition = reader.ReadVector2();
    }

    public override void AI()
    {
        base.AI();
        
        UpdateTeleport();
        UpdateState();
        
        UpdateShooting();
        UpdateDirection();
        
        UpdateGunPosition();
        
        Projectile.timeLeft = 2;
    }

    protected virtual void UpdateTeleport()
    {
        const float threshold = 32f * 16f;
        
        var distance = Projectile.DistanceSQ(Owner.Center);
        
        if (distance <= threshold * threshold)
        {
            return;
        }

        GunPosition = Owner.Center;
        Projectile.Center = Owner.Center;
        
        Projectile.netUpdate = true;
    }
    
    protected virtual void UpdateState()
    {
        const int step = 5;
    
        Projectile.alpha -= step * Active.ToDirectionInt();
        Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, byte.MinValue, byte.MaxValue);
        
        if (Active || Projectile.alpha < byte.MaxValue)
        {
            return;
        }
        
        Projectile.Kill();
    }
    
    protected virtual void UpdateShooting()
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
        
        var position = GunPosition;
        var velocity = GunPosition.DirectionTo(Main.MouseWorld) * item.shootSpeed;

        var type = item.shoot;

        var damage = Owner.GetWeaponDamage(item);
        var knockback = Owner.GetWeaponKnockback(item);

        ItemLoader.ModifyShootStats(item, Owner, ref position, ref velocity, ref type, ref damage, ref knockback);

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
    
    protected virtual void UpdateDirection()
    {
        var direction = Math.Sign(Main.MouseWorld.X - Projectile.Center.X);

        Projectile.direction = direction;
        Projectile.spriteDirection = direction;
    }

    protected virtual void UpdateGunPosition()
    {
        var offset = new Vector2(Projectile.spriteDirection * 32f, 16f);
        var position = Projectile.Center + offset;

        GunPosition = Vector2.SmoothStep(GunPosition, position, 0.2f);
    }

    public override void PostDraw(Color lightColor)
    {
        base.PostDraw(lightColor);
        
        DrawGun(in lightColor);
    }
    
    protected virtual void DrawGun(in Color lightColor)
    {
        var item = Owner.HeldItem;

        if (item == null || item.IsAir)
        {
            return;
        }

        Main.instance.LoadItem(item.type);
        
        var texture = TextureAssets.Item[item.type].Value;

        var position = GunPosition - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

        var color = Projectile.GetAlpha(lightColor);

        var origin = texture.Size() / 2f;

        var effects = SpriteEffects.None;

        if (Projectile.spriteDirection == -1)
        {
            effects |= SpriteEffects.FlipHorizontally;
        }

        if ((int)Owner.gravDir == -1)
        {
            effects |= SpriteEffects.FlipVertically;
        }

        var progress = 1f - Owner.itemTime / (float)Owner.itemTimeMax;

        var rotation = (Owner.Center - Main.MouseWorld).ToRotation() * Owner.gravDir + MathHelper.PiOver2;

        if (progress < 0.4f)
        {
            rotation += -0.45f * MathF.Pow((0.4f - progress) / 0.4f, 2f) * Projectile.direction;
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
            rotation,
            origin,
            1f,
            effects
        );
    }
}