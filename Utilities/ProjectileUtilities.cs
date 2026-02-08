namespace Series.Utilities;

public static class ProjectileUtilities
{
    /// <summary>
    ///     Checks whether a projectile of the specified type exists in the world.
    /// </summary>
    /// <param name="type">The type of the projectile to check.</param>
    /// <returns><c>true</c> if a projectile of the specified type exists in the world; otherwise, <c>false</c>.</returns>
    public static bool Exists(int type)
    {
        foreach (var projectile in Main.ActiveProjectiles)
        {
            if (projectile.active && projectile.type == type)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks whether a projectile of the specified type exists in the world.
    /// </summary>
    /// <typeparam name="T">The type of the projectile to check.</typeparam>
    /// <returns><c>true</c> if a projectile of the specified type exists in the world; otherwise, <c>false</c>.</returns>
    public static bool Exists<T>() where T : ModProjectile
    {
        foreach (var projectile in Main.ActiveProjectiles)
        {
            if (projectile.active && projectile.type == ModContent.ProjectileType<T>())
            {
                return true;
            }
        }

        return false;
    }
}