using HololiveMod.Tiles.Ores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod.Items.Placeables.Ores
{
    class PureHateOre : ModItem
    {

        public override void SetDefaults()
        {
            Item.height = Item.width = 20;
            Item.maxStack = 9999;
            Item.value = Terraria.Item.buyPrice(silver: 3);
            Item.value = Terraria.Item.sellPrice(silver: 1);
            Item.consumable = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.createTile = ModContent.TileType<PureHateOreTile>();

        }



    }
}
