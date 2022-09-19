using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read;

    public  class BoxInitX
    {
        public const short ID = 31650;

        public string Hash { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public BoxInitX(EndianBinaryReader param1)
        {
            this.Hash = param1.ReadString();
            
            this.X = param1.ReadInt32();
            this.X = (int)((uint)this.X << 1 | (uint)this.X >> 31);
            this.Y = param1.ReadInt32();
            this.Y = (int)((uint)this.Y >> 11 | (uint)this.Y << 21);
            
        }
        
    }

