namespace Series.Utilities;

public static class ProjectileUtilities
{
    /// <summary>
    ///     Checks whether a projectile of the specified type exists in the world.
    /// </summary>
    /// <typeparam name="T">The type of the projectile to check.</typeparam>
    /// <returns><see langword="true"/> if a projectile of the specified type exists in the world; otherwise, <see langword="false"/>.</returns>
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