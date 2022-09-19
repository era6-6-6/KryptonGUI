using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.StatsManager.obj
{
    public class NpcStats
    {
        public string? Name { get; set; }
        public int Killed { get; set; } = 0;

        public NpcStats()
        {

        }
        public NpcStats(string name)
        {
            Name = name;
            Killed = 1;
        }

    }
}
