using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read;

public class ClanBaseOnMap
{
    public const short ID = -31392;
    public int X { get; set; }
    public int Y { get; set; }
    public int BaseID { get; set; }
    public string Name { get; set; }
    public int FactionID { get; set; }
    public string Owner { get; set; }
    public int AssetId { get; set; }
    public bool bool1, bool2, bool3, bool4, bool5;
    public string OwnerString;
    public ClanBaseOnMap(EndianBinaryReader param1)
    {

        var assetId = param1.ReadInt32();
        AssetId = (int)((uint)assetId >> 1 | (uint)assetId << 31);
        var var_2597 = param1.ReadInt32();
        Y = (int)((uint)var_2597 << 11 | (uint)var_2597 >> 21);
        bool1 = param1.ReadBoolean();
        var factionId = param1.ReadInt32();
        FactionID = (int)((uint)factionId >> 1 | (uint)factionId << 31);
        Name = param1.ReadString();
        var name_96 = param1.ReadInt32();
        name_96 = (int)((uint)name_96 >> 2 | (uint)name_96 << 30);
        var name_183 = param1.ReadInt32();
        name_183 = (int)((uint)name_183 << 5 | (uint)name_183 >> 27);

        param1.ReadInt16();
        param1.ReadInt16(); 

        var loc3 = param1.ReadByte();
        for (int i = 0; i < loc3; i++)
        {
            param1.ReadUInt16();
            var userId = param1.ReadInt32();
            userId = (int)((uint)userId >> 12 | (uint)userId << 20);


            var modifier = param1.ReadInt16();


            var attribute = param1.ReadInt32();
            attribute = (int)((uint)attribute << 7 | (uint)attribute >> 25);


            OwnerString = param1.ReadString();


            var count = param1.ReadInt32();
            count = (int)((uint)count >> 14 | (uint)count << 18);


            bool2 = param1.ReadBoolean();
            


        }
        bool3 = param1.ReadBoolean();
        bool4 = param1.ReadBoolean();




        
        var var_2573 = param1.ReadInt32();
        var_2573= (int)((uint)var_2573 << 15 | (uint)var_2573 >> 17);
        Owner = param1.ReadString();
        var var_1998 = param1.ReadInt32();
        X = (int)((uint)var_1998 << 15 | (uint)var_1998 >> 17);

       bool5 =  param1.ReadBoolean();


        param1.ReadInt16();
        param1.ReadInt16();
        
        
        Console.WriteLine("station x: " + X + "station y: " + Y);
        if (FactionID == 0)
        {

            Console.WriteLine(Name);
            Console.WriteLine(Owner);
            Console.WriteLine("Base Init");
        }
        
        
        
            

    }
}