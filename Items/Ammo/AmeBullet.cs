using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Terraria.DataStructures;
using HololiveMod.Projectiles.Range;

namespace HololiveMod.Items.Ammo
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class AmeBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetNameOverride("Time bullet");
            Tooltip.Format("A unique bullet from a girl called herself a \"time traveller detective\". Slow down enemies on hit");
            Item.width = 8;
            Item.height = 8;

            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 1.25f;

            Item.maxStack = 999;
            Item.consumable = true;

            Item.ammo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<AmeBulletProjectile>();
        }
    }
}

