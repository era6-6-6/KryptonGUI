using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class Login : Command
    {
        public const short ID = 7;
        private int UserID { get; set; }
        private int InstanceID { get; set; }
        private string Version { get; set; }
        private string SID { get; set; }
        private short FactionID { get; set; }
        public Login(int userID, int instanceID, string version, string sid, short factionID)
        {
            UserID = userID;
            InstanceID = instanceID;
            Version = version;
            SID = sid;
            FactionID = factionID;
            Write();
        }


        public void Write()
        {
            param1.writeByte(0);
            param1.writeShort(49);
            param1.writeShort(7);
            param1.writeInt((int)((uint)UserID << 3 | (uint)UserID >> 29));
            param1.writeShort((short)(65535 & ((65535 & (ushort)FactionID) >> 9 | (65535 & (ushort)FactionID) << 7)));
            param1.writeUTF(SID);
            param1.writeUTF("");
            param1.writeInt((int)((uint)InstanceID << 12 | (uint)InstanceID >> 20));

            param1.writeBoolean(true);
        }
        public void Repair()
        {
            param1.writeShort(7);
            param1.writeInt((int)((uint)UserID << 3 | (uint)UserID >> 29));
            param1.writeShort((short)(65535 & ((65535 & (ushort)FactionID) >> 9 | (65535 & (ushort)FactionID) << 7)));
            param1.writeUTF(SID);
            param1.writeUTF("");
            param1.writeInt((int)((uint)InstanceID << 12 | (uint)InstanceID >> 20));
            param1.writeBoolean(true);
        }


    }
}
