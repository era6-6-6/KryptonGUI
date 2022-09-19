using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Write
{
    public class CaptchaCollect : Command
    {
       
        private int Index { get; set; }
        public CaptchaCollect(int index)
        {
            Index = index;
            Write();
        }

        private void Write()
        {
            param1.writeByte(0);
            param1.writeShort(6);
            param1.writeShort(0);
            param1.writeInt(Index << 4 | Index >> 28);
        }
    }
}
