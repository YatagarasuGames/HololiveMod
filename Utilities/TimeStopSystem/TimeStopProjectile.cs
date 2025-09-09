using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.Utilities
{
    internal class TimeStopProjectile : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            if (TimeStopSystem.ShouldEntityBeFrozen(projectile))
            {
                foreach (var player in Main.player)
                {
                    if (projectile.owner == player.whoAmI) projectile.Kill();
                }
                return false;
            }
            return base.PreAI(projectile);
        }
    }
}
