using HololiveMod.Items.Placeables.Workbenches;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace HololiveMod.Tiles.Workbenches
{
    public class HoloOfficeTile : ModTile
    {
        public override void SetStaticDefaults()
        {

            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileID.Sets.DisableSmartCursor[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 21, 16, 16 };
            TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
            TileObjectData.addTile(Type);

            AddMapEntry(new Microsoft.Xna.Framework.Color(200, 200, 200), CreateMapEntryName());


        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int itemType = ModContent.ItemType<HoloOfficeWorkbench>();

            Item.NewItem(
                new EntitySource_TileBreak(i, j),
                i * 16,
                j * 16,
                48,
                32,
                ModContent.ItemType<HoloOfficeWorkbench>()
                );
        }
    }
}
