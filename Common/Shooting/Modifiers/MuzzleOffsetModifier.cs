namespace Series.Common.Shooting.Modifiers;

public sealed class MuzzleOffsetModifier : IShootContextModifier
{
    /// <summary>
    ///     Gets the distance to offset the muzzle from the original position in the direction of the shot, in pixels.
    /// </summary>
    public float Offset { get; }

    public MuzzleOffsetModifier(float offset)
    {
        Offset = offset;
    }
    
    void IShootContextModifier.Modify(ref ItemShootContext context)
    {
        var offset = Vector2.Normalize(context.Velocity) * Offset;

        if (!Collision.CanHit(context.Position, 0, 0, context.Position + offset, 0, 0))
        {
            return;
        }

        context.Position += offset;
    }
}