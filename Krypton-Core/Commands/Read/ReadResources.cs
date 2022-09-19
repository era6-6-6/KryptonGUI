using Krypton_Core.Managers;
using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read
{
    public class ReadResources 
    {
        public const short ID = 143;
        public short Type { get; set; }
        public ReadResources(EndianBinaryReader param1)
        {
            var eventType = param1.ReadInt16();
            Console.WriteLine("event type: " + eventType);
            param1.ReadInt16();
            Type = param1.ReadInt16();
            
            var hash = param1.ReadString();
            Console.WriteLine("hash: " + hash);




        }
        
    }
                        //var uk1 = param1.ReadBoolean();
                        //Console.WriteLine("uk1: " + uk1);
                        //param1.ReadInt16();
                        //var uk2 = param1.ReadInt32();
                        //uk2 = (int)((uint)uk2 << 14 | (uint)uk2 >> 18);
                        //Console.WriteLine("uk2: " + uk2);
                        //var loc3 = param1.ReadInt16();
                        //for (int i = 0; i < loc3; i++)
                        //{
                        //    param1.ReadInt16();
                        //    //463
                        //    var loc4 = param1.ReadByte();
                        //    for (int j = 0; j < loc4; j++)
                        //    {
                        //        //-19750
                        //        param1.ReadInt16();
                        //        var attribute = param1.ReadString();
                        //        var uk3 = param1.ReadSingle();
                        //        Console.WriteLine("Atribute: " + attribute);
                        //        Console.WriteLine("uk3: " + uk3);
                        //    }
                        //    var uk4 = param1.ReadInt32();
                        //    uk4 = (int)((uint)uk4 << 3 | (uint)uk4 >> 29);
                        //    Console.WriteLine("uk4: " + uk4);
                        //    var uk5 = param1.ReadInt32();
                        //    uk5 = (int)((uint)uk5 << 13 | (uint)uk5 >> 19);
                        //    Console.WriteLine("uk5: " + uk5);
                        //    var lootID = param1.ReadString();
                        //    Console.WriteLine("lootID: " + lootID);
                        //    var itemID = param1.ReadInt32();
                        //    itemID = (int)((uint)itemID << 12 | (uint)uk5 >> 19);
                        //    Console.WriteLine("itemID: " + itemID);
                        //    var uk6 = param1.ReadInt32();
                        
                        //    uk6 = (int)((uint)uk6 >> 8 | (uint)uk6 << 24);
                        //    Console.WriteLine("uk6: " + uk6);
                        
                        //    var loc6 = param1.ReadByte();
                        //    for (int k = 0; k < loc6; k++)
                        //    {
                        //        var uk7 = param1.ReadString();
                        //        Console.WriteLine("uk7: " + uk7);
                        //    }
                        //    var uk8 = param1.ReadInt32();
                        //    uk8 = (int)((uint)uk8 >> 3 | (uint)uk8 << 29);
                        //    Console.WriteLine("uk8: " + uk8);
                        
}
