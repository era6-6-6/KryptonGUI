
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class TrackVar : Command
    {
        public TrackVar()
        {
            Write();
        }
        public void Write()
        {
            param1.writeByte(0x00);
            param1.writeByte(0x00);
            param1.writeByte(0x17);
            param1.writeByte(0x5c);
            param1.writeByte(0x82);
            param1.writeByte(0x01);
            param1.writeByte(0xa4);
            param1.writeByte(0xc7);
            param1.writeByte(0x13);
            param1.writeByte(0xd5);
            param1.writeByte(0x00);
            param1.writeByte(0x0a);
            param1.writeByte(0x6d);
            param1.writeByte(0x61);
            param1.writeByte(0x70);
            param1.writeByte(0x5f);
            param1.writeByte(0x63);
            param1.writeByte(0x6c);
            param1.writeByte(0x69);
            param1.writeByte(0x63);
            param1.writeByte(0x6b);
            param1.writeByte(0x73);
            param1.writeByte(0x08);
            param1.writeByte(0x00);
            param1.writeByte(0x00);
            param1.writeByte(0x00);
        }
    }
}
