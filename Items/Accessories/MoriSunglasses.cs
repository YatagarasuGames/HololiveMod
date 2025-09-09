using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.Items.Accessories
{
    public class MoriSunglasses : ModItem
    {
        private bool _wereBuffsApplied = false;
        public override void SetDefaults()
        {
            Item.SetNameOverride("Rapper's sunglasses");
            Item.width = 40;
            Item.height = 25;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.10f;
        }

    }
}
