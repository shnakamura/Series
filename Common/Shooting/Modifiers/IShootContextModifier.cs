namespace Series.Common.Shooting.Modifiers;

public interface IShootContextModifier
{
    void Modify(ref ItemShootContext context);
}