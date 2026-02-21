using Series.Core.Items;
using Series.Utilities;

namespace Series.Common.Items.Minions;

public sealed class ItemMinionPlayerSystem : ModPlayer
{
    public override void PostUpdate()
    {
        base.PostUpdate();

        SpawnMinions();
    }

    private void SpawnMinions()
    {
        var item = Player.HeldItem;

        if (!item.TryGetComponent(out ItemMinionData data))
        {
            return;
        }
        
        foreach (var minion in data.Minions)
        {
            if (ProjectileUtilities.Exists(minion.Type, minion.Amount))
            {
                continue;
            }
            
            Projectile.NewProjectile(Player.GetSource_ItemUse(item), Player.Center, Player.velocity, minion.Type, 0, 0f, Player.whoAmI);
        }
    }
}