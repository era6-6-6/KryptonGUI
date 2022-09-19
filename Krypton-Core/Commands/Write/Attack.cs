using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Managers;

namespace Krypton_Core.Commands.Write
{
    public class Attack : Command
    {
        private const short ID = 4;
        private uint TargetId { get; set; }
        private uint TargetX { get; set; }
        private uint TargetY { get; set; }
        public Attack([NotNull]int id, int targetX, int targetY)
        {
            TargetId = (uint)id;
            TargetX = (uint)targetX;
            TargetY = (uint)targetY;
            Write();
            
        }

        public void Write()
        {
            param1.writeByte(0);
            param1.writeShort(14);
            param1.writeShort(4);
            param1.writeUint(TargetId <<15 | TargetId >> 17);
            param1.writeUint(TargetX << 9 | TargetX >> 23);
            param1.writeUint(TargetY << 9 | TargetY >> 23);
        }
    }
}
