using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using HololiveMod.Tiles.Trophies;

namespace HololiveMod.Items.Placeables.Trophies
{
    public class MoriTrophy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.SetNameOverride("Mori Trophy");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
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
            Item.createTile = ModContent.TileType<MoriTrophyTile>();
        }
    }
}
