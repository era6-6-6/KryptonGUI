using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class Close : Command
    {
        public const short ID = 260;

        public Close()
        {
            Write();
        }
        public void Write()
        {
            param1.writeByte(0);
            param1.writeByte(0);
            param1.writeByte(3);
            param1.writeByte(1);
            param1.writeByte(4);
            param1.writeByte(1);
        }
    }
}
