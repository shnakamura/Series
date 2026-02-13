namespace Series.Common.Recipes;

/// <summary>
///     Handles the registration of custom recipe groups used by the mod.
/// </summary>
public sealed class RecipeGroupSystem : ModSystem
{
    /// <summary>
    ///     The unique identifier for the Evil Bar recipe group.
    /// </summary>
    public const string EVIL_BAR_RECIPE_GROUP_NAME = $"{nameof(Series)}:{nameof(EvilBar)}";
    
    /// <summary>
    ///     The unique identifier for the Tungsten Bar recipe group.
    /// </summary>
    public const string TUNGSTEN_BAR_RECIPE_GROUP_NAME = $"{nameof(Series)}:{nameof(TungstenBar)}";

    /// <summary>
    ///     A recipe group containing <see cref="ItemID.DemoniteBar" /> and
    ///     <see cref="ItemID.CrimtaneBar" />.
    /// </summary>
    public static RecipeGroup EvilBar { get; private set; }
    
    /// <summary>
    ///     A recipe group containing <see cref="ItemID.TungstenBar" /> and
    ///     <see cref="ItemID.SilverBar" />.
    /// </summary>
    public static RecipeGroup TungstenBar { get; private set; }

    public override void AddRecipeGroups()
    {
        base.AddRecipeGroups();

        EvilBar = new RecipeGroup(static () => $"{Lang.GetItemNameValue(ItemID.DemoniteBar)}/{Lang.GetItemNameValue(ItemID.CrimtaneBar)}", ItemID.DemoniteBar, ItemID.CrimtaneBar);

        RecipeGroup.RegisterGroup(EVIL_BAR_RECIPE_GROUP_NAME, EvilBar);
        
        TungstenBar = new RecipeGroup(static () => $"{Lang.GetItemNameValue(ItemID.TungstenBar)}/{Lang.GetItemNameValue(ItemID.SilverBar)}", ItemID.TungstenBar, ItemID.SilverBar);
        
        RecipeGroup.RegisterGroup(TUNGSTEN_BAR_RECIPE_GROUP_NAME, TungstenBar);
    }

    public override void Unload()
    {
        base.Unload();

        EvilBar = null;
        TungstenBar = null;
    }
}