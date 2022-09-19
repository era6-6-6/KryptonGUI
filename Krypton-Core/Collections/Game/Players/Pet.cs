using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Players
{
    public class Pet
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public int Level { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int factionID { get; set; }
        public int Fuel { get; internal set; }
        public int Hp { get; internal set; }
        public int MaxFuel { get; internal set; }
        public int MaxShd { get; internal set; }
        public int Shd { get; internal set; }
        public int MaxHp { get; internal set; }
        public double Experience { get; internal set; }
    }
}
