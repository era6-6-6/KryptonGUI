using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Collectables
{
    public class Box
    {
        public enum BoxType
        {
            BONUS_BOX, CARGO_BOX, FROM_SHIP
        }
        public string? Hash { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string? type { get; set; }
    }
}
