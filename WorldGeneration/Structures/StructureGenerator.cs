using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace HololiveMod.WorldGeneration
{
    public static class StructureGenerator
    {
        public static void GenerateExampleStructure(int x, int y)
        {
            // Размеры структуры
            int width = 15;
            int height = 10;

            // Основной материал структуры
            ushort tileType = TileID.Diamond;
            ushort wallType = WallID.LunarBrickWall;

            // Создаем фундамент
            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    // Очищаем область
                    WorldGen.KillTile(i, j);
                    WorldGen.KillWall(i, j);

                    // Создаем пол и стены
                    if (j == y + height - 1 || i == x || i == x + width - 1 || j == y)
                    {
                        WorldGen.PlaceTile(i, j, tileType, true, true);
                    }
                    else
                    {
                        WorldGen.PlaceWall(i, j, wallType, true);
                    }
                }
            }

            // Добавляем вход
            WorldGen.KillTile(x + width / 2, y + height - 1);
            WorldGen.KillTile(x + width / 2 - 1, y + height - 1);
            WorldGen.KillTile(x + width / 2 + 1, y + height - 1);

            // Добавляем сундук
            //PlaceChest(x + 2, y + height - 2, false, 0);

            // Добавляем факелы
            WorldGen.PlaceTile(x + 2, y + 2, TileID.Torches);
            WorldGen.PlaceTile(x + width - 3, y + 2, TileID.Torches);
        }

        /*private static int PlaceChest(int x, int y, bool notNearOtherChests = false, int style = 0)
        {
            // Пытаемся разместить сундук
            int chestIndex = WorldGen.PlaceChest(x, y, (ushort)ModContent.TileType<Tiles.ExampleChest>(), notNearOtherChests, style);

            if (chestIndex > -1)
            {
                // Заполняем сундук предметами
                for (int i = 0; i < 5; i++)
                {
                    int itemType = ItemID.HealingPotion;
                    int stack = WorldGen.genRand.Next(1, 5);
                    Main.chest[chestIndex].item[i].SetDefaults(itemType);
                    Main.chest[chestIndex].item[i].stack = stack;
                }
            }

            return chestIndex;
        }*/
    }
}
