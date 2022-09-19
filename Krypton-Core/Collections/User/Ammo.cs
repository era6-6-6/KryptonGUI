using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class Ammo
    {
        public double LCB10 { get; set; }
        public double MCB25 { get; set; }
        public double MCB50 { get; set; }
        public double UCB100 { get; set; }
        public double SAB { get; set; }

        public List<AmmoType> Ammos = new();

        public double R310 { get; set; }
        public double Plt2026 { get; set; }
    }

    public class AmmoType
    {
        public string Type { get; set; }
        public double Count { get; set; }
        public AmmoType(string name, double count)
        {
            Type = name;
            Count = count;
        }
    }
}
