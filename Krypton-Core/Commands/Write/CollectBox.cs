
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class CollectBox : Command
    {
        public short ID = 26;
        public string Hash { get; set; }
        public uint PlayerX { get; set; }
        public uint PlayerY { get; set; }
        public uint BoxX { get; set; }
        public uint BoxY { get; set; }

        public CollectBox(string hash , int pX , int pY , int bX , int bY)
        {
            Hash = hash;
            PlayerX = (uint)pX;
            PlayerY = (uint)pY;
            BoxX = (uint)bX;
            BoxY = (uint)bY;
            Write();
        }
        public void Write()
        {
            param1.writeByte(0);
            var length = Hash.Length + 20; 
            param1.writeShort((short)length);
            param1.writeShort(ID);
            param1.writeUTF(Hash);
            param1.writeInt((int)((uint)PlayerX >> 1 | (uint)PlayerX << 31));
            param1.writeInt((int)((uint)PlayerY >> 6 | (uint)PlayerY << 26));
            param1.writeInt((int)((uint)BoxX >> 10 | (uint)BoxX << 22));
            param1.writeInt((int)((uint)BoxY << 7 | (uint)BoxY >> 25));

        }
    }
}
