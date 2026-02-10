namespace Series.Common.Shooting;

public interface IShootContextModifier
{
    void Modify(ref ItemShootContext context);
}