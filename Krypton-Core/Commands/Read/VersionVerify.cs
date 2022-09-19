using Krypton_Core.Packets.Bytes;
using Command = Krypton_Core.Managers.Command;


namespace Krypton_Core.Commands.Read;

    public class VersionVerify : Command
    {
        private string _version = "8753a5f412c7d8a969778fb6fabd7322";
        public const short ID = 667;

        public string Version
        {
            get => _version;
            set => _version = value;
        }

        public VersionVerify(EndianBinaryReader param1)
        {
            
            Version = param1.ReadString();
        }
        

    }
