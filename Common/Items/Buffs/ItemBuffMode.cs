namespace Series.Common.Items.Buffs;

/// <summary>
///     Specifies the mode in which a buff will be applied by an item.
/// </summary>
public enum ItemBuffMode
{
    /// <summary>
    ///     No buffs will be applied by the item.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Buffs will be applied by the item when it is used to make contact damage with an NPC or player.
    /// </summary>
    Contact = 1 << 0,

    /// <summary>
    ///     Buffs will be applied by the item when it is used to shoot a projectile that hits an NPC or
    ///     player.
    /// </summary>
    Shoot = 1 << 1,

    /// <summary>
    ///     Buffs will be applied both when the item is used to make contact damage with an NPC or player
    ///     and when it is used to shoot a projectile that hits an NPC or player.
    /// </summary>
    All = Contact | Shoot
}