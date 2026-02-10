namespace Series.Common.Items.Shooting;

public sealed class FriendlyModifier : IProjectileModifier
{
    void IProjectileModifier.Modify(Projectile projectile)
    {
        projectile.friendly = true;
        projectile.hostile = false;
    }
}