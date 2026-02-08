using System.Diagnostics.CodeAnalysis;

namespace Series.Core.Items;

public static class ItemComponentExtensions
{
    public static void Enable<T>(this Item item) where T : ItemComponent
    {
        item.GetGlobalItem<T>().Enabled = true;
    }

    public static T Get<T>(this Item item, bool enable = true) where T : ItemComponent
    {
        var component = item.GetGlobalItem<T>();
        
        if (enable && !item.Has<T>())
        {
            item.Enable<T>();
        }

        return component;
    }
    
    public static bool TryGet<T>(this Item item, [MaybeNullWhen(false)] out T component) where T : ItemComponent
    {
        return item.TryGetGlobalItem(out component) && component.Enabled;
    }
    
    public static bool Has<T>(this Item item) where T : ItemComponent
    {
        return item.GetGlobalItem<T>().Enabled;
    }
}