using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Commands.Read.Group.GroupHelpers;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
    public class GroupInfoChanged
    {
        public const short ID = 20584;
        public PositionChanged? Position { get; set; } = null;
        public HpChanged? HpChanged { get; set; } = null;
        public TargetNew? Target { get; set; } = null;
        public int UserId { get; set; }

       

        public GroupInfoChanged(EndianBinaryReader param1)
        {
            var userId = param1.ReadInt32();
            UserId = (int)((uint)userId << 9 | (uint)userId >> 23);
            var loc3 = param1.ReadByte();
            for (int i = 0; i < loc3; i++)
            {
                var id = param1.ReadInt16();
                
                if (id == HpChanged.ID)
                {
                    HpChanged = new HpChanged(param1);


                }
                else if(id == PositionChanged.ID)
                {
                    Position = new(param1);

                }
                else if(id == TargetNew.ID)
                {
                    Target = new(param1);

                }
                else
                {
                    Console.WriteLine("class ID: " + id);
                }

            }
        }

    }
}
