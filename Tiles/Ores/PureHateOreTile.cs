using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using HololiveMod.Items.Placeables.Ores;
using Terraria.DataStructures;

namespace HololiveMod.Tiles.Ores
{
    public class PureHateOreTile : ModTile
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

            HitSound = SoundID.Tink;

            AddMapEntry(Color.Red, CreateMapEntryName());

            DustType = DustID.Tungsten;
            MineResist = 1.5f;
            MinPick = 70;

            
        }

    }
}
