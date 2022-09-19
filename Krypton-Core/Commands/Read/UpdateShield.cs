using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read
{

    internal class UpdateShield
    {
        public const short ID = 248;
        public int Shield { get; set; }
        public int MaxShield { get; set; }
        public UpdateShield(EndianBinaryReader reader)
        {
            Shield = reader.ReadInt32();
            Shield = (int)((uint)Shield >> 16 | (uint)Shield << 16);
            MaxShield = reader.ReadInt32();
            MaxShield = (int)((uint)MaxShield >> 12 | (uint)MaxShield << 20);
        }
    }
}
