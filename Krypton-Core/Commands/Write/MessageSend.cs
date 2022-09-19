
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class MessageSend : Command
    {
        private string _Message { get; set; }
        public MessageSend(string message)
        {
            _Message = message;
             Write();
        }
        public MessageSend()
        {
            Write();
        }
        public void Write()
        {
            param1.writeByte(0);
            var length = (short)_Message.Length + 4;
            param1.writeShort((short)length);
            param1.writeShort(1);
            //param1.writeByte(0);
            param1.writeUTF(_Message);
        }
    }
}
