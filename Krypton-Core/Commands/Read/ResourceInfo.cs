using Krypton_Core.Collections.Collectables;
using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read
{
    public class ResourceInfo
    {
        public const short ID = 106;
        public List<Resources> resources = new List<Resources>();
        public double total = 0;
        

        public ResourceInfo(EndianBinaryReader param1)
        {
            var loc3 = param1.ReadByte();
            for (int i = 0; i < loc3; i++)
            {
                param1.ReadInt16();
                //105
                param1.ReadInt16();
                //1280
                var type = param1.ReadInt16();
                //back 105
                var count = param1.ReadDouble();
                if(type != 3)
                {
                    total += count;
                }
                Console.WriteLine("Type: " + type);
                Console.WriteLine("Count " + count);
                Resources res = new Resources();
                res.Type = type;
                res.Count = count;
                resources.Add(res);
            }

        }
    }
}
