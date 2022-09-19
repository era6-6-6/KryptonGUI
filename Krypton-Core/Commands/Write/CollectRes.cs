using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Write
{
    public class CollectRes : Command
    {
        public const short ID = 94;

        public string? Hash { get; private set; }

        public CollectRes(string hash)
        {
            Hash = hash;
            Write();
        }

        private void Write()
        {
            var length = Hash.Length + 4;
            param1.writeByte(0);
            param1.writeShort((short)length);
            param1.writeShort(ID);
            
            param1.writeUTF(Hash);
        }
    }
}
