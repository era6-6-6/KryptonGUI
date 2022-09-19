using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Maps
{
    public class MapSize
    {
        public static System.Drawing.PointF SizeOfTheMap(int mapID)
        {
            if (mapID == 91 || mapID == 93 || mapID == 16 || mapID == 29)
            {
                return new System.Drawing.PointF(41000, 26000);
            }
            else
            {
                return new System.Drawing.PointF(20500, 13000);
            }
        }
    }
}
