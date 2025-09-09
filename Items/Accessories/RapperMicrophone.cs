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
    public class RapperMicrophone : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetNameOverride("Rapper's microphone");
            Item.width = 36;
            Item.height = 30;
            Item.accessory = true;
            
        }

        public override string Texture => "HololiveMod/Items/Accessories/Rapper's microphone";

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Hololive().rapperMicrophone = true;
        }
    }
}
