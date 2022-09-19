using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
    public class MineInit
    {
        public const short ID = 29820;
        public string Hash { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public int Type { get; set; }
        public MineInit(EndianBinaryReader param1)
        {
            param1.ReadInt16();
            BoxInitX x = new BoxInitX(param1);
            Hash = x.Hash;
            X = x.X;
            Y = x.Y;
            Type = param1.ReadInt32();
            Type = (int)((uint)Type >> 16 | (uint)Type << 16);
            param1.ReadBoolean();
            param1.ReadBoolean();
        }
    }
}
