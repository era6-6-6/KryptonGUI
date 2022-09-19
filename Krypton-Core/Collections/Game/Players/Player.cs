using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Settings;

namespace Krypton_Core.Collections.Game.Players
{
    public class Player
    {
        public string? Username { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ID { get; set; }
        public bool isNpcs { get; set; }
        public NpcSettings Settings { get; set; }
        public int FactionID { get; set; }
        public int MaxHP { get; set; } = 0;
        public int MaxShd { get; set; } = 0;
        public int Hp { get; set; } = 0;
        public int Shd { get; set; } = 0;
        public bool isTaken { get; set; }
        public int TempId { get; set; }
        public string ClanTag { get; set; } = "";
        public int NanoHp { get; set; } = 0;
        public int MaxNano { get; set; } = 0;
        public Player Target { get; set; } = null;
        public bool Cloacked { get; set; }
        public int Level { get; set; }
        public int MapId { get; set; }
    }
}
