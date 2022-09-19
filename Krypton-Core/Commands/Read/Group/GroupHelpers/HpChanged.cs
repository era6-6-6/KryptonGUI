using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read.Group.GroupHelpers
{
    public class HpChanged
    {
        public const short ID = -14800;

        public int MaxHp, MaxNano, MaxShd, Hp, Shd, Nano;
        public HpChanged(EndianBinaryReader param1)
        {
            param1.ReadInt16();
            var uk11 = param1.ReadInt32();
            MaxHp = (int)((uint)uk11 << 1 | (uint)uk11 >> 31);
            Console.WriteLine("uk11: " + uk11);
            var uk12 = param1.ReadInt32();
            MaxNano = (int)((uint)uk12 >> 8 | (uint)uk12 << 24);
            Console.WriteLine("uk12: " + uk12);
            var uk13 = param1.ReadInt32();
            MaxShd = (int)((uint)uk13 << 14 | (uint)uk13 >> 18);
            Console.WriteLine("uk13: " + uk13);
            var Npchp = param1.ReadInt32();
            Hp = (int)((uint)Npchp >> 2 | (uint)Npchp << 30);
            Console.WriteLine("npcHp: " + Npchp);
            var Npcshield = param1.ReadInt32();
            Shd = (int)((uint)Npcshield << 16 | (uint)Npcshield >> 16);
            Console.WriteLine("npcShield: " + Npcshield);
            var uk14 = param1.ReadInt32();
            Nano = (int)((uint)uk14 >> 6 | (uint)uk14 << 26); // targetId

          
        }
    }
}
