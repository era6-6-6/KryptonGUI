using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Objects
{
    public class Portal
    {
        public int TypeID { get; set; }
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int MapID { get; set; }  //next map id 
        public short FactionId { get; set; }
    }
}
