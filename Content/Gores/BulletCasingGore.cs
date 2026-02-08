using Terraria.DataStructures;
using Terraria.GameContent;

namespace Series.Content.Gores;

public class BulletCasingGore : ModGore
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        
        ChildSafety.SafeGore[Type] = true;
    }

    public override void OnSpawn(Gore gore, IEntitySource source)
    {
        base.OnSpawn(gore, source);
        
        gore.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
    }

    public override bool Update(Gore gore)
    {
        const byte step = 5;
        
        if (gore.alpha >= byte.MaxValue)
        {
            gore.active = false;
        }
        else
        {
            gore.alpha += step;
        }

        return true;
    }
}