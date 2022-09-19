using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Write
{
    public class SellPalla : Command
    {
        private int Count { get; set; }


        public SellPalla(int count)
        {
            Count = count;
            Write();
        }

        private void Write()
        {
            var message = $"XCP|{Count}";
            param1.writeByte(0);
            var length = message.Length + 4;
            param1.writeShort((short)length);
            param1.writeShort(1);
            param1.writeUTF(message);
        }
    }
}
