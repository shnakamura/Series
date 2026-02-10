namespace Series.Common.Items.Shooting.Patterns;

public class IntervalShootPattern : ItemShootPattern
{
    /// <summary>
    ///     Gets the number of item uses required between each projectile shot.
    /// </summary>
    public int Interval { get; }
    
    /// <summary>
    ///     Gets the current usage counter used to track the shooting interval.
    /// </summary>
    public int Counter { get; private set; }

    public IntervalShootPattern(int interval)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(interval, nameof(interval));

        Interval = interval;
    }
    
    public override int Shoot(in ItemShootContext context)
    {
        Counter++;

        if (Counter < Interval)
        {
            return -1;
        }

        Counter = 0;
        
        return Projectile.NewProjectile(context.Source, context.Position, context.Velocity, context.Type, context.Damage, context.Knockback, context.Player.whoAmI);
    }
}