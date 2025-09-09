using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using HololiveMod.Tiles.Ores;

namespace HololiveMod.Items.Placeables.Ores
{
    class PureHateBar : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 36;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.consumable = true;
            Item.value = Terraria.Item.sellPrice(gold: 1);
            Item.value = Terraria.Item.buyPrice(gold: 3);
            Item.maxStack = 999;

            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.createTile = ModContent.TileType<PureHateBarTile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<PureHateOre>(4).
                AddTile(TileID.Furnaces).Register();
        }

    }
}
