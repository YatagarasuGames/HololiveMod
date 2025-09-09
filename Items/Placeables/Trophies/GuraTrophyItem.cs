using HololiveMod.Tiles.Trophies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod.Items.Placeables.Trophies
{
    class GuraTrophyItem : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 50000;
            Item.rare = ItemRarityID.Blue;
            Item.createTile = ModContent.TileType<Tiles.Trophies.GuraTrophyTile>();
        }

    }
}
