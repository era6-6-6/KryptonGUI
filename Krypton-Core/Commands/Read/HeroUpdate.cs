

using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read;

    public class HeroUpdate
    {
        public const short ID = 50;
        public int UserID { get; set; }
        public int Hitpoints { get; set; }
        public int Shield { get; set; }
        public int Unknown { get; set; }
        public int MaxShd { get; set; }
        public int MaxHp { get; set; }
        public int Unknown4 { get; set; }
        public int Unknown5 { get; set; }
        public bool Taken { get; set; }
        public HeroUpdate(EndianBinaryReader param1)
        {
            var userId = param1.ReadInt32();
            this.UserID = (int)((uint)userId << 8 | (uint)userId >> 24);
            var var_2759 = param1.ReadInt32();
            Unknown = (int)((uint)var_2759 << 5 | (uint)var_2759 >> 27);
            var shield = param1.ReadInt32();
            Shield = (int)((uint)shield << 7 | (uint)shield >> 25);
            var name_92 = param1.ReadInt32();
            MaxShd = (int)((uint)name_92 << 15 | (uint)name_92 >> 17);
            var hitpoints = param1.ReadInt32();
            Hitpoints = (int)((uint)hitpoints << 15 | (uint)hitpoints >> 17);
            var name_85 = param1.ReadInt32();
            MaxHp = (int)((uint)name_85 << 13 | (uint)name_85 >> 19);
            var var_4281 = param1.ReadInt32();
            Unknown4 = (int)((uint)var_4281 << 1  | (uint)var_4281 >> 31);
            var var_3980 = param1.ReadInt32();
            Unknown5 = (int)((uint)var_3980 << 3 | (uint)var_3980 >> 29);
            Taken = param1.ReadBoolean();
            //print all
           
        

    }
    }

