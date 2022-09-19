using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Objects
{
    public class BarrierPath
    {
        public string? Name { get; set; }
        public Rectangle Rec { get; set; }

        public BarrierPath(Rectangle rec, string? name = null)
        {
            Name = name;
            Rec = rec;
        }
    }
}
