
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write

{
    public class JumpX2CPU : Command
    {
        public JumpX2CPU()
        {
            Write();
        }

        private void Write()
        {
            //00 00 0c 00 01 00 08
            param1.writeByte(0);
            param1.writeByte(0);
            param1.writeByte(0x0c);
            param1.writeByte(0);
            param1.writeByte(0x01);
            param1.writeByte(0);
            param1.writeByte(0x08);
            param1.writeUTF("JCPU|S|6");

        }
    }
}
