using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.Game.Ammo;

namespace Krypton_Core.Settings
{
    public class NpcSettings
    {
        public string Ammo { get; set; } = AmmoCollection.LCB_10;
        public int Radius { get; set; } = 500;
        public bool Drag { get; set; } = false;
    }
}
