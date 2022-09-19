
using Krypton_Core.Packets.Bytes;
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Read;

    
    public class GateInit : Command
    {
        public const short ID = 3389;
        private List<int> var67 = new List<int>();
        public int GateID { get; set; }
        public int TypeID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public GateInit(EndianBinaryReader param1)
        {
            var var_866 = param1.ReadInt32();
            GateID = (int)((uint)var_866 >> 10 | (uint)var_866 << 22);
            var  factionId = param1.ReadInt32();
            factionId = (int)((uint)factionId << 2 | (uint)factionId >> 30);
            var name_183 = param1.ReadInt32();
            TypeID = (int)((uint)name_183 << 15 | (uint)name_183 >> 17);
            X = param1.ReadInt32();
            X = (int)((uint)X >> 11 | (uint)X << 21);
            Y = param1.ReadInt32();
            Y = (int)((uint)Y >> 9 | (uint)Y << 23);
            var var_3863 = param1.ReadBoolean();
            var var_2997 = param1.ReadBoolean();
         


        }

    }

