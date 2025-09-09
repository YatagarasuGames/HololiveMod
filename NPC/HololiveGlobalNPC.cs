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
        public int whirlpoolMarksCount = 0;
        public override bool InstancePerEntity => true;

    }
}
