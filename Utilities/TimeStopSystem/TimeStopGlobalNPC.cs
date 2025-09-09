using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace HololiveMod.Utilities
{
    internal class TimeStopGlobalNPC : GlobalNPC
    {
        public override bool PreAI(Terraria.NPC npc)
        {
            if (TimeStopSystem.ShouldEntityBeFrozen(npc)) return false;
            return base.PreAI(npc);
        }

        public override void PostAI(Terraria.NPC npc)
        {
            if (TimeStopSystem.ShouldEntityBeFrozen(npc))
            {
                npc.velocity = Vector2.Zero;
            }
        }
    }
}
