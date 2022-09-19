using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Managers;

namespace Krypton_Core.Commands.Write
{
    public class BuyPetFuel : Command
    {
        public BuyPetFuel()
        {
            param1.writeByte(0);
            param1.writeShort(4);
            param1.writeShort(153);
            param1.writeShort(6);
            //00 00 04 00 99 00 06
        }
    }
}
