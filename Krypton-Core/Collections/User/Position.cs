using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TargetX { get; set; }

    
        public int TargetY { get; set; }
        public bool Moving { get; set; }
        public int ActualX { get; set; }
        public int ActualY { get; set; }
    }
}
