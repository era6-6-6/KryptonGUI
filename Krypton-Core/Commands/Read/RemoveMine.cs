using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
    
    public class RemoveMine
    {
        public const short ID = -30546;
        public string Hash { get; set; }
        public RemoveMine(EndianBinaryReader param1)
        {
            Hash = param1.ReadString();

        }
    }
}
