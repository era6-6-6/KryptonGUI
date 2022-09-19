
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class EndPoint : Command
    {
        public EndPoint()
        {
            param1.writeByte(0);
            param1.writeByte(0);
            param1.writeByte(0x10);
            param1.writeByte(0xad);
            param1.writeByte(0xd2);
            param1.writeByte(0x00);
            param1.writeByte(0x08);
            param1.writeByte(0x68);
            param1.writeByte(0x68);
            param1.writeByte(0x33);
            param1.writeByte(0x5f);
            param1.writeByte(0x62);
            param1.writeByte(0x34);
            param1.writeByte(0x38);
            param1.writeByte(0x32);
            param1.writeByte(0x00);
            param1.writeByte(0x02);
            param1.writeByte(0x63);
            param1.writeByte(0x73);
        }
    }
}
