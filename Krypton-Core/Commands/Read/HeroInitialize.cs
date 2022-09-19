
using Krypton_Core.Packets.Bytes;


namespace Krypton_Core.Commands.Read;

public class HeroInitialize
{
    //BinaryReader Reader;
    public const short ID = 49;
    public int Unknown1 { get; set; }
    public int Unknown2 { get; set; }
    public int Unknown3 { get; set; }
    public int Unknown4 { get; set; }
    public int Unknown5 { get; set; }
    public int Unknown6 { get; set; }
    public int Unknown7 { get; set; }
    public int Unknown8 { get; set; }
    public uint MaxShield { get; private set; } //name_103
    public bool Premium { get; private set; }
    public bool var_4823 { get; private set; }
    public double Credits { get; private set; }
    public double Honor { get; private set; } //var_4055
    public uint ClanID { get; private set; } //name_48
    public double Uridium { get; private set; }
    public bool var_3678 { get; private set; }
    public uint Rank { get; private set; } //name_134
    public bool Cloaked { get; private set; }
    public string Username { get; private set; } //var_3495
    public uint Speed { get; private set; }
    public int CargoCapacity { get; private set; } //var_3020
    public int Shield { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public int UserID { get; private set; } //name_125
    public uint var_3377 { get; private set; }
    public uint var_3914 { get; private set; } //var_3914
    public uint FreeCargoSpace { get; private set; } //var_4296
    public string Shipname { get; private set; } //name_122
    public int HP { get; private set; } //var_1065
    public int Level { get; private set; }
    public uint NanoHP { get; private set; } //var_2224
    public double XP { get; private set; } //var_4549
    public int Map { get; private set; }
    public int FactionID { get; private set; }
    public string ClanTag { get; private set; } //name_138
    public int MaxHP { get; private set; } //var_1851
    public uint MaxNanoHP { get; private set; } //var_1600
    public string typeId { get; private set; }
    public int var_641 { get; set; }
    public int var_23 { get; set; }
    public int var_1993 { get; set; }
    public float Jackpot { get; set; }
    public int Unknown9 { get; private set; }
   


    public HeroInitialize(EndianBinaryReader param1)
    {

      
        this.UserID = param1.ReadInt32();
        this.UserID = (int)((uint)this.UserID >> 14 | (uint)this.UserID << 18);
        this.Username = param1.ReadString();
        this.Shipname = param1.ReadString();
        this.Speed = (uint)param1.ReadInt32();
        this.Speed = (uint)this.Speed << 12 | (uint)this.Speed >> 20;
        this.Shield = param1.ReadInt32();
        this.Shield = (int)((uint)this.Shield << 14 | (uint)this.Shield >> 18);
        this.MaxShield = (uint)param1.ReadInt32();
        this.MaxShield = (uint)this.MaxShield << 13 | (uint)this.MaxShield >> 19;
        this.var_641 = param1.ReadInt32();
        HP = (int)((uint)this.var_641 << 9 | (uint)this.var_641 >> 23);
        this.var_23 = param1.ReadInt32();
        MaxHP = (int)((uint)this.var_23 << 15 | (uint)this.var_23 >> 17);
        this.var_1993 = param1.ReadInt32();
        Unknown1 = (int)((uint)this.var_1993 << 10 | (uint)this.var_1993 >> 22);
        var var_4734 = param1.ReadInt32();
        CargoCapacity = (int)((uint)var_4734 >> 14 | (uint)var_4734 << 18);
        var var_4280 = param1.ReadInt32();
        Unknown6 = (int)((uint)var_4280 >> 11 | (uint)var_4280 << 21);
        var var_3979 = param1.ReadInt32();
        Unknown3 = (int)((uint)var_3979 >> 15 | (uint)var_3979 << 17);
        this.X = param1.ReadInt32();
        this.X = this.X >> 14 | this.X << 18;
        this.Y = param1.ReadInt32();
        this.Y = this.Y >> 4 | this.Y << 28;
        this.Map = param1.ReadInt32();
        this.Map = (int)((uint)this.Map << 11 | (uint)Map >> 21);
        this.FactionID = param1.ReadInt32();
        this.FactionID = (int)((uint)this.FactionID << 5 | (uint)this.FactionID >> 27);
        var name_88 = param1.ReadInt32();
        Unknown4 = (int)((uint)name_88 >> 3 | (uint)name_88 << 29);
        var var_2573 = param1.ReadInt32();
        Unknown5 = var_2573 >> 5 | var_2573 << 27;
        this.Premium = param1.ReadBoolean();
        XP = param1.ReadDouble();
        Honor = param1.ReadDouble();
        this.Level = param1.ReadInt32();
        this.Level = (int)((uint)this.Level >> 15 | (uint)this.Level << 17);
        this.Credits = param1.ReadDouble();
        this.Uridium = param1.ReadDouble();
        this.Jackpot = param1.ReadSingle();
        var name_39 = param1.ReadInt32();
        Rank = (uint)name_39 << 14 | (uint)name_39 >> 18;
        this.ClanTag = param1.ReadString();
        var var_1240 = param1.ReadInt32();
        Unknown9 = (int)((uint)var_1240 << 5 | (uint)var_1240 >> 27);
        var var_3885 = param1.ReadBoolean();
        this.Cloaked = param1.ReadBoolean();
        var var_2770 = param1.ReadBoolean();
        var _loc2_ = 0;
        var _loc3_ = 0;
      

        _loc2_ = 0;
        _loc3_ = param1.ReadByte();
        for (int i = 0; i < _loc3_; i++)
        {
            param1.ReadInt16();
            var UserId = param1.ReadInt32();
            UserId = UserID >> 12 | UserId << 20;
            var mod = param1.ReadInt16();
            var attribut = param1.ReadInt32();
            attribut = attribut << 7 | attribut >> 25;
            var var_1457 = param1.ReadString();
            var count = param1.ReadInt32();
            count = count >> 14 | count << 18;

            bool activate = param1.ReadBoolean();


            Console.WriteLine("UserId:" +UserId);
            Console.WriteLine("mod: " + mod);
            Console.WriteLine("Atribbute: " + attribut);
            Console.WriteLine("uk: " +  var_1457);
            Console.WriteLine("Count: " + count);
            Console.WriteLine("Active: " + activate );

        }
        //while (_loc2_ < _loc3_)
        //{
        //    var a = param1.ReadInt16();





        //}
        //print all
        //foreach (class321 _loc4_ in this.modifier)
        //{
        //print all uknown




    }
}
