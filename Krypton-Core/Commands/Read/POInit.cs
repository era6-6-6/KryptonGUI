using Krypton_Core.Packets.Bytes;
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Read;

public class POInit : Command
{
    public const short ID = -5676;
    public List<int> points = new List<int>();
    public string type { get; set; } = "barrier";
    public string Name { get; set; } = null;
    public bool Active { get; set; }
    public bool Visible { get; set; }

    public POInit(EndianBinaryReader param1)
    {
        param1.ReadInt16();
        param1.ReadInt16();
        var shape = param1.ReadInt16();

        param1.ReadInt16();
        param1.ReadInt16();

        type = param1.ReadString();
        Visible = param1.ReadBoolean();
        Name = param1.ReadString();
        
        Active = param1.ReadBoolean();
        

        var temp = param1.ReadByte();
        for (int i = 0; i < temp; i++)
        {
            var s = param1.ReadInt32();
            s = s >> 6 | s << 26;
           
            points.Add(s);

        }

      
    


        



       

    }
}

