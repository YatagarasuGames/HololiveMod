using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using HololiveMod.NPC.Bosses;

namespace HololiveMod.Utilities
{
    public static class TimeStopSystem
    {

        public static bool IsTimeStopped = false;
        public static int TimeStopTimer;
        public static int TimeStopDuration;

        public static void ActivateTimeStop(int duration)
        {
            IsTimeStopped = true;
            TimeStopTimer = 0;
            TimeStopDuration = duration;

            SoundEngine.PlaySound(SoundID.AbigailAttack, new Microsoft.Xna.Framework.Vector2(Main.player[Main.myPlayer].position.X, Main.player[Main.myPlayer].position.Y));

            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(Main.player[Main.myPlayer].position, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height,
                    DustID.GemDiamond, 0f, 0f, 100, default, 2f);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }

        public static void Update()
        {
            if (IsTimeStopped)
            {
                TimeStopTimer++;
                if (TimeStopTimer >= TimeStopDuration)
                {
                    IsTimeStopped = false;

                    //SoundEngine.PlaySound(SoundID.AbigailAttack, new Microsoft.Xna.Framework.Vector2(Main.player[Main.myPlayer].position.X, Main.player[Main.myPlayer].position.Y));
                }
            }
        }

        public static bool ShouldEntityBeFrozen(Entity entity)
        {
            // Проверяем, должно ли существо быть заморожено
            if (!IsTimeStopped) return false;

            // Исключения - босс и его миньоны не замораживаются
            if (entity is Terraria.NPC npc)
            {
                if (npc.type == ModContent.NPCType<AmeliaWatson>())
                    return false;
            }

            // Снаряды босса не замораживаются
            if (entity is Projectile projectile)
            {
                if (projectile.owner == -1) // Снаряды, созданные не игроком
                    return false;
            }

            return true;
        }

    }
}
