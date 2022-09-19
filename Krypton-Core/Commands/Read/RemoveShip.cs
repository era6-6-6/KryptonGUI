using Krypton_Core.Packets.Bytes;


namespace Krypton_Core.Commands.Read
{
    public class RemoveShip
    {
        public const short ID = 52;
        public int UserID { get; set; }
        public RemoveShip(EndianBinaryReader param1)
        {
            this.UserID = param1.ReadInt32();
            this.UserID = (int)((uint)this.UserID >> 5 | (uint)this.UserID << 27);
        }

    }
}
