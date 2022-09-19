
using Krypton_Core;
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class Repair : Command
    {
        public const short ID = 255;
        User _User { get; set; }
        private short Selection { get; set; }
        public Repair(short selection , User user)
        {
            _User = user;
            Selection = selection;
            Write();
        }
        private void Write()
        {
            param1.writeByte(0x00);
            param1.writeShort(55);
            param1.writeShort(ID);
            param1.writeShort(254);
            param1.writeShort(Selection);
            param1.writeShort(7);
            param1.writeInt((int)((uint)_User.userData.UserID << 3 | (uint)_User.userData.UserID >> 29));
            param1.writeShort((short)(65535 & ((65535 & (ushort)_User.userData.FactionID) >> 9 | (65535 & (ushort)_User.userData.FactionID) << 7)));
            param1.writeUTF(_User.userData.SID);
            param1.writeUTF("");
            param1.writeInt((int)((uint)0 << 12 | (uint)0 >> 20));
            param1.writeBoolean(false);
            //new Login(_User.UserID, 0, "", _User.SID, (short) _User.FactionID).Repair();

        }
    }
}
