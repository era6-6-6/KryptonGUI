using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
    public class PlayersInit
    {
        public const short ID = 83;


        //List<class321> _loc4_;
        public int UserID { get; set; }
        public string ShipName { get; set; }
        public string ClanTag { get; set; }
        public string Username { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int FactionID { get; set; }
        public bool IsNpC { get; set; } 
        public bool Cloacked { get; set; }
        public int Unknown { get; set; }
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }
        public int Unknown3 { get; set; }
        public int Unknown4 { get; set; }
        public int Unknown5 { get; set; }


        public PlayersInit(EndianBinaryReader param1)
        {
           
            var userId = param1.ReadInt32();
            UserID = (int)((uint)userId >> 15 | (uint)userId << 17);
            ShipName = param1.ReadString();
            var var_2573 = param1.ReadInt32();
            Unknown = (int)((uint)var_2573 >> 8 | (uint)var_2573 << 24);
            this.ClanTag = param1.ReadString();
            this.Username = param1.ReadString();
            this.X = param1.ReadInt32();
            this.X = (int)((uint)this.X << 6 | (uint)this.X >> 26);
            this.Y = param1.ReadInt32();
            this.Y = (int)((uint)this.Y << 15 | (uint)this.Y >> 17);
            this.FactionID = param1.ReadInt32();
            this.FactionID = (int)((uint)this.FactionID >> 1 | (uint)this.FactionID << 31);
            var name_88 = param1.ReadInt32();
            Unknown1 = (int)((uint)name_88 >> 15 | (uint)name_88 << 17);
            var name_39 = param1.ReadInt32();
            Unknown2 = (int)((uint)name_39 << 1 | (uint)name_39 >> 31);
            var var_2888 = param1.ReadBoolean();
            var name_132 = param1.ReadInt16();
            if (null != name_132)
            {
                name_132 = param1.ReadInt16();
            }
            var var_1240 = param1.ReadInt32();
            Unknown3 = (int)((uint)var_1240 >> 12 | (uint)var_1240 << 20);
            var var_3885 = param1.ReadBoolean();
            IsNpC = param1.ReadBoolean();
            Cloacked = param1.ReadBoolean();
            var var_2261 = param1.ReadInt32();
            Unknown4 = (int)((uint)var_2261 >> 10 | (uint)var_2261 << 22);
            var var_213 = param1.ReadInt32();
            Unknown5 = (int)((uint)var_213 >> 1 | (uint)var_213 << 31);
            var var_3899 = param1.ReadString();
            Console.WriteLine(var_3899);
            var _loc2_ = 0;
            var _loc3_ = 0;
        }
    }
    
}
