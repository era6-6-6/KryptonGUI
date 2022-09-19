using Krypton_Core.Packets.Bytes;
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Read;



public class RemovePlayer : Command
{
    public const short ID = 52;

    public int UserID { get; set; }

    public RemovePlayer(EndianBinaryReader r)
    {
        UserID = r.ReadInt32();
        UserID = (int)((uint)UserID >> 5 | (uint)UserID << 27);
    }

}