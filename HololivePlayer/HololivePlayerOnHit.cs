using HololiveMod.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.HololivePlayer
{
    public partial class HololivePlayer : ModPlayer
    {
        public override void ModifyHitNPCWithItem(Item item, Terraria.NPC target, ref Terraria.NPC.HitModifiers modifiers)
        {
            if (whirlpool)
            {
                target.Hololive().whirlpoolMarksCount++;
                if(target.Hololive().whirlpoolMarksCount >= 5)
                {
                    target.Hololive().whirlpoolMarksCount = 0;
                    target.SimpleStrikeNPC(100, 0);
                }
            }
            
        }

        public override void OnHitNPCWithProj(Projectile proj, Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithProj(proj, target, hit, damageDone);
        }
    }
}
