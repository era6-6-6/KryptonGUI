using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read;
public class ClanStationInit
{
    public const short ID = 1150;
    public ClanStationInit(EndianBinaryReader param1)
    {
        var uk1 = param1.ReadDouble();
        var uk2 = param1.ReadByte();
        var uk3 = param1.ReadInt16();
        for (int i = 0; i < uk2; i++)
        {
            Console.WriteLine("---BaseInit---");
            var mapId = param1.ReadInt32();
            mapId = (int)((uint)mapId >> 3 % 32 | (uint)mapId << 32 - 3 % 32);
            Console.WriteLine("mapid: " + mapId);
            var name_184 = param1.ReadInt32();
            name_184 =  (int)((uint)name_184 >> 7 % 32 | (uint)name_184 << 32 - 7 % 32);
            Console.WriteLine(name_184);
            var name_44 = param1.ReadInt32();
            name_44 = (int)((uint)name_44 << 2 % 32 | (uint)name_44 >> 32 - 2 % 32);
            Console.WriteLine(name_44);
            var status = param1.ReadInt16();
            Console.WriteLine(status);
            var name_58 = param1.ReadDouble();
            Console.WriteLine(name_58);
            var clanName = param1.ReadString();
            Console.WriteLine(clanName);
            var name_65 = param1.ReadString();
            Console.WriteLine(name_65);
            var var_1250 = param1.ReadInt16();
            var uk4 = param1.ReadInt16();
            if (null != var_1250)
            {
                param1.ReadInt16();
                var faction = param1.ReadInt16();
                Console.WriteLine("faction: " + faction);
            }
        }
    }
}