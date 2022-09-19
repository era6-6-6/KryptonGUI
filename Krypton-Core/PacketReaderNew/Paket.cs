using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.PacketReaderNew
{
    public abstract class Paket
    {
        public abstract void Read(EndianBinaryReader r);
        public abstract void Write();
    }
    
}
