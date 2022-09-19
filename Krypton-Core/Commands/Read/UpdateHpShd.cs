using Krypton_Core.Packets.Bytes;


namespace Krypton_Core.Commands.Read
{
    public class UpdateHpShd
    {
        public const short ID = 28;
       
        public int Unknown { get; set; }
        public int UserID { get; set; }
        public int HP { get; set; }
        public int Shd { get; set; }
        public int Unknown4 { get; set; }
        public int Damage { get; set; }
        public UpdateHpShd(EndianBinaryReader param1, Api api)
        {
            param1.ReadInt16();
            param1.ReadInt16();
            var name_153 = param1.ReadInt32();
            UserID = (int)((uint)name_153 << 7 | (uint)name_153 >> 25);
            var var_1692 = param1.ReadInt32();
            UserID = (int)((uint)var_1692 >> 3 | (uint)var_1692 << 29);
            var var_5516 = param1.ReadInt32();
            HP = (int)((uint)var_5516 >> 13 | (uint)var_5516 << 19);
            var var_3661 = param1.ReadInt32();
            Shd = (int)((uint)var_3661 << 13 | (uint)var_3661 >> 19);
            var var_4734 = param1.ReadInt32();
            Unknown4 = (int)((uint)var_4734 >> 8 | (uint)var_4734 << 24);
            this.Damage = param1.ReadInt32();
            this.Damage = (int)((uint)this.Damage >> 8 | (uint)this.Damage << 24);
            var var_3740 = param1.ReadBoolean();
            
           
            

        }
 
    }
    
}
