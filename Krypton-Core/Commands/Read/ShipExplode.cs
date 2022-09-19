using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read
{
    public class ShipExplode
    {
        public const short ID = 30;
        public int UserID { get; set; }

        public ShipExplode(EndianBinaryReader param1)
        {
            UserID = param1.ReadInt32();
            UserID = (int)((uint)this.UserID << 5 % 32 | (uint)this.UserID >> 32 - 5 % 32);
            var var_2933 = param1.ReadInt32();
            var_2933 = var_2933 >> 7 % 32 | var_2933 << 32 - 7 % 32;
        }
    }
}
