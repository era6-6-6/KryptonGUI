using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class Statistics
    {
        internal int PalladiumCount { get; set; } = 0;

        public int BoxesCollected { get; set; }
        public int NpcsKilled { get; set; }
        public double Uridium { get; set; }
        public double Credits { get; set; }
        public double Experience { get; set; }
        public double Honor { get; set; }
        public bool Cloacked { get; set; }
        public bool Premium { get; set; }
        public int TotalDeaths { get; set; } = 0;
        public int RankId { get; set; }
        public int Level { get; set; }
        public int UriPH { get; set; }
        public int CrPH { get; set; }
        public int ExpPH { get; set; }
        public int HonPH { get; set; }
        public double CollectedUridium { get; set; }
        public double CollectedCredits { get; set; }
        public double CollectedXP { get; set; }
        public double CollectedHonor { get; set; }
        public int CollectedEE { get; set; }
    }
}
