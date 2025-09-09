using HololiveMod.Projectiles.Boss;
using HololiveMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod.NPC.Bosses
{
    /// NPC.ai[1] - таймер текущей фазы
    /// NPC.ai[2] - таймер задержки начала следующей фазы
    ///
    internal class AmeliaWatson : ModNPC
    {
        public float AIPhase
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;

        }
        public float PhaseTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public float DelayBetweenPhases
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        private enum AttackPhase
        {
            ChangingPhase = -1,
            RoundAttack = 0,
            XAttack = 1,
            SyringeAttack = 2,
            BulletFastBursts = 3,
            ClockAttack = 4,
            BeamAttack = 5,



        }

        private AttackPhase attackPhase = AttackPhase.ChangingPhase;  

        public static Asset<Texture2D> AmeliaPhase1;
        public static Asset<Texture2D> AmeliaPhase2;

        public bool hasSpawnedBuba = false;
        int bubaID;


        private float flightPath = 0;
        private bool timeStopUsed = false;

        public float phaseTime = 300;

        public float maxDashSpeedToStartAvoid = 3f;
        public float minAvoidDistance;

        private float spinAttackRotateAngle = 15;
        public override void SetDefaults()
        {
            NPC.width = 90;
            NPC.height = 170;
            NPC.damage = 50;
            NPC.defense = 0;
            NPC.lifeMax = 100_000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100 * 100 * 100 * 2;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = MusicID.Boss3;

            AmeliaPhase1 = ModContent.Request<Texture2D>("HololiveMod/NPC/Bosses/AmeliaWatsonPhase1", AssetRequestMode.AsyncLoad);
            AmeliaPhase2 = ModContent.Request<Texture2D>("HololiveMod/NPC/Bosses/AmeliaWatsonPhase2", AssetRequestMode.AsyncLoad);
        }


        public override string Texture => "HololiveMod/NPC/Bosses/AmeliaWatsonPhase1";

        private Texture2D CurrentTexture
        {
            get
            {
                if (NPC.life <= 0.9f * NPC.lifeMax)
                    return AmeliaPhase2.Value;
                return AmeliaPhase1.Value;
            }
        }

        float angle = 0;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (CurrentTexture == null)
                return true;

            // Получаем рамку для анимации (если есть)
            Rectangle frame = NPC.frame;
            if (NPC.frame.Width == 0 || NPC.frame.Height == 0)
                frame = new Rectangle(0, 0, CurrentTexture.Width, CurrentTexture.Height);

            // Вычисляем origin
            Vector2 origin = frame.Size() / 2f;

            // Определяем эффект отражения (если нужно)
            SpriteEffects effects = SpriteEffects.None;

            // Рисуем NPC с правильной текстурой
            spriteBatch.Draw(CurrentTexture, NPC.Center - Main.screenPosition, frame,
                             drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);

            // Отменяем стандартную отрисовку
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            if(player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if(player.dead || !player.active)
                {
                    NPC.velocity = new Vector2(0, -10);
                    return;
                }
            }
            float lifeRatio = NPC.life / NPC.lifeMax;
            float distanceX = HoloMathHelper.Abs(NPC.Center.X - player.Center.X);


            // Change X direction of movement
            if (flightPath == 0)
            {
                if (NPC.Center.X < player.Center.X)
                {
                    flightPath = 1;
                }
                else
                {
                    flightPath = -1;
                }
            }

            // Velocity and acceleration
            float speedIncreaseTimer = 75f;
            float accelerationBoost = 0.4f;
            float velocityBoost = 6f;
            float acceleration = 1.1f + accelerationBoost;
            float velocity = 16f;








            // Distance needed from target to change direction
            float changeDirectionThreshold = 400f;


            // Change X movement path if far enough away from target
            if (NPC.Center.X < player.Center.X && flightPath < 0 && distanceX > changeDirectionThreshold)
                flightPath = 0;
            if (NPC.Center.X > player.Center.X && flightPath > 0 && distanceX > changeDirectionThreshold)
                flightPath = 0;




            switch (AIPhase)
            {
                case 0:
                    Phase3AI(player);
                    break;
                case 1:
                    Phase2AI(player);
                    break;
                case 2:
                    TimeStopDialogueAI(player);
                    break;
                case 3:
                    Phase3AI(player);
                    break;
            }
        }
        private void Phase1AI(Player player)
        {
            //if (PhaseTimer == 0) BossAnnouncementUI.ShowAnnouncement("The Time Detective\nAmelia Watson", 180, Color.Red, 2f);
            switch (attackPhase)
            {
                case AttackPhase.RoundAttack:
                    if (NPC.localAI[0] == 0)
                    {
                        NPC.localAI[1] = Main.rand.NextFloat(-20f, 20f); // Случайное смещение по X
                        NPC.localAI[2] = Main.rand.NextFloat(-20f, 20f); // Случайное смещение по Y
                        NPC.localAI[0] = 30; // Таймер для обновления смещения (30 тиков = 0.5 секунды)
                    }
                    else
                    {
                        NPC.localAI[0]--;
                    }

                    // Вычисляем целевую позицию справа от игрока со смещением
                    Vector2 targetPosition = new Vector2(
                        player.Center.X + 700f + NPC.localAI[1],
                        player.Center.Y + NPC.localAI[2]
                    );

                    // Рассчитываем вектор до цели
                    Vector2 distanceToTarget = targetPosition - NPC.Center;

                    // Используем SmoothMovement для плавного перемещения
                    NPCUtils.SmoothMovement(
                        NPC,
                        100f,                // Минимальная дистанция для начала замедления
                        distanceToTarget,    // Вектор до цели
                        10f,                  // Базовая скорость
                        0.6f,                // Ускорение
                        true                 // Использовать SimpleFlyMovement
                    );
                    break;
                case AttackPhase.XAttack:
                    //Circle movement

                    float flyRadius = 400;
                    float circleSpeed = HoloMathHelper.ToRadians(3); //3 угол в кадр


                    angle += circleSpeed;
                    if (angle > HoloMathHelper.TwoPi) angle -= HoloMathHelper.TwoPi;

                    Vector2 nextPosition = new Vector2(HoloMathHelper.Cos(angle) * flyRadius, HoloMathHelper.Sin(angle) * flyRadius);

                    Vector2 targetDirection = player.Center + nextPosition;

                    float smoothness = 0.6f;

                    NPC.velocity = (targetDirection - NPC.Center) * smoothness;
                    break;
            }


            switch (attackPhase)
            {
                case AttackPhase.ChangingPhase:

                    if(DelayBetweenPhases != 0) DelayBetweenPhases--;
                    if (DelayBetweenPhases > 0) return;

                    attackPhase = (AttackPhase)Main.rand.Next(2);
                    break;
                case AttackPhase.RoundAttack:

                    if (PhaseTimer % 25 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.AbigailAttack, NPC.Center);
                        int numberOfProjectiles = 18; 

                        for (int i = 0; i < numberOfProjectiles; i++)
                        {
                            Vector2 shootDirection = Vector2.UnitX.RotatedBy(MathHelper.TwoPi / numberOfProjectiles * i);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, shootDirection * 10f, ProjectileID.CursedFlameHostile, NPC.damage / 2, 1f, Main.myPlayer);
                        }
                    }

                    PhaseTimer++;
                    if (PhaseTimer >= phaseTime)
                    {
                        attackPhase = AttackPhase.ChangingPhase;
                        NPC.TargetClosest();
                        PhaseTimer = 0;
                        DelayBetweenPhases = 30;
                    }

                    break;
                case AttackPhase.XAttack:

                    if (PhaseTimer % 20 == 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Vector2 shootDirection = Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i + MathHelper.PiOver4 + MathHelper.ToRadians(spinAttackRotateAngle));
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, shootDirection * 8f, ProjectileID.CursedFlameHostile, NPC.damage / 2, 1f, Main.myPlayer);

                        }
                        spinAttackRotateAngle += 15;
                        spinAttackRotateAngle %= 360;
                    }

                    PhaseTimer++;
                    if (PhaseTimer >= phaseTime)
                    {
                        attackPhase = AttackPhase.ChangingPhase;
                        NPC.TargetClosest();
                        PhaseTimer = 0;
                        DelayBetweenPhases = 30;
                    }

                    break;
            }


            if (NPC.life < NPC.lifeMax * 0.99f)
            {
                AIPhase = 3;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                NPC.netUpdate = true;
                attackPhase = AttackPhase.ChangingPhase;
                PhaseTimer = 0;
            }

        }
        int[] beamIds = new int[4];
        private void Phase2AI(Player player)
        {
            // Спавн Бубы если еще не спавнен
            if (!hasSpawnedBuba && Main.netMode != NetmodeID.MultiplayerClient)
            {
                bubaID = Terraria.NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Buba>());
                Main.npc[bubaID].localAI[0] = NPC.whoAmI;
                Main.npc[bubaID].localAI[1] = player.whoAmI;
                hasSpawnedBuba = true;
                NPC.netUpdate = true;
                NPC.dontTakeDamage = true;
            }

            if (NPC.localAI[0] == 0)
            {
                NPC.localAI[1] = Main.rand.NextFloat(-20f, 20f); // Случайное смещение по X
                NPC.localAI[2] = Main.rand.NextFloat(-20f, 20f); // Случайное смещение по Y
                NPC.localAI[0] = 30; // Таймер для обновления смещения (30 тиков = 0.5 секунды)
            }
            else
            {
                NPC.localAI[0]--;
            }

            // Вычисляем целевую позицию справа от игрока со смещением
            Vector2 targetPosition = new Vector2(
                player.Center.X + 700f + NPC.localAI[1],
                player.Center.Y + NPC.localAI[2]
            );

            // Рассчитываем вектор до цели
            Vector2 distanceToTarget = targetPosition - NPC.Center;

            // Используем SmoothMovement для плавного перемещения
            NPCUtils.SmoothMovement(
                NPC,
                100f,                // Минимальная дистанция для начала замедления
                distanceToTarget,    // Вектор до цели
                10f,                  // Базовая скорость
                0.6f,                // Ускорение
                true                 // Использовать SimpleFlyMovement
            );

            if (!Main.npc[bubaID].active)
            {
                AIPhase = 2;
                PhaseTimer = 0;
            }


        }

        int[] clockArrowsIds = new int[12];
        private void Phase3AI(Player player)
        {
            /*if (NPC.localAI[0] == 0)
            {
                NPC.localAI[1] = Main.rand.NextFloat(-20f, 20f); // Случайное смещение по X
                NPC.localAI[2] = Main.rand.NextFloat(-20f, 20f); // Случайное смещение по Y
                NPC.localAI[0] = 30; // Таймер для обновления смещения (30 тиков = 0.5 секунды)
            }
            else
            {
                NPC.localAI[0]--;
            }

            // Вычисляем целевую позицию справа от игрока со смещением
            Vector2 targetPosition = new Vector2(
                player.Center.X + 700f + NPC.localAI[1],
                player.Center.Y + NPC.localAI[2]
            );

            // Рассчитываем вектор до цели
            Vector2 distanceToTarget = targetPosition - NPC.Center;

            // Используем SmoothMovement для плавного перемещения
            NPCUtils.SmoothMovement(
                NPC,
                100f,                // Минимальная дистанция для начала замедления
                distanceToTarget,    // Вектор до цели
                10f,                  // Базовая скорость
                0.6f,                // Ускорение
                true                 // Использовать SimpleFlyMovement
            );*/

            switch (attackPhase)
            {
                case AttackPhase.ChangingPhase:

                    if (DelayBetweenPhases != 0) DelayBetweenPhases--;
                    if (DelayBetweenPhases > 0) return;



                    attackPhase = (AttackPhase)Main.rand.Next(6);
                    if(attackPhase == AttackPhase.SyringeAttack) attackPhase = AttackPhase.XAttack;

                    if (attackPhase == AttackPhase.ClockAttack)
                        hasArrowsSpawned = false;

                    break;

                case AttackPhase.BeamAttack:
                    if (beamIds[0] == 0)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_WitherBeastAuraPulse, NPC.Center);
                        for (int i = 0; i < 4; i++)
                        {
                            Vector2 beamDirection = (HoloMathHelper.PiOver2 * i).ToRotationVector2();
                            beamIds[i] = Utilities.Utilities.NewProjectileBetter(NPC.Center, beamDirection, ModContent.ProjectileType<AmeliaBeam>(), NPC.damage * 2, 0, -1, 0.02f, NPC.whoAmI);
                            
                        }
                        return;
                    }
                    PhaseTimer++;

                    if (PhaseTimer >= phaseTime)
                    {
                        attackPhase = AttackPhase.ChangingPhase;
                        NPC.TargetClosest();
                        PhaseTimer = 0;
                        DelayBetweenPhases = 30;
                        for (int i = 0; i<beamIds.Length; i++) beamIds[i] = 0;
                    }

                    break;

                case AttackPhase.ClockAttack:
                    if (!hasArrowsSpawned)
                    {
                        

                        //Main.NewText("Can spawn arrows");
                        int firstArrowToSkip = Main.rand.Next(7);
                        int secondArrowToSkip = Main.rand.Next(7);
                        while(secondArrowToSkip == firstArrowToSkip) secondArrowToSkip = Main.rand.Next(7);

                        if(PhaseTimer % 3 == 0)
                        {
                            if (clockArrowIndex == firstArrowToSkip || clockArrowIndex == secondArrowToSkip)
                            {
                                clockArrowIndex++;
                                return;
                            }
                            if(clockArrowIndex >= 13)
                            {
                                hasArrowsSpawned = true;
                                return;
                            }

                            if (Main.netMode != NetmodeID.Server)
                            {
                                SoundEngine.PlaySound(HololiveMod.ClockArrowSpawn, NPC.Center);
                            }

                            float angleInRadians = HoloMathHelper.Pi / 6 * clockArrowIndex;
                            //Main.NewText(HoloMathHelper.ToDegrees(angleInRadians));
                            //Main.NewText(new Vector2(-HoloMathHelper.Sin(angleInRadians) * 140, HoloMathHelper.Cos(angleInRadians) * 140));
                            Vector2 direction = new Vector2(-HoloMathHelper.Sin(angleInRadians) * 140, HoloMathHelper.Cos(angleInRadians) * 140);
                            // Создаем проектиль с нулевой скоростью
                            int projectile = Utilities.Utilities.NewProjectileBetter(
                                NPC.Center,
                                Vector2.Normalize(direction) * 60f,
                                ModContent.ProjectileType<AmeliaClockArrow>(),
                                NPC.damage / 2,
                                0,
                                -1,
                                60 - PhaseTimer
                            );

                            var createdProjectile = Main.projectile[projectile];

                            // Устанавливаем поворот
                            createdProjectile.rotation = angleInRadians;


                            // Смещаем позицию
                            createdProjectile.Center += direction;

                            // Обновляем сетевую синхронизацию
                            createdProjectile.netUpdate = true;
                            clockArrowIndex++;
                        }

                        //Main.NewText("Spawning Clock Arrows");
                        
                    }
                    PhaseTimer++;
                    if (PhaseTimer >= phaseTime)
                    {
                        attackPhase = AttackPhase.ChangingPhase;
                        NPC.TargetClosest();
                        PhaseTimer = 0;
                        DelayBetweenPhases = 30;
                        hasArrowsSpawned = false;
                        clockArrowIndex = 0;
                    }
                    break;
                case AttackPhase.RoundAttack:

                    if (PhaseTimer % 25 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.AbigailAttack, NPC.Center);
                        int numberOfProjectiles = 18;

                        for (int i = 0; i < numberOfProjectiles; i++)
                        {
                            Vector2 shootDirection = Vector2.UnitX.RotatedBy(MathHelper.TwoPi / numberOfProjectiles * i);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, shootDirection * 10f, ProjectileID.CursedFlameHostile, NPC.damage / 2, 1f, Main.myPlayer);
                        }
                    }

                    PhaseTimer++;
                    if (PhaseTimer >= phaseTime)
                    {
                        attackPhase = AttackPhase.ChangingPhase;
                        NPC.TargetClosest();
                        PhaseTimer = 0;
                        DelayBetweenPhases = 30;
                    }

                    break;


                case AttackPhase.XAttack:

                    if (PhaseTimer % 10 == 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Vector2 shootDirection = Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i + MathHelper.PiOver4 + MathHelper.ToRadians(spinAttackRotateAngle));
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, shootDirection * 8f, ProjectileID.CursedFlameHostile, NPC.damage / 2, 1f, Main.myPlayer);

                        }
                        spinAttackRotateAngle += 15;
                        spinAttackRotateAngle %= 360;
                    }

                    PhaseTimer++;
                    if (PhaseTimer >= phaseTime)
                    {
                        attackPhase = AttackPhase.ChangingPhase;
                        NPC.TargetClosest();
                        PhaseTimer = 0;
                        DelayBetweenPhases = 30;
                    }

                    break;

                case AttackPhase.BulletFastBursts:

                    bulletBurstTimer++;

                    if(bulletsInBurstFired == 0)
                    {
                        if (bulletBurstTimer == 45)
                        {
                            bulletsInBurstFired++;
                            Utilities.Utilities.NewProjectileBetter(NPC.Center, Vector2.Normalize(player.Center - NPC.Center + player.velocity * 10) * 15, ProjectileID.CursedFlameHostile, NPC.damage / 2, 0);
                            bulletBurstTimer = 0;
                            return;
                        }
                        else return;
                        
                    }

                    if(bulletsInBurstFired == bulletsInBurst)
                    {
                        bulletBurstTimer = 0;
                        bulletsInBurstFired = 0;
                    }

                    else
                    {
                        if(bulletBurstTimer == 5)
                        {
                            bulletsInBurstFired++;
                            Utilities.Utilities.NewProjectileBetter(NPC.Center, Vector2.Normalize(player.Center - NPC.Center + player.velocity * 10) * 15, ProjectileID.CursedFlameHostile, NPC.damage / 2, 0);
                            bulletBurstTimer = 0;
                            return;
                        }
                    }

                    PhaseTimer++;
                    if (PhaseTimer >= phaseTime)
                    {
                        attackPhase = AttackPhase.ChangingPhase;
                        NPC.TargetClosest();
                        PhaseTimer = 0;
                        DelayBetweenPhases = 30;
                    }

                    break;
            }



            //атака шприцами
        }
        private int bulletBurstTimer = 0;
        private int bulletsInBurstFired;
        private int bulletsInBurst = 7;
        float clockArrowIndex = 0;
        private bool hasArrowsSpawned = false;
        private void TimeStopDialogueAI(Player player)
        {
            if(PhaseTimer == 0) { Main.NewText("Well... lets start from begining", Color.Yellow); NPC.dontTakeDamage=false; }
            if (PhaseTimer == 180) { TimeStopSystem.ActivateTimeStop(600); }
            TimeStopSystem.Update();

            // Вычисляем целевую позицию справа от игрока со смещением
            Vector2 targetPosition = new Vector2(
                player.Center.X + 500f + NPC.localAI[1],
                player.Center.Y + NPC.localAI[2]
            );

            // Рассчитываем вектор до цели
            Vector2 distanceToTarget = targetPosition - NPC.Center;

            // Используем SmoothMovement для плавного перемещения
            NPCUtils.SmoothMovement(
                NPC,
                100f,                // Минимальная дистанция для начала замедления
                distanceToTarget,    // Вектор до цели
                10f,                  // Базовая скорость
                0.6f,                // Ускорение
                true                 // Использовать SimpleFlyMovement
            );

            if(PhaseTimer > 300)
            {
                if(PhaseTimer % 10 == 0)
                {
                    int lifeToAdd = (int)(NPC.lifeMax * 0.01);
                    NPC.life = int.Clamp(NPC.life + lifeToAdd, 0, NPC.lifeMax);
                }
                
                
            }


            PhaseTimer++;
            
        }


    }
}
