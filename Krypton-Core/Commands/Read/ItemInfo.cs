using Krypton_Core.Managers;
using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read
{
    internal class ItemInfo 
    {
        public const short ID = -7573;
        public bool IsNew { get; set; }
        public string LootId { get; set; }
        public double Amout { get; set; }
        public double ItemId { get; set; }
        public int UpgradeLvl { get; set; }
        public string Value { get; set; }

        public short Key { get; set; }


        public ItemInfo(EndianBinaryReader param1)
        {
            param1.ReadInt16();
            LootId = param1.ReadString();
            UpgradeLvl = param1.ReadInt32();

            UpgradeLvl = UpgradeLvl << 8 | UpgradeLvl >> 24;

            IsNew = param1.ReadBoolean();
            var unknown = param1.ReadDouble();
            var loc = param1.ReadByte();
            for (int i = 0; i < loc; i++)
            {
                param1.ReadInt16();
                Value = param1.ReadString();
                Key = param1.ReadInt16();

            }
            Amout = param1.ReadDouble();
            ItemId = param1.ReadDouble();
            
            

            

           



            




        }
    }
}
