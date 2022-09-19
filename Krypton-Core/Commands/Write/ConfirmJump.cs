
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
   public class ConfirmJump : Command
    {
        public ConfirmJump()
        {
            Write();
        }

        private void Write()
        {
            param1.writeByte(0);
            param1.writeByte(0);
            param1.writeByte(0x0a);
            param1.writeByte(0);
            param1.writeByte(0x01);
            param1.writeByte(0);
            param1.writeByte(0x06);
            param1.writeUTF("JCPU|J");
        }
    }
}
