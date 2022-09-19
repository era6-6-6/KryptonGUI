using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read.Group.GroupHelpers
{
    public class PositionChanged
    {
        public const short ID = 16936;
        public int X, Y, MapID;
        public PositionChanged(EndianBinaryReader param1)
        {
            param1.ReadInt16();
            var x = param1.ReadInt32();
            X = (int)((uint)x << 13 | (uint)x >> 19);
            
            var mapId = param1.ReadInt32(); // map
            MapID = (int)((uint)mapId << 5 | (uint)mapId >> 27);
            Console.WriteLine("x : " + x);
            Console.WriteLine("MapId: " + mapId);
            var y = param1.ReadInt32();
            Y = (int)((uint)y << 11 | (uint)y >> 21);
            Console.WriteLine("y: " + y);
        }
    }
}
