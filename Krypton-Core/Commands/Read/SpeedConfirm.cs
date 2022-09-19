using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
   
    public class SpeedConfirm
    {
        public const short ID = 96;
        public int Speed { get; set; } 

        public SpeedConfirm(EndianBinaryReader param1)
        {
            var speedOld = param1.ReadInt32();
            speedOld = (int)((uint)speedOld << 12 % 32 | (uint)speedOld >> 32 - 12 % 32);
            var speed = param1.ReadInt32();
            Speed = (int)((uint)speed << 1 % 32 | (uint)speed >> 32 - 1 % 32);

            Console.WriteLine("Speed old " + speedOld);
            Console.WriteLine("speed new " + Speed);
        }

    }
}
