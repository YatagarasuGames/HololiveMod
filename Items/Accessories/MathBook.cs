using HololiveMod.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.Items.Accessories
{
    public class MathBook : ModItem
    {

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 40;
            Item.height = 48;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Hololive().mathBook = true;
            player.maxMinions++;
            player.statLifeMax2 += 25;
            player.GetDamage(DamageClass.Summon) += 0.1f;
        }

    }
}
