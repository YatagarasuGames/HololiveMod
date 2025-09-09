using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace HololiveMod.Systems
{
    public class HololiveKeybinds : ModSystem
    {
        public static ModKeybind RapperMicrophoneBuffHotKey { get; private set; }

        public override void Load()
        {
            RapperMicrophoneBuffHotKey = KeybindLoader.RegisterKeybind(Mod, "ActivateRapperMicrophoneBuff", "B");
        }

        public override void Unload()
        {
            RapperMicrophoneBuffHotKey = null;
        }
    }
}
