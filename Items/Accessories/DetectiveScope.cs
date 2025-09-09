using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using HololiveMod.Utilities;

namespace HololiveMod.Items.Accessories
{
    public class DetectiveScope : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetNameOverride("Detective's scope");
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Ranged) += 0.15f;
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.GetCritChance(DamageClass.Ranged) += 2f;
            player.Hololive().rangedAmmoCost *= 0.5f;
            
        }


    }
}
