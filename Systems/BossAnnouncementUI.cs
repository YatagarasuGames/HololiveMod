using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using System;
using Terraria.UI;
using System.Collections.Generic;
using ReLogic.Graphics;

namespace HololiveMod.Systems
{
    public class BossAnnouncementUI : ModSystem
    {
        public static string AnnouncementText = "";
        public static int DisplayTime = 0;
        public static int MaxDisplayTime = 180; // 3 секунды при 60 FPS
        public static Color TextColor = Color.Red;
        public static float Scale = 2f;

        public override void UpdateUI(GameTime gameTime)
        {
            if (DisplayTime > 0)
                DisplayTime--;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: Boss Announcement",
                    delegate
                    {
                        if (DisplayTime > 0)
                            DrawAnnouncement();
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        private void DrawAnnouncement()
        {
            if (string.IsNullOrEmpty(AnnouncementText))
                return;

            // Вычисляем прозрачность текста (плавное появление и исчезание)
            float opacity = 1f;
            if (DisplayTime > MaxDisplayTime - 30)
                opacity = (MaxDisplayTime - DisplayTime) / 30f;
            else if (DisplayTime < 30)
                opacity = DisplayTime / 30f;

            // Получаем шрифт
            DynamicSpriteFont font = FontAssets.DeathText.Value;

            // Измеряем размер текста
            Vector2 textSize = font.MeasureString(AnnouncementText) * Scale;

            // Вычисляем позицию текста (центр экрана)
            Vector2 position = new Vector2(
                Main.screenWidth / 2 - textSize.X / 2,
                Main.screenHeight / 3 - textSize.Y / 2
            );

            // Рисуем тень текста
            Vector2 shadowOffset = new Vector2(2, 2);
            Main.spriteBatch.DrawString(
                font,
                AnnouncementText,
                position + shadowOffset,
                Color.Black * opacity * 0.5f,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );

            // Рисуем основной текст
            Main.spriteBatch.DrawString(
                font,
                AnnouncementText,
                position,
                TextColor * opacity,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );
        }

        public static void ShowAnnouncement(string text, int duration = 180, Color? color = null, float scale = 2f)
        {
            AnnouncementText = text;
            DisplayTime = duration;
            MaxDisplayTime = duration;
            TextColor = color ?? Color.Red;
            Scale = scale;
        }
    }
}
