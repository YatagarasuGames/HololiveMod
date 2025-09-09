using HololiveMod.Buffs;
using HololiveMod.Systems;
using HololiveMod.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod.HololivePlayer
{
    public partial class HololivePlayer : ModPlayer
    {
        public float rangedAmmoCost = 1f;

        public bool rapperMicrophone = false;
        public int rapperMicrophoneCooldown = 0;
        public int rapperMicrophoneBuffTime = 0;

        public bool YingYangSphere = false;
        public bool sunglasses = false;
        public bool mathBook = false;
        public bool whirlpool = false;
        public bool giantFin = false;
        public bool magnifyingGlass = false;

        public bool wasWhirlpoolBuffApplied = false;


        public override void ResetEffects()
        {
            rapperMicrophone = false;
            YingYangSphere = false;
            sunglasses = false;
            mathBook = false;
            whirlpool = false;
            giantFin = false;
            magnifyingGlass = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if(rapperMicrophone && Player.whoAmI == Main.myPlayer && HololiveKeybinds.RapperMicrophoneBuffHotKey.JustPressed && rapperMicrophoneCooldown <= 0)
            {
                rapperMicrophoneCooldown = MiscUtils.SecondsToFrames(50);
                rapperMicrophoneBuffTime = MiscUtils.SecondsToFrames(10);

                foreach (Player target in Main.player)
                {
                    if (target.active && !target.dead && Vector2.Distance(Player.Center, target.Center) < 300f)
                    {
                        target.AddBuff(ModContent.BuffType<MeraMeraMicrophoneBuff>(), rapperMicrophoneBuffTime);

                        for (int i = 0; i < 10; i++)
                        {
                            Dust.NewDust(target.position, target.width, target.height, DustID.GoldFlame);
                        }
                    }
                }
            }
        }

        public override void ModifyHitByNPC(Terraria.NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (YingYangSphere) modifiers.FinalDamage *= 1.5f;
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (YingYangSphere) modifiers.FinalDamage *= 1.5f;
        }

        public override void ModifyHitNPC(Terraria.NPC target, ref Terraria.NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(target, ref modifiers);
        }

    }
}
