using HololiveMod.Tiles.Workbenches;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod.Items.Placeables.Workbenches
{
    public class HoloOfficeWorkbench : ModItem
    {


        public override void SetDefaults()
        {
            Item.SetNameOverride("Hololive office");
            Item.width = 26;
            Item.height = 40;

            Item.useTime = 15;
            Item.useAnimation = 15;

            Item.useStyle = ItemUseStyleID.Swing;

            Item.autoReuse = true;
            Item.useTurn = true;

            Item.maxStack = 999;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<HoloOfficeTile>();
            Item.placeStyle = 0;
        }
    }
}
