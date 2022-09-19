
using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Read
{
    public class BoxCollected
    {
        public const short ID = -14384;
        public bool Collected { get; set; }
        public string Hash { get; set; }

        public BoxCollected(EndianBinaryReader param1)
        {
            Collected = param1.ReadBoolean();
            Hash = param1.ReadString();
            
            
        }
    }
}
