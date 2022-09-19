using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
    public class RemovePlayerFromGroup
    {
        public const short ID = -8419;
        public int UserID { get; set; }
        public RemovePlayerFromGroup(EndianBinaryReader param1)
        {

            var var_943 = param1.ReadInt32();
            UserID = (int)((uint)var_943 >> 3 % 32 | (uint)var_943 << 32 - 3 % 32);
            Console.WriteLine("UserID: " + UserID);
            var reason = param1.ReadInt16();
        }
    }
}
