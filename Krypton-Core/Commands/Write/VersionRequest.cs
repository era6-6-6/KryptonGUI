
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class VersionRequest : Command
    {
        public const short ID = 666;
        private string Version { get; set; } = "2241526a5500be7996093d57b540d129";
        public VersionRequest(string version)
        {
            Write();
        }
        private void Write()
        {
            short totalLength = (short)(Version.Length + 4);
            param1.writeInt(totalLength);
            param1.writeShort(666);
            param1.writeUTF(Version);

        }

    }
}
