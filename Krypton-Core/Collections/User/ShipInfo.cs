using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class ShipInfo
    {
        public string ShipName { get; set; } = "ship";
        public int Speed { get; set; }
        public uint Config { get; set; }
        public bool Destroyed { get; set; } = false;

        public int Hp { get; set; }
        public int Shd { get; set; }
        public int MaxHp { get; set; }
        public int MaxShd { get; set; }
        public int NanoHp { get; set; }
        public int MaxNanoHp { get; set; }
        public int CargoSpace { get; set; }
        public int MaxCargoSpace { get; set; }
    }
}
