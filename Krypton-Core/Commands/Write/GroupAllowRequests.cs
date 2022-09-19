using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Managers;

namespace Krypton_Core.Commands.Write
{
    public class GroupAllowRequests : Command
    {
        public const short ID = -21158;
        public GroupAllowRequests()
        {
            Write();
        }

        private void Write()
        {
            param1.writeByte(0);
            param1.writeShort(6);
            param1.writeShort(ID);
            param1.writeShort(19651);
            param1.writeShort(1);
        }
    }
}

