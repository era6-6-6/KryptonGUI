using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class Action : Command
    {
        public const short ID = -27647;
        private string ActionStr { get; set; }

        public short Unknown { get; set; }
        public short Unknow1 { get; set; }

        public Action(string action , short uk , short uk1)
        {
            ActionStr = action;
            Unknown = uk;
            Unknow1 = uk1;
            Write();
        }
        public void Write()
        {
            param1.writeByte(0x00);
            var length = ActionStr.Length + 8;
            param1.writeShort((short)length);
            param1.writeShort(ID);
            param1.writeShort(Unknown);
            param1.writeUTF(ActionStr);
            param1.writeShort(Unknow1);
            
            
        }

    }
}
