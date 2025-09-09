using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.Items.Accessories
{
    public class MagnifyingGlass : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 30;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
        }
    }
}
