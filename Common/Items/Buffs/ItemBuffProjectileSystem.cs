using System.Collections.Generic;
using System.IO;
using Series.Core.Items;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Series.Common.Items.Buffs;

public sealed class ItemBuffProjectileSystem : GlobalProjectile
{
    private List<IItemBuffApplicationData> buffs;

    public List<IItemBuffApplicationData> Buffs
    {
        get => buffs;
        set
        {
            buffs = value;

            Enabled = true;
        }
    }

    public bool Enabled { get; private set; }

    public override bool InstancePerEntity { get; } = true;

    public override GlobalProjectile Clone(Projectile from, Projectile to)
    {
        var original = base.Clone(from, to);

        if (original is not ItemBuffProjectileSystem clone)
        {
            return original;
        }

        clone.Buffs = Buffs;
        clone.Enabled = Enabled;

        return clone;
    }

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        base.OnSpawn(projectile, source);

        if (source is not EntitySource_ItemUse_WithAmmo use || !use.Item.TryGetComponent(out ItemBuffData component) || (component.Mode & ItemBuffMode.Shoot) == 0)
        {
            return;
        }

        Buffs = component.Buffs;
    }
    
    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
        base.SendExtraAI(projectile, bitWriter, binaryWriter);

        if (!Enabled)
        {
            return;
        }

        var length = Buffs.Count;

        binaryWriter.Write(length);

        for (var i = 0; i < length; i++)
        {
            var data = Buffs[i];

            binaryWriter.Write(data.Type);
            binaryWriter.Write(data.Duration);
        }
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
    {
        base.ReceiveExtraAI(projectile, bitReader, binaryReader);

        if (!Enabled)
        {
            return;
        }

        var length = binaryReader.ReadInt32();

        Buffs = new List<IItemBuffApplicationData>(length);

        for (var i = 0; i < length; i++)
        {
            var type = binaryReader.ReadInt32();
            var duration = binaryReader.ReadInt32();

            Buffs.Add(new ItemBuffApplicationData(type, duration));
        }
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(projectile, target, hit, damageDone);

        if (!Enabled)
        {
            return;
        }

        foreach (var entry in Buffs)
        {
            target.AddBuff(entry.Type, entry.Duration);
        }
    }

    public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
        base.OnHitPlayer(projectile, target, info);

        if (!Enabled)
        {
            return;
        }

        foreach (var entry in Buffs)
        {
            target.AddBuff(entry.Type, entry.Duration);
        }
    }
}