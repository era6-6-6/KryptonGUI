
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class InitPacket : Command
    {
        public const short ID = 14;

        public short Index { get; private set; }

        public string Message { get; private set; } = "2D 1418x815 .root1.instance473.MainClientApplication0.ApplicationSkin2.Group3.Group4._-v2c5.instance25061 root1 false -1";

        public InitPacket(short num)
        {
            this.Index = num;
            Write();
        }

        public void Write()
        {
            var length = Message.Length + 6;
            param1.writeByte(0);
            param1.writeShort((short)length);
            param1.writeShort(ID);
            param1.writeUTF(Message);
            param1.writeShort(Index);
        }
    }
}
