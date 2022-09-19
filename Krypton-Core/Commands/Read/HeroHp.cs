using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
    public class HeroHp
    {
        public const short ID = 1300;
        public int Hitpoints, MaxHp, Nano, NanoMax;
        public HeroHp(EndianBinaryReader param1)
        {
            Hitpoints = param1.ReadInt32();
            Hitpoints = (int)((uint)Hitpoints << 9 | (uint)Hitpoints >> 23);
            MaxHp = param1.ReadInt32();
            MaxHp = (int)((uint)MaxHp >> 1 | (uint)MaxHp << 31);
            Nano = param1.ReadInt32();
            Nano = (int)((uint)Nano << 13 | (uint)Nano >> 19);
            NanoMax = param1.ReadInt32();
            NanoMax = (int)((uint)NanoMax >> 6 | (uint)NanoMax << 26);

            
        }
    }
}
