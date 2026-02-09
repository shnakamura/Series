namespace Series.Common.Shooting.Modifiers;

public sealed class MuzzleOffsetModifier(float Offset) : IShootContextModifier
{
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