namespace Series.Common.Items.Shooting;

public interface IShootContextModifier
{
    void Modify(ref ItemShootContext context);
}