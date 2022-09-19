using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read
{
    public class PetInit
    {
        public const short ID = 155;
        public int userID { get; set; }
        public string? Username { get; set; }
        public int Level { get; set; }
        public short FactionID { get; set; }

        private string ClanTag { get; set; }
        

        public int X { get; set; }
        public int Y { get; set; }


        public PetInit(EndianBinaryReader param1)
        {
            var uk1 = param1.ReadInt32();
            uk1 = (int)((uint)uk1 >> 14 | (uint)uk1 << 18);
            Console.WriteLine("uk1 : " + uk1);
            var uk2 = param1.ReadInt32();
            userID = (int)((uint)uk2 << 5 | (uint)uk2 >> 27);
            Console.WriteLine("uk2 : " + uk2);
            var uk3 = param1.ReadInt16();
            uk3 = (short)(65535 & ((65535 & (ushort)uk3) << 1 | (65535 & (ushort)uk3) >> 15));
            Console.WriteLine("uk3 : " + uk3);
            short uk4 = param1.ReadInt16();
            uk4 = (short)(65535 & ((65535 & (ushort)uk4) >> 11 | (65535 & (ushort)uk4) << 5));
            Console.WriteLine("uk4 : " + uk4);
            uk4 = uk4 > 32767 ? (short)((int)(uk4 - 65536)) : (short)((int)(uk4));
            Console.WriteLine("uk4 : " + uk4);
            Username = param1.ReadString();
            
            var factionId = param1.ReadInt16();
            factionId = (short)(65535 & ((65535 & (ushort)factionId) << 1 | (65535 & (ushort)factionId) >> 15));
            FactionID = factionId > 32767 ? (short)((int)(factionId - 65536)) : (short)((int)(factionId));
            Console.WriteLine("factionid : " + factionId);
            var uk7 = param1.ReadInt32();
            uk7 = (int)((uint)uk7 >> 12 | (uint)uk7 << 20);
            Console.WriteLine("uk7 : " + uk7);
            var uk8 = param1.ReadInt16();
            uk8 = (short)(65535 & ((65535 & (ushort)uk8) >> 7 | (65535 & (ushort)uk8) << 9));
            Level = uk8 > 32767 ? (short)((int)(uk8 - 65536)) : (short)((int)(uk8));
            Console.WriteLine("uk8 : " + uk8);
            ClanTag= param1.ReadString();
            
            X = param1.ReadInt32();
            X = (int)((uint)X << 12 | (uint)X >> 20);
            Console.WriteLine("X : " + X);
            Y = param1.ReadInt32();
            Y = (int)((uint)Y >> 11 | (uint)Y << 21);
            Console.WriteLine("Y : " + Y);
            var uk11 = param1.ReadInt32();
            uk11 = (int)((uint)uk11 >> 9 | (uint)uk11 << 23);
            var uk12 = param1.ReadInt16();
            Console.WriteLine("uk12 : " + uk12);
            var uk13 = param1.ReadInt16();
            Console.WriteLine("uk13 : " + uk13);

        }

      
    }
}
