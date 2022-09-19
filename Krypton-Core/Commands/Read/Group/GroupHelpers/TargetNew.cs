using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read.Group.GroupHelpers
{
    public class TargetNew
    {
        public const short ID = -21094;
        public HpChanged? TargetInfo { get; set; } = null;
        public string Name { get; set; } = "";
        public TargetNew(EndianBinaryReader param1)
        {
            param1.ReadInt16();
            Name = param1.ReadString();
            
            //class 446
            param1.ReadInt16();
            param1.ReadInt16();
            param1.ReadInt16();
            //1012
            param1.ReadInt16();

            TargetInfo = new HpChanged(param1);


        }
    }
}
