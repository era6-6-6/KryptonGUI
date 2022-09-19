
using Command = Krypton_Core.Managers.Command;

//USE THIS INLY IF YOU WANT TO TEST ANY PACKET!!!!
namespace Krypton_Core.Commands.Write
{
    public class TestP : Command
    {
        public TestP()
        {
            Write();    
        }

        public void Write()
        {
            param1.writeByte(0);
            param1.writeByte(0);
            param1.writeByte(0x06);
            param1.writeByte(0x73);
            param1.writeByte(0xcb);
            param1.writeByte(0x00);
            param1.writeByte(0x00);
            param1.writeByte(0x01);
            param1.writeByte(0x01);
        }
        


    }
    
}
