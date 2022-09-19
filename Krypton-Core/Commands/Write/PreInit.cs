
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class PreInit : Command
    {
        public PreInit()
        {
            param1.writeByte(0x00);
            param1.writeByte(0x00);
            param1.writeByte(0x02);
            param1.writeByte(0x00);
            param1.writeByte(0x89);

        }
    }
}
