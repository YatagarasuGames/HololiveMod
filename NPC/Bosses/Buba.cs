using HololiveMod.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod.NPC.Bosses
{
    internal class Buba : ModNPC
    {
        private int dashCountPerRound = 4;
        private int dashesCompleted = 0;
        private int dashPrepareTime = 60;
        private bool isDashing = false;

        private enum Phase
        {
            SwitchingPhase = 0,
            PrepareToDashes = 1,
            Dashing = 2,
            BulletHell = 3,
        }
        private float AIPhase
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        private float PhaseTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        private float DelayBetweenDashes
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        private float TimeBetweenPhases
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        
        

        public override void SetDefaults()
        {
            NPC.width = 200;
            NPC.height = 120;
            NPC.lifeMax = 50_000;
            NPC.defense = 0;
            NPC.damage = 20;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 2 * 100 * 100;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            DelayBetweenDashes = 60;
        }

        public override void AI()
        {
            // Получаем босса и игрока
            Terraria.NPC boss = Main.npc[(int)NPC.localAI[0]];
            Player player = Main.player[(int)NPC.localAI[1]];

            if (!boss.active || !player.active || player.dead)
            {
                NPC.active = false;
                return;
            }

            

            float dashSpeed = 5f;
            float dashAcceleration = 0.5f;

            switch (AIPhase)
            {
                case (int)Phase.SwitchingPhase:
                    CreateDashPrepareParticles(Color.Green);
                    if (PhaseTimer < 10) { PhaseTimer++; return; }
                    if (dashesCompleted == 4) AIPhase = (int)Phase.BulletHell;
                    else AIPhase = 1;
                    PhaseTimer = 0;
                    break;

                case (int)Phase.PrepareToDashes:
                    RotateToPlayer(player);
                    if(PhaseTimer % 20 == 0)
                    {
                        CreateDashPrepareParticles(Color.Red);
                    }
                    if(PhaseTimer == 60)
                    {
                        PhaseTimer = 0;
                        AIPhase = (int)Phase.Dashing;
                        return;
                    }

                    PhaseTimer++;
                    break;

                case (int)Phase.Dashing:

                    if(dashesCompleted == 4)
                    {
                        AIPhase = (int)Phase.SwitchingPhase;
                    }
                    if (isDashing)
                    {
                        if (PhaseTimer == 30) { NPC.velocity = Vector2.Zero; isDashing = false; PhaseTimer = 0; dashesCompleted++; }
                        else { PhaseTimer++; }
                    }

                    else
                    {
                        if(PhaseTimer == 20)
                        {
                            SoundEngine.PlaySound(SoundID.DD2_KoboldFlyerChargeScream, NPC.Center);
                            float chargeSpeed = 32f;
                            NPC.velocity = NPC.SafeDirectionTo(player.Center + player.velocity * 4.5f) * chargeSpeed;
                            isDashing = true;
                            PhaseTimer = 0;
                        }

                        else
                        {
                            RotateToPlayer(player);
                            PhaseTimer++;
                        }
                        
                    }



                    break;

                case (int)Phase.BulletHell:
                    dashesCompleted = 0;
                    
                    if(PhaseTimer % 10 == 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Vector2 shootDirection = Vector2.UnitX.RotatedBy(HoloMathHelper.PiOver2 * i + HoloMathHelper.PiOver4 + HoloMathHelper.ToRadians(spinAttackRotateAngle));
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, shootDirection * 8f, ProjectileID.CursedFlameHostile, NPC.damage / 2, 1f, Main.myPlayer);
                            
                        }
                        spinAttackRotateAngle += 15 + Main.rand.Next(15);
                    }
                    PhaseTimer++;

                    if(PhaseTimer == 300)
                    {
                        AIPhase = (int)Phase.SwitchingPhase;
                        PhaseTimer = 0;
                    }

                    break;
            }
            
        }

        private int spinAttackRotateAngle = 15;

        private void CreateDashPrepareParticles(Color color)
        {
            for(int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height,
                        DustID.RedsWingsRun, 0f, 0f, 100, color, 1f);
                dust.noGravity = true;
            }
        }

        private void RotateToPlayer(Player player)
        {
            if (NPC.Center.X > player.Center.X) NPC.spriteDirection = -1;
            else NPC.spriteDirection = 1;
            Vector2 direction = player.Center - NPC.Center;

            float angle = (float)HoloMathHelper.Atan2(direction.Y, direction.X);

            if (NPC.spriteDirection == -1)
            {
                angle += HoloMathHelper.Pi;
            }
            Main.NewText(angle);

            NPC.rotation = angle;
        }


    }
}
