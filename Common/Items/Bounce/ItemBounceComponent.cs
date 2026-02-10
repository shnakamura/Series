using Series.Core.Items;

namespace Series.Common.Items.Bounce;

public sealed class ItemBounceComponent : ItemComponent
{
    /// <summary>
    ///     Gets the amount of times a projectile shot from this item can bounce.
    /// </summary>
    public int Bounces { get; private set; }
    
    /// <summary>
    ///     Gets the multiplier applied to the velocity of the projectile shot from this item after bouncing.
    /// </summary>
    public float Multiplier { get; private set; }
    
    /// <summary>
    ///     Sets the amount of times a projectile shot from this item can bounce.
    /// </summary>
    /// <param name="bounces">The amount of times a projectile shot from this item can bounce.</param>
    /// <returns></returns>
    public ItemBounceComponent SetBounces(int bounces)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bounces, nameof(bounces));
        
        Bounces = bounces;

        return this;
    }
    
    /// <summary>
    ///     Sets the multiplier applied to the velocity of the projectile shot from this item after bouncing.
    /// </summary>
    /// <param name="multiplier">The multiplier applied to the velocity of the projectile shot from this item after bouncing.</param>
    /// <returns></returns>
    public ItemBounceComponent SetMultiplier(float multiplier)
    {
        Multiplier = multiplier;

        return this;
    }
}