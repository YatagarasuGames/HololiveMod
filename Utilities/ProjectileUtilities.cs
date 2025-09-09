using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;

namespace HololiveMod.Utilities
{
    public static partial class Utilities
    {
        public static int NewProjectileBetter(float spawnX, float spawnY, float velocityX, float velocityY, int type, int damage, float knockback, int owner = -1, float ai0 = 0f, float ai1 = 0f, float ai2 = 0f)
        {
            if (owner == -1)
                owner = Main.myPlayer;
            int index = Projectile.NewProjectile(new EntitySource_WorldEvent(), spawnX, spawnY, velocityX, velocityY, type, damage, knockback, owner, ai0, ai1, ai2);
            if (index >= 0 && index < Main.maxProjectiles)
                Main.projectile[index].netUpdate = true;

            return index;
        }

        public static int NewProjectileBetter(Vector2 center, Vector2 velocity, int type, int damage, float knockback, int owner = -1, float ai0 = 0f, float ai1 = 0f, float ai2 = 0f)
        {
            return NewProjectileBetter(center.X, center.Y, velocity.X, velocity.Y, type, damage, knockback, owner, ai0, ai1, ai2);
        }
    }
}
