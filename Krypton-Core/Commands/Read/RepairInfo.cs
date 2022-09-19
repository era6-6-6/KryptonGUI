using System;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
    public class RepairInfo
    {
        public const short ID = 252;
        public int X { get; set; }
        public int Y { get; set; }
        public bool RepairOnPlaceAvaible = false;
        //TODO: fix me! asap
        public RepairInfo(EndianBinaryReader param1)
        {
            var uk1 = param1.ReadString();
            var uk2 = param1.ReadString();
            var uk3 = param1.ReadString();
            param1.ReadInt16();
            var uk4 = param1.ReadInt16();
            System.Console.WriteLine("uk1: " + uk1);
            System.Console.WriteLine("uk2: " + uk2);
            System.Console.WriteLine("uk3: " + uk3);
            Console.WriteLine("uk4: " + uk4);
            var loc4 = param1.ReadByte();
            System.Console.WriteLine("loc4: " + loc4);

            for(int i = 0; i < loc4; i++)
            {
                //253
                param1.ReadInt16();
                var uk6 = param1.ReadInt16();
                param1.ReadInt16();
                var type = param1.ReadInt16();


                var amount = param1.ReadInt32();
                amount = amount << 14 | amount >> 18;
                var uk7 = param1.ReadBoolean();
                var uk8 = param1.ReadInt32();
                uk8 = uk8 << 11 | uk8 >> 21;
                

                for(int k = 0 ; k < 4 ; k++)
                {
                    param1.ReadInt16();
                    var baseKEy = param1.ReadString();
                    param1.ReadInt16();
                    var uk9 = param1.ReadInt16();
                    var loc5 = param1.ReadByte();
                    for(int j = 0 ; j < loc5; j++)
                    {

                        param1.ReadInt16();
                        var wildcard = param1.ReadString();
                        var replacement = param1.ReadString();
                        param1.ReadInt16();
                        var uk10 = param1.ReadInt16();
                        System.Console.WriteLine("wildcard: " + wildcard + " replacement: " + replacement);
                        System.Console.WriteLine(   "uk10: " + uk10);
                        if(replacement.Contains("desc_killscreen_repair_location_for_money") || wildcard.Contains("desc_killscreen_repair_location_for_money") || wildcard.Contains("desc_killscreen_free_repair_cause_premium"))
                        {
                            RepairOnPlaceAvaible = true;
                            return;
                        }

                    }
                    if(baseKEy.Contains("desc_killscreen_repair_location_for_money"))
                    {
                        RepairOnPlaceAvaible = true;
                        return;
                    }
                    System.Console.WriteLine("baseKey: " + baseKEy);
                    System.Console.WriteLine("uk9: " + uk9);

                } 
              
                Console.WriteLine("uk6: " + uk6);
                Console.WriteLine("type: " + type);
                Console.WriteLine("amount: " + amount);
                Console.WriteLine("uk7: " + uk7);
                Console.WriteLine("uk8: " + uk8);

               






            }

        }

        //create method which will shift the bits of the ints
        //create method which will shift the bits of the ints
        
    
    }
}