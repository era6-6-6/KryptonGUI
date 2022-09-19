using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read
{
    public class PetInitUp
    {
        public const short ID = 157;

        public int PetID { get; set; }
        public int Hp { get; set; }
        public double PetExp { get; set; }
        public int Level { get; set; }
        public int Fuel { get; set; }
        public int heatLvl { get; set; }
        public int Shd { get; set; }
        public int MaxHp { get; set; }
        public int MaxShd { get; set; }
        public string PetName { get; set; }
        public int MaxFuel { get; set; }

        public PetInitUp(EndianBinaryReader param1)
        {
            PetID = param1.ReadInt32();
            PetID = (int)((uint)PetID >> 11 | (uint)PetID << 21);

            Level = param1.ReadInt32();
            // Level = Level << 5 | Level >> 27;
            Level = (int)((uint)Level << 5 | (uint)Level >> 27); 

            PetExp = param1.ReadDouble();
            param1.ReadDouble();

            Hp = param1.ReadInt32();
            // Hp = Hp << 12 | Hp >> 20;
            Hp = (int)((uint)Hp << 12 | (uint)Hp >> 20);

            MaxHp = param1.ReadInt32();
            MaxHp = (int)((uint)MaxHp >> 5 | (uint)MaxHp << 27);

            Shd = param1.ReadInt32();
            Shd = (int)((uint)Shd << 13 | (uint)Shd >> 19);


            MaxShd = param1.ReadInt32();
            MaxShd = (int)((uint)MaxShd << 2 | (uint)MaxShd >> 30);

            Fuel = param1.ReadInt32();
            Fuel = (int)((uint)Fuel << 4 | (uint)Fuel >> 28);

            MaxFuel = param1.ReadInt32();
            MaxFuel = (int)((uint)MaxFuel << 6 | (uint)MaxFuel >> 26);

            param1.ReadInt32();

            PetName = param1.ReadString();
            param1.ReadString();








            



        }


    }




    
}
