using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read
{
    public class ShooterInfo
    {
        public const short ID = 3;
        public int AttackerID { get; set; }
        public int TargetID { get; set; }
        

        public ShooterInfo(EndianBinaryReader param1 , Api api)
        {
            AttackerID = param1.ReadInt32();
            AttackerID = (int)((uint)AttackerID >> 2 | (uint)AttackerID << 30);
            TargetID = param1.ReadInt32();
            TargetID = (int)((uint)TargetID<< 1 | (uint)TargetID >> 31);

            if(AttackerID == api._user.userData.UserID)
            {
                api._user.events.Shooting = true;
            }
            if(AttackerID != api._user.userData.UserID && TargetID != api._user.userData.UserID)
            {
                var npc = api._user.players.Npcs.Find(x => x.ID == TargetID);
                if(npc != null)
                {
                    npc.isTaken = true;
                }
               
            }

        }
    }
}
