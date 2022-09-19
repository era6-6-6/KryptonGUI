using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Write
{
    public class Logout : Command
    {
        public const short ID = 16887;

        public string text = "logout";

        public Logout()
        {
            Write();
        }

        private void Write()
        {
            param1.writeByte(0);
            param1.writeShort(10);
            param1.writeShort(ID);
            param1.writeUTF(text);
        }
    }
}
