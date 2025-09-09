using HololiveMod.Projectiles.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.Projectiles.Boss
{
    internal class AmeliaBeam : BaseBeamProjectile
    {
        private Effect shaderEffect;
        private bool shaderInitialized = false;

        public float InitialDirection = -100f;
        public int OwnerIndex => (int)Projectile.ai[1];
        public override float LifeTime => 280;
        public override Color LaserOverlayColor => Color.White;
        public override Color LightCastColor => Color.White;
        public override Texture2D BeamBeginTexture => ModContent.Request<Texture2D>("HololiveMod/NPC/Bosses/PrimeBeamBegin", AssetRequestMode.ImmediateLoad).Value;
        public override Texture2D BeamMiddleTexture => ModContent.Request<Texture2D>("HololiveMod/NPC/Bosses/PrimeBeamMid", AssetRequestMode.ImmediateLoad).Value;
        public override Texture2D BeamEndTexture => ModContent.Request<Texture2D>("HololiveMod/NPC/Bosses/PrimeBeamEnd", AssetRequestMode.ImmediateLoad).Value;
        public override string Texture => "HololiveMod/NPC/Bosses/PrimeBeamBegin";
        public override float MaxLength => 3100f;
        public override float MaxScale => 4f;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 40;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = (int)LifeTime;

        }

        private void InitializeShader()
        {
            if (shaderInitialized) return;

            // Загружаем шейдер
            shaderEffect = ModContent.Request<Effect>("HololiveMod/Effects/YourShaderName", AssetRequestMode.ImmediateLoad).Value;

            // Загружаем текстуру для карты затухания (если нужно)
            // var fadeMap = ModContent.Request<Texture2D>("YourMod/Textures/FadeMap", AssetRequestMode.ImmediateLoad).Value;
            // shaderEffect.Parameters["uImage1"].SetValue(fadeMap);

            shaderInitialized = true;
        }


        public override void AttachBeamToObject()
        {
            Main.NewText($"Owner{OwnerIndex}");
            //if (InitialDirection == -100f)
            //InitialDirection = Projectile.velocity.ToRotation();

            if (!Main.npc.IndexInRange(OwnerIndex))
            {
                Projectile.Kill();
                Main.NewText("Kill");
                return;
            }

            Projectile.Center = Main.npc[OwnerIndex].Center;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D glowTexture = ModContent.Request<Texture2D>("HololiveMod/BloomCircle").Value;
            Color glowColor = Color.Brown;
            glowColor.A = 0;

            Main.EntitySpriteDraw(glowTexture, Projectile.Center - Main.screenPosition, null, glowColor, 0f, glowTexture.Size() * 0.5f, 1.3f * Projectile.scale, SpriteEffects.None, 0);
            return base.PreDraw(ref lightColor);


        }





    }
}
