using HololiveMod.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.Items.Accessories
{
    internal class YingYangSphere : ModItem
    {
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = Item.height = 30;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.50f;
            player.Hololive().YingYangSphere = true;
        }
    }
}
