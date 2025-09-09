using HololiveMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace HololiveMod.Projectiles.Base
{
    /// <summary>
    /// Base beam class.
    /// <para>AI[0] reserved for RotationSpeed</para> 
    /// <para>localAI[0] reserved for Timer</para> 
    /// <para>localAI[1] reserved for beam Length</para> 
    /// 
    /// </summary>
    public abstract class BaseBeamProjectile : ModProjectile
    {
        public float RotationSpeed
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public float Timer
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public float Length
        {
            get => Projectile.localAI[1];
            set => Projectile.localAI[1] = value;
        }


        #region Override params
        public abstract float MaxLength { get; }
        public abstract float LifeTime { get; }
        public abstract float MaxScale { get; }
        public virtual float ScaleExpandRate => 4;


        public abstract Texture2D BeamBeginTexture { get; }
        public abstract Texture2D BeamMiddleTexture { get; }
        public abstract Texture2D BeamEndTexture { get; }
        public virtual Color LightCastColor => Color.Red;
        public virtual Color LaserOverlayColor => Color.Red * 0.9f;

        #endregion


        #region Overridable methods
        public virtual void Update()
        {
            AttachBeamToObject();

            Projectile.velocity = Projectile.velocity.SafeNormalize(-Vector2.UnitY);

            Timer++;
            if (Timer >= LifeTime)
            {
                Projectile.Kill();
                return;
            }

            ControlScale();
            MoveBeam();

            float idealLaserLength = MaxLength;
            Length = HoloMathHelper.Lerp(Length, idealLaserLength, 0.7f);


            DelegateMethods.v3_1 = LightCastColor.ToVector3();
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * Length, Projectile.width * Projectile.scale, DelegateMethods.CastLight);
        }

        public virtual void AttachBeamToObject() { }

        public virtual void MoveBeam()
        {
            float velocityRotation = Projectile.velocity.ToRotation(); //получаю угол(в радианах) от вектора скорости луча относительно начала единичной окр
            velocityRotation += RotationSpeed; //добавляю скорость вращения(рад)
            Projectile.rotation = velocityRotation - HoloMathHelper.PiOver2; //тк спрайт вертикальный, а тмодлоадер говно блять и у него начало отсчета идет справа на единичной окр, надо перейти из -pi/2 в -pi
            Projectile.velocity = velocityRotation.ToRotationVector2();
        }

        public virtual void ControlScale()
        {
            Projectile.scale = HoloMathHelper.Sin(Timer / LifeTime * HoloMathHelper.Pi) * ScaleExpandRate * MaxScale;
            if (Projectile.scale > MaxScale)
            {
                Projectile.scale = MaxScale;
            }
        }

        public virtual void LateUpdate() { }

        #endregion

        #region TModLoader hooks
        public override void AI()
        {
            Update();
            LateUpdate();
        }

        protected void DrawBeamWithColor(SpriteBatch spriteBatch, Color color, float scale)
        {
            Rectangle beamStart = BeamBeginTexture.Frame();
            Rectangle beamMid = BeamMiddleTexture.Frame();
            Rectangle beamEnd = BeamEndTexture.Frame();

            //Отрисовка старта луча
            spriteBatch.Draw(BeamBeginTexture,
                Projectile.Center - Main.screenPosition,
                beamStart,
                color,
                Projectile.rotation,
                BeamBeginTexture.Size() / 2,
                scale,
                SpriteEffects.None,
                0);

            float beamMidLength = Length;
            beamMidLength -= (BeamBeginTexture.Height + BeamEndTexture.Height) * scale;
            Vector2 beamCenter = Projectile.Center;
            beamCenter += Projectile.velocity * scale * BeamBeginTexture.Height / 2;

            if (beamMidLength > 0)
            {
                float beamMidSpriteOffset = beamMid.Height * scale;
                float currentLength = 0;

                while (currentLength + 1 < beamMidLength)
                {
                    spriteBatch.Draw(BeamMiddleTexture,
                        beamCenter - Main.screenPosition,
                        beamMid,
                        color,
                        Projectile.rotation,
                        BeamMiddleTexture.Width * 0.5f * Vector2.UnitX,
                        scale,
                        SpriteEffects.None,
                        0);
                    currentLength += beamMidSpriteOffset;
                    beamCenter += Projectile.velocity * beamMidSpriteOffset;

                }

            }

            if (Math.Abs(Length - MaxLength) < 30f)
            {
                spriteBatch.Draw(BeamEndTexture,
                beamCenter - Main.screenPosition,
                beamEnd,
                color,
                Projectile.rotation,
                BeamEndTexture.Size() / 2,
                scale,
                SpriteEffects.None,
                0);

            }



        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
                return false;

            DrawBeamWithColor(Main.spriteBatch, LaserOverlayColor, Projectile.scale);
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackMelee;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * Length, Projectile.Size.Length() * Projectile.scale, DelegateMethods.CutTiles);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
                return true;
            float _ = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Length, Projectile.Size.Length() * Projectile.scale, ref _);
        }

        public override bool ShouldUpdatePosition() => false;

        #endregion
    }
}
