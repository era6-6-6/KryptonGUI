
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class Move : Command
    {
        public const short ID = 82;
        public uint X { get; set; }
        public uint Y { get; set; }
        public uint CurY { get; set; }
        public uint CurX { get; set; }

        public Move(uint x , uint y, uint curX , uint curY)
        {
            X = x;
            Y = y;
            CurX = curX;
            CurY = curY;
            Write();
        }
        private void Write()
        {
            param1.writeByte(0);
            param1.writeShort(18);
            param1.writeShort(ID);
            param1.writeUint(CurX << 11 | CurX >> 21);
            param1.writeUint(Y >> 15 | Y << 17);
            param1.writeUint(X << 13 | X >> 19);
            param1.writeUint(CurY << 10 | CurY >> 22);
            
        }

    }
}
