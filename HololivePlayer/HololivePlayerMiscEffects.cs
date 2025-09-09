using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HololiveMod.HololivePlayer
{
    public partial class HololivePlayer : ModPlayer
    {
        

        
        public override void PostUpdateMiscEffects()
        {
            MiscEffects();
        }

        

        private void MiscEffects()
        {
            Main.NewText($"MoveSpeed before function { Player.moveSpeed}");
            if (rapperMicrophoneCooldown > 0 && rapperMicrophoneBuffTime <= 0)
            {
                rapperMicrophoneCooldown--;
            }

            if(!rapperMicrophone && rapperMicrophoneBuffTime > 0)
            {
                rapperMicrophoneBuffTime = 0;
                rapperMicrophoneCooldown = Utilities.MiscUtils.SecondsToFrames(50);
            }

            if(rapperMicrophone && rapperMicrophoneBuffTime > 0)
            {
                rapperMicrophoneBuffTime--;
            }

            if (sunglasses && Main.dayTime)
            {
                Player.GetAttackSpeed(DamageClass.Generic) += 0.05f;
                Player.GetDamage(DamageClass.Generic) += 0.05f;
                Player.GetArmorPenetration(DamageClass.Generic) += 3; // 3 единицы, а не процент!
                Player.GetCritChance(DamageClass.Generic) += 5; // 5%, а не 0.05f
                Player.lifeRegen += 5;
                Player.manaRegen += 7;
                Player.moveSpeed += 0.05f;
                Main.NewText($"Move speed in function {Player.moveSpeed}");
            }
  
        }


    }
}
