using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Write
{
    public class SendGroup : Command
    {
        public const short ID = 16906;
        private string? Username { get; set; }
        public SendGroup(string name)
        {
            Username = name;
            Write();
        }

        private void Write()
        {
            param1.writeByte(0);
            var buffer = Encoding.UTF8.GetBytes(Username);
            var length = buffer.Length + 4;
            param1.writeShort((short)length);
            param1.writeShort(ID);
            param1.writeShort((short)buffer.Length);
            param1.writeUTF1(Username);
            buffer = null;
        }
    }
    
}
