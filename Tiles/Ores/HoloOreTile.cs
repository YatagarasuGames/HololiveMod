using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Audio;
using HololiveMod.Items.Placeables.Ores;

namespace HololiveMod.Tiles.Ores
{
    public class HoloOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 900;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 350;

            AddMapEntry(new Color(200, 200, 200), CreateMapEntryName());

            DustType = DustID.Tungsten;
            MineResist = 1.5f;
            MinPick = 100;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int itemType = ModContent.ItemType<HoloOre>();

            Item.NewItem(
                new EntitySource_TileBreak(i, j),
                i * 16,
                j * 16,
                48,
                32,
                ModContent.ItemType<HoloOre>()
                );
        }
    }
}
