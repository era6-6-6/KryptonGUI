using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.StatsManager.obj
{
    public class BoxStats
    {
        public string? Type { get; set; }
        public int Collected { get; set; }


        public BoxStats()
        {

        }
        public BoxStats(string type)
        {
            Type = type;
            Collected = 1;
        }
    }
}
