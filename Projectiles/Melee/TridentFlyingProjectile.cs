using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.Projectiles.Melee
{
    public class TridentFlyingProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 88;
            Projectile.height = 88;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;

            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] < 60f)
            {
                Projectile.velocity *= 1.01f;
            }
            else
            {
                Projectile.velocity *= 1.05f;
                if (Projectile.ai[0] >= 180)
                {
                    Projectile.Kill();
                }
            }

            //float rotateSpeed = 0.35f * (float)Projectile.direction;
            //Projectile.rotation += rotateSpeed;

            Lighting.AddLight(Projectile.Center, 0.75f, 0.75f, 0.75f);
        }
    }
}
