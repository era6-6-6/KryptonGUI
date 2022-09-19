using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Write
{
    public class AcceptGroup : Command
    {
        public const short ID = 20141;
        private int UserID { get; set; }
        public AcceptGroup(int id)
        {
            UserID = id;
            Write();
        }

        private void Write()
        {
            param1.writeByte(0);
            param1.writeShort(6);
            param1.writeShort(ID);
            param1.writeInt(UserID >> 16 | UserID << 16);
        }
    }
}
