using Krypton_Core.Commands.Read;
using Krypton_Core.Packets.Bytes;
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Read;

public class InitResources : Command
{
    public const short ID = 147;
    public string Hash { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public ushort Type { get; set; }
    public InitResources(EndianBinaryReader param1)
    {
        var type = param1.ReadUInt16();
        var BoxIn = new BoxInitX(param1);
        this.Hash = BoxIn.Hash;
        this.X = BoxIn.X;
        this.Y = BoxIn.Y;
        param1.ReadUInt16();
        Type = param1.ReadUInt16();
    }



}