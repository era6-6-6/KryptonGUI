using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Objects
{
    public class Mine
    {

        public string Hash { get; set; } = "";
        public int Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Mine(string hash, int type, int x, int y)
        {
            Hash = hash;
            Type = type;
            X = x;
            Y = y;
        }
    }
}
