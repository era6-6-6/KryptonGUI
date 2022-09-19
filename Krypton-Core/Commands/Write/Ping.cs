
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class Ping : Command
    {
        public Ping()
        {
            Write();
        }

        public void Write()
        {
            param1.writeByte(0);
            param1.writeShort(2);
            param1.writeShort(2);
        }
    }
}
