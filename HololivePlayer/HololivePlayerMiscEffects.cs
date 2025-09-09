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
        private bool _wereBuffsApplied = false;

        public override void PostUpdateMiscEffects()
        {
            MiscEffects();
        }

        private void MiscEffects()
        {
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

            if (sunglasses)
            {
                if (Main.dayTime)
                {
                    if (_wereBuffsApplied == true) return;
                    Player.GetAttackSpeed(DamageClass.Generic) += 0.05f;
                    Player.GetDamage(DamageClass.Generic) += 0.05f;
                    Player.GetArmorPenetration(DamageClass.Generic) += 0.03f;
                    Player.GetCritChance(DamageClass.Generic) += 0.05f;
                    Player.lifeRegen += 5;
                    Player.manaRegen += 7;
                    Player.moveSpeed *= 1.05f;
                    Player.maxRunSpeed *= 1.05f;
                    _wereBuffsApplied = true;
                }
                else
                {
                    if (_wereBuffsApplied == false) return;
                    Player.GetAttackSpeed(DamageClass.Generic) -= 0.05f;
                    Player.GetDamage(DamageClass.Generic) -= 0.05f;
                    Player.GetArmorPenetration(DamageClass.Generic) -= 0.03f;
                    Player.GetCritChance(DamageClass.Generic) -= 0.05f;
                    Player.lifeRegen -= 5;
                    Player.manaRegen -= 7;
                    Player.moveSpeed /= 1.05f;
                    Player.maxRunSpeed /= 1.05f;
                    _wereBuffsApplied = false;
                }
            }
            else
            {
                if (_wereBuffsApplied == false) return;
                Player.GetAttackSpeed(DamageClass.Generic) -= 0.05f;
                Player.GetDamage(DamageClass.Generic) -= 0.05f;
                Player.GetArmorPenetration(DamageClass.Generic) -= 0.03f;
                Player.GetCritChance(DamageClass.Generic) -= 0.05f;
                Player.lifeRegen -= 5;
                Player.manaRegen -= 7;
                Player.moveSpeed /= 1.05f;
                Player.maxRunSpeed /= 1.05f;
                _wereBuffsApplied = false;
            }
            
        }

    }
}
