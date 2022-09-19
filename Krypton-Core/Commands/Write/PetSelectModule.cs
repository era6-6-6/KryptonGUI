using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Write
{
    public class PetSelectModule : Command
    {
        public const short ID = 154;
        public const short classID = 164;
        public short moduleID = 0;

        public PetSelectModule(short moduleId)
        {
            this.moduleID = moduleId;
            Write();
        }
        
        public void Write()
        {
            param1.writeByte(0);
            param1.writeShort(8);
            param1.writeShort(ID);
            param1.writeShort(classID);
            param1.writeShort(moduleID);
            param1.writeShort(0);

        }

    }
}
