using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HololiveMod.Utilities
{
    internal class TimeStopPlayer : ModPlayer
    {
        private Vector2 lockedPosition;
        private float originalGravity;
        private bool originalGravControl;
        public override void PreUpdate()
        {
            if (TimeStopSystem.IsTimeStopped)
            {
                if (lockedPosition == Vector2.Zero)
                {
                    lockedPosition = Player.position;
                    originalGravity = Player.gravity;
                    originalGravControl = Player.gravControl;
                }


                // Заморозка движения игрока
                Player.controlLeft = false;
                Player.controlRight = false;
                Player.controlUp = false;
                Player.controlDown = false;
                Player.controlJump = false;
                Player.velocity = Vector2.Zero;
                Player.gravity = 0;
                Player.controlHook = false;
                Player.controlUseItem = false;
                Player.controlMap = false;
                Player.gravControl = false;
                Player.controlMount = false;
                Player.controlSmart = false;
                Player.controlTorch = false;
                Player.controlThrow = false;
                Player.controlInv = false;
                Player.controlQuickHeal = false;
                Player.controlQuickMana = false;
                Player.controlUseTile = false;

                if (Player.mount.Active)
                    Player.mount.Dismount(Player);

                Player.grappling[0] = -1;
                Player.grapCount = 0;
                Player.dash = 0;
                Player.wingTime = 0;
                Player.ignoreWater = true;
                Player.portalPhysicsFlag = false;

                Player.position = lockedPosition;
                Player.fallStart = (int)(Player.position.Y / 16f);

                // Визуальные эффекты на игроке
                if (Main.rand.Next(5) == 0)
                {
                    Dust dust = Dust.NewDustDirect(Player.position, Player.width, Player.height,
                        DustID.GemDiamond, 0f, 0f, 100, default, 1f);
                    dust.noGravity = true;
                }
            }
        }
    }
}
