using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.ID;
using HololiveMod.Tiles.Ores;

namespace HololiveMod.Systems
{
    internal class HololiveOreGeneration : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                // Вставляем генерацию после "Shinies" (стандартные руды)
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Example Ore", GenerateExampleOre));
            }

            // Находим индекс прохода, после которого будем добавлять наши структуры
            int index = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));

            if (index != -1)
            {
                // Добавляем наш проход генерации структур
                tasks.Insert(index + 1, new PassLegacy("Generating Custom Structures", GenerateStructures));
            }
        }

        private void GenerateExampleOre(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Generating Example Ore";

            // Настройки генерации
            int tileType = ModContent.TileType<HoloOreTile>();
            int minHeight = 0; // Минимальная высота (под землёй)
            int maxHeight = (int)(Main.maxTilesY * 0.35f); // Глубина: 35% от мира

            // Генерация в виде жил
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.001); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next(minHeight, maxHeight);

                // Размер жилы
                WorldGen.TileRunner(
                    x, y,
                    WorldGen.genRand.Next(3, 6), // Размер
                    WorldGen.genRand.Next(2, 5), // Количество
                    tileType
                );
            }
        }



        private void GenerateStructures(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Generating custom structures";

            // Количество структур для генерации
            int structureCount = 10;

            for (int i = 0; i < structureCount; i++)
            {
                // Выбираем случайные координаты в подземелье
                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int y = WorldGen.genRand.Next((int)Main.worldSurface + 100, Main.maxTilesY - 200);

                // Проверяем, что место подходит для структуры
                if (IsValidLocation(x, y))
                {
                    // Генерируем структуру
                    WorldGeneration.StructureGenerator.GenerateExampleStructure(x, y);
                }

                // Обновляем прогресс
                progress.Set((float)(i + 1) / structureCount);
            }
        }

        private bool IsValidLocation(int x, int y)
        {
            // Проверяем, что область достаточно плоская для структуры
            int width = 15;
            int height = 10;

            for (int i = x - 2; i < x + width + 2; i++)
            {
                for (int j = y - 2; j < y + height + 2; j++)
                {
                    if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY)
                        return false;

                    // Проверяем, что нет жидкости
                    if (Main.tile[i, j].LiquidAmount > 0)
                        return false;

                    // Проверяем, что нет опасных плиток
                    if (Main.tile[i, j].TileType == TileID.Lavafall ||
                        Main.tile[i, j].TileType == TileID.DemonAltar)
                        return false;
                }
            }

            return true;
        }

    }
}
