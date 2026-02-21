namespace Series.Utilities;

public static class ProjectileUtilities
{
    public static bool Exists(int type, int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(type, nameof(type));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, nameof(amount));

        var count = 0;

        foreach (var projectile in Main.ActiveProjectiles)
        {
            if (!projectile.active || projectile.type != type)
            {
                continue;
            }
            
            count++;
        }

        return count >= amount;
    }
}