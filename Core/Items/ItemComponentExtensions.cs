using System.Diagnostics.CodeAnalysis;

namespace Series.Core.Items;

public static class ItemComponentExtensions
{
    /// <summary>
    ///     Enables the specified <see cref="ItemComponent" /> on the given <see cref="Item" />.
    /// </summary>
    /// <param name="item">The <see cref="Item" /> on which to enable the component.</param>
    /// <typeparam name="T">The type of the <see cref="ItemComponent" /> to enable.</typeparam>
    /// <returns>The enabled <see cref="ItemComponent" /> instance.</returns>
    public static T EnableComponent<T>(this Item item) where T : ItemComponent
    {
        var component = item.GetGlobalItem<T>();

        component.Enabled = true;

        return component;
    }

    /// <summary>
    ///     Disables the specified <see cref="ItemComponent" /> on the given <see cref="Item" />.
    /// </summary>
    /// <param name="item">The <see cref="Item" /> on which to disable the component.</param>
    /// <typeparam name="T">The type of the <see cref="ItemComponent" /> to disable.</typeparam>
    /// <returns>
    ///     <see langword="true" /> if the <see cref="ItemComponent" /> instance was enabled and is now
    ///     disabled; otherwise, <see langword="false" /> if the <see cref="ItemComponent" /> instance was
    ///     already disabled.
    /// </returns>
    public static bool DisableComponent<T>(this Item item) where T : ItemComponent
    {
        var component = item.GetGlobalItem<T>();

        if (!component.Enabled)
        {
            return false;
        }

        component.Enabled = false;

        return true;
    }

    /// <summary>
    ///     Retrieves the specified <see cref="ItemComponent" /> from the given <see cref="Item" />.
    /// </summary>
    /// <param name="item">The <see cref="Item"/> from which to retrieve the component.</param>
    /// <typeparam name="T">The type of the <see cref="ItemComponent"/> to retrieve.</typeparam>
    /// <returns>The retrieved <see cref="ItemComponent"/> instance.</returns>
    public static T GetComponent<T>(this Item item) where T : ItemComponent
    {
        return item.GetGlobalItem<T>();
    }

    /// <summary>
    ///     Attempts to retrieve the specified <see cref="ItemComponent" /> from the given
    ///     <see cref="Item" />.
    /// </summary>
    /// <param name="item">The <see cref="Item" /> from which to retrieve the component.</param>
    /// <param name="component">
    ///     When this method returns <see langword="true" />, contains the enabled
    ///     <see cref="ItemComponent" /> instance; otherwise, contains <see langword="null" />.
    /// </param>
    /// <typeparam name="T">The type of the <see cref="ItemComponent" /> to retrieve.</typeparam>
    /// <returns>
    ///     <see langword="true" /> if the <see cref="ItemComponent" /> instance is enabled; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public static bool TryGetComponent<T>(this Item item, [MaybeNullWhen(false)] out T component) where T : ItemComponent
    {
        return item.TryGetGlobalItem(out component) && component.Enabled;
    }

    /// <summary>
    ///     Determines whether the specified <see cref="ItemComponent" /> is enabled on the given
    ///     <see cref="Item" />.
    /// </summary>
    /// <param name="item">The <see cref="Item" /> to check.</param>
    /// <typeparam name="T">The type of the <see cref="ItemComponent" /> to check.</typeparam>
    /// <returns>
    ///     <see langword="true" /> if the <see cref="ItemComponent" /> instance is enabled; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public static bool HasComponent<T>(this Item item) where T : ItemComponent
    {
        return item.GetGlobalItem<T>().Enabled;
    }
}