using System;
using System.Collections.Generic;
using System.Linq;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands
{
    public class CaptchaBox
    {
        public const short ID = -3117;
        public string text { get; set; } = "failed";
        public List<int> CanCollect = new();


        public CaptchaBox(EndianBinaryReader param1)
        {
            var uk3 = param1.ReadInt32();
            uk3 = (int)((uint)uk3 >> 8 | (uint)uk3 << 24);
            var uk4 = param1.ReadInt32();
            uk4 = (int)((uint)uk4 << 11 | (uint)uk4 >> 21);
            var uk5 = param1.ReadInt32();
            uk5 = (int)((uint)uk5 << 1 | (uint)uk5 >> 31);
            var uk6 = param1.ReadInt32();
            uk6 = (int)((uint)uk6 >> 12 | (uint)uk6 << 20);
            var type = param1.ReadInt16();

            var uk7 = param1.ReadInt32();
            uk7 = (int)((uint)uk7 >> 10 | (uint)uk7 << 22);

            var uk1 = param1.ReadInt32();
            uk1 = (int)((uint)uk1 << 10 | (uint)uk1 >> 22);
            
            var uk2 = param1.ReadInt32();
            uk2 = (int)((uint)uk2 >> 9 | (uint)uk2 << 23);

            Console.WriteLine("uk1: " + uk1);
            Console.WriteLine("uk2: " + uk2);
            Console.WriteLine("uk3: " + uk3);
            Console.WriteLine("uk4: " + uk4);
            Console.WriteLine("uk5: " + uk5);
            Console.WriteLine("uk6: " + uk6);
            switch (type)
            {
                case 0:
                    Console.WriteLine("collect some black: " + uk6);
                    break;
                case 1:
                    Console.WriteLine("collect all black: " + uk6);
                    break;
                case 2:
                    Console.WriteLine("collect some red: " + uk6);
                    break;
                case 3:
                    Console.WriteLine("collect all red: " + uk6);
                    break;
            }

        }
    }
}