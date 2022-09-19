
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    internal class SendRocket : Command
    {
        public const short ID = 89;
        public int NpcX { get; set; }
        public int NpcY { get; set; }
        public int RocketType { get; set; }
        public SendRocket(int npcX, int npcY, int rocketType)
        {
            NpcX = npcX;
            NpcY = npcY;
            RocketType = rocketType;
            Write();
        }

        public void Write()
        {
            param1.writeByte(0);
            param1.writeShort(ID);
            param1.writeShort(12);
            param1.writeInt((int)((uint)RocketType << 2 | (uint)RocketType >> 30));
            param1.writeInt((int)((uint)NpcX << 12 | (uint)NpcX>> 20));
            param1.writeInt((int)((uint)NpcY >> 14 | (uint)NpcY << 18));

        }


    }
}
