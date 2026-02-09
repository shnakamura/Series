using Terraria.DataStructures;

namespace Series.Common.Shooting;

public struct ItemShootContext
{
    /// <summary>
    ///     The entity source of the projectile being shot.
    /// </summary>
    public EntitySource_ItemUse_WithAmmo Source;

    /// <summary>
    ///     The item from which the projectile is being shot.
    /// </summary>
    public Item Item;

    /// <summary>
    ///     The player who is shooting the projectile.
    /// </summary>
    public Player Player;

    /// <summary>
    ///     The position from which the projectile is being shot.
    /// </summary>
    public Vector2 Position;

    /// <summary>
    ///     The velocity of the projectile being shot.
    /// </summary>
    public Vector2 Velocity;

    /// <summary>
    ///     The type of the projectile being shot.
    /// </summary>
    public int Type;

    /// <summary>
    ///     The damage of the projectile being shot.
    /// </summary>
    public int Damage;

    /// <summary>
    ///     The knockback of the projectile being shot.
    /// </summary>
    public float Knockback;

    public ItemShootContext(EntitySource_ItemUse_WithAmmo source, Item item, Player player, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Source = source;
        
        ArgumentNullException.ThrowIfNull(item, nameof(item));
        
        Item = item;
        
        ArgumentNullException.ThrowIfNull(player, nameof(player));
        
        Player = player;
        
        Position = position;
        Velocity = velocity;
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(type, nameof(type));
        
        Type = type;
        
        ArgumentOutOfRangeException.ThrowIfNegative(damage, nameof(damage));
        
        Damage = damage;
        
        ArgumentOutOfRangeException.ThrowIfNegative(knockback, nameof(knockback));
        
        Knockback = knockback;
    }
}