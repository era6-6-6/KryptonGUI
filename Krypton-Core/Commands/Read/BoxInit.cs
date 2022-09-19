

using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read;

    public class BoxInit
    {
        public const short ID = 6868;
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Hash { get; set; }
        public BoxInit(EndianBinaryReader param1)
        {
            param1.ReadInt16();
            var init = new BoxInitX(param1);
            Type = param1.ReadString();
            X = init.X;
            Y = init.Y;
            Hash = init.Hash;
        }
    }

