using HololiveMod.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.NPC
{
    public partial class HololiveGlobalNPC : GlobalNPC
    {

        public override void ModifyHitByItem(Terraria.NPC npc, Player player, Item item, ref Terraria.NPC.HitModifiers modifiers)
        {
            if (!npc.townNPC)
            {
                if (player.Hololive().YingYangSphere)
                {
                    npc.SimpleStrikeNPC(100, 0);
                    npc.SimpleStrikeNPC(10, 0);

                }
            }
        }

        public override void ModifyHitByProjectile(Terraria.NPC npc, Projectile projectile, ref Terraria.NPC.HitModifiers modifiers)
        {
            if (!npc.townNPC)
            {
                Player player = Main.player[projectile.owner];
                if (player.Hololive().YingYangSphere)
                {
                    npc.SimpleStrikeNPC(100, 0);
                }
            }
        }
    }
}
