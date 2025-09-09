using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class HololiveMod : Mod
	{

        public static readonly SoundStyle ClockArrowSpawn = new SoundStyle("HololiveMod/Sounds/clockTick", SoundType.Sound);


        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> GuardiansLaserShader = new Ref<Effect>(ModContent.Request<Effect>("HololiveMod/Effects/GuardiansLaserShader", AssetRequestMode.ImmediateLoad).Value);
                GameShaders.Misc["HololiveMod:GuardiansLaserShader"] = new MiscShaderData(GuardiansLaserShader, "TrailPass");
            }
        }

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                // AddBoss, bossname, order or value in terms of vanilla bosses, inline method for retrieving downed value.
                bossChecklist.Call("AddBoss", "Amelia Watson", 5.5f, (Func<bool>)(() => true));
                //bossChecklist.Call(....
                // To include a description:
                //bossChecklist.Call("AddBossWithInfo", "Amelia Watson", 5.5f, (Func<bool>)(() => true), "Use a [i:" + ("StrongFlareGun") + "] at night in the Example Biome");
            }
        }
    }
}
