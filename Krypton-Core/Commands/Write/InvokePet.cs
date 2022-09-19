using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Write
{
    public class InvokePet : Command
    {
        public const short ID = 153;
        

        public InvokePet()
        {
            Write();
        }

        public void Write()
        {
            param1.writeByte(0);
            param1.writeShort(4);
            param1.writeShort(ID);
            param1.writeShort(3);

        }
    }
}
