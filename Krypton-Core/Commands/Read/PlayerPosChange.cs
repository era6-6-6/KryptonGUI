using Krypton_Core.Packets.Bytes;


namespace Krypton_Core.Commands.Read
{
    public class PlayerPosChange
    {
        public const ushort ID = 90;
        public int UserID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Duration { get; set; }
        public PlayerPosChange(EndianBinaryReader param1)
        {
            this.UserID = param1.ReadInt32();
            this.UserID = (int)((uint)this.UserID >> 16 | (uint)this.UserID << 16);
            this.X = param1.ReadInt32();
            this.X = (int)((uint)this.X >> 13 | (uint)this.X << 19);
            this.Y = param1.ReadInt32();
            this.Y = (int)((uint)this.Y << 16  | (uint)this.Y >> 16);
            this.Duration = param1.ReadInt32();
            this.Duration = (int)((uint)this.Duration << 14 | (uint)this.Duration >> 18);
        }
    }
}
