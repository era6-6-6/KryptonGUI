
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class JumpPortal : Command
    {
        public JumpPortal()
        {
            param1.writeByte(0x00);
            param1.writeShort(2);
            param1.writeShort(10);
        }
    }
}
