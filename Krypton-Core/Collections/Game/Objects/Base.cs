using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Bases
{
    public class Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int FactionId { get; set; }
        public string OwnerClan { get; set; }
        public int ActiveModules { get; set; }
        public bool bool1 { get;  set; }
        public bool bool2 { get;  set; }
        public int AssetId { get; set; }
        public bool bool3 { get;  set; }
        public bool bool4 { get;  set; }
        public bool bool5 { get;  set; }
    }
}
