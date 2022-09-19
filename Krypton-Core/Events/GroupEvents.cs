using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Read;
using Krypton_Core.Commands.Read.Group;
using Krypton_Core.Commands.Read.Group.GroupHelpers;
using Krypton_Core.Commands.Write;
using Krypton_Core.Managers;

namespace Krypton_Core.Events
{
    public class GroupEvents
    {
        private Api Api { get; set; }

        public GroupEvents(Api api)
        {
            Api = api;
        }

        public void RegisterEvents()
        {
            Api._user.packetManager.GroupInicialize += (s, e) =>
            {
                Api._user.players.AddGroupMembers(e.UsersInGroup);
            };
            Api._user.packetManager.RemovePlayerFromGroup += (s, e) =>
            {
                Api._user.players.RemovePlayerFromGroup(e.UserID);
            };
            Api._user.packetManager.onGroupDestroyed += (s, e) =>
            {
                Api._user.players.DestroyGroup();
            };
            Api._user.packetManager.onChangeGroupInfo += (s, e) =>
            {
                if (e.Target != null)
                {
                    HandleNewInfo(e.Target , e.UserId);
                }

                if (e.Position != null)
                {
                    HandlePosition(e.Position, e.UserId);

                }

                if (e.HpChanged != null)
                {
                    HandleHp(e.HpChanged , e.UserId);
                }
            };
            Api._user.packetManager.GroupRequestInitied += (s, e) =>
            {
                GroupRequest(e);

            };
        }

        private void GroupRequest(GroupRequest e)
        {
            if (e.UserID == Api._user.setting.GroupSet.LeaderId)
                SendPacket(new AcceptGroup(e.UserID));

        }

      

        private void HandlePosition(PositionChanged position, int ID)
        {
            lock (Api._user.players.PlayersInGroup)
            {
                Player? player = Api._user.players.PlayersInGroup.Find(x => x.ID == ID);
                if (player != null)
                {
                    player.X = position.X;
                    player.MapId = position.MapID;
                    player.Y = position.Y;
                }
            }
        }

        private void HandleHp(HpChanged hpChange, int ID)
        {
            lock (Api._user.players.PlayersInGroup)
            {
                Player? player = Api._user.players.PlayersInGroup.Find(x => x.ID == ID);
                if (player != null)
                {
                    player.Hp = hpChange.Hp;
                    player.Shd = hpChange.Shd;
                    player.MaxHP = hpChange.MaxHp;
                    player.MaxShd = hpChange.MaxShd;
                    player.NanoHp = hpChange.Nano;
                    player.MaxNano = hpChange.MaxNano;
                }
            }
        }

        private void HandleNewInfo(TargetNew rec, int ID)
        {
            lock (Api._user.players.PlayersInGroup)
            {
                Player? player = Api._user.players.PlayersInGroup.Find(x => x.ID == ID);
                if (player != null)
                {
                    var Target = new Player();
                    Target.Username = rec.Name;
                    Target.Hp = rec.TargetInfo.Hp;
                    Target.MaxHP = rec.TargetInfo.MaxHp;
                    Target.Shd = rec.TargetInfo.Shd;
                    Target.MaxShd = rec.TargetInfo.MaxShd;
                    Target.NanoHp = rec.TargetInfo.Nano;
                    Target.MaxNano = rec.TargetInfo.MaxNano;
                    player.Target = Target;
                }

               
            }
        }
        private void SendPacket(Command com)
        {
            Api._user.packetManager?.Send(com);
        }
    }

   
}
