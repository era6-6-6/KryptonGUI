using Krypton_Core.Managers;
using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read.Group
{
    public class GroupRequest : Command
    {
        public const short ID = -21743;

        public string? Username { get; private set; }
        public int UserID { get; private set; }

        public string? RecName { get; private set; }
        public GroupRequest(EndianBinaryReader param1)
        {
            var uk2 = param1.ReadInt32();
            uk2 = (int)((uint)uk2 << 5 | (uint)uk2 >> 27);

            param1.ReadInt16();
            param1.ReadInt16();

            param1.ReadInt16();
            var uk = param1.ReadString();
            Console.WriteLine(uk);


            param1.ReadInt16();

            param1.ReadInt16();

            param1.ReadInt16();
            var uk4 = param1.ReadInt32();
            uk4 = uk4 >> 6 | uk4 << 26;


            var uk3 = param1.ReadString();

            RecName = uk;
            UserID = uk2;
            Username = uk3;


            Console.WriteLine(uk); //recname
            Console.WriteLine(uk2);//userid
            Console.WriteLine(uk3);//sender name
            Console.WriteLine(uk4); //req id
















        }
    }
}
