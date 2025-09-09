using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace HololiveMod.Utilities
{
    public static partial class HololiveUtils
    {
        public static HololivePlayer.HololivePlayer Hololive(this Player player) => player.GetModPlayer<HololivePlayer.HololivePlayer>();
    }
}
