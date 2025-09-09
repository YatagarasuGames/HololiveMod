using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace HololiveMod.Projectiles.Boss
{
    internal class AmeliaClockArrow : ModProjectile
    {
        public override string Texture => "HololiveMod/Projectiles/Boss/AmeliaClockArrow";
        public float TimeBeforeLaunch
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public Vector2 velocity = Vector2.Zero;
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 120;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.hostile = true;

        }

        public override void AI()
        {
            if (TimeBeforeLaunch > 0)
            {
                if (velocity == Vector2.Zero) velocity = Projectile.velocity;
                Projectile.velocity = Vector2.Zero;
                TimeBeforeLaunch--;
                Projectile.timeLeft++;
                return;
            }
            Projectile.velocity = velocity;

        }
    }
}
