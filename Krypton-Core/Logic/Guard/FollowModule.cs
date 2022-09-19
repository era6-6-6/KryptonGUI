using Krypton_Core.Collections.Game.Ammo;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unglide;
using Action = Krypton_Core.Commands.Write.Action;

namespace Krypton_Core.Logic.Guard
{
    public class FollowModule : LogicInterface
    {

        public bool Running { get; set; } = false;

        public FollowModule(Api api, Tweener tween) : base(api, tween)
        {

        }

        private void EventRegister()
        {
            Api.Sr.onShipAttack += (s, e) =>
            {
                Api._user.Target = e;

                SendPacket(new SelectEnemy(e.ID, e.Y, e.X, Api._user.Position.X, Api._user.Position.Y));
                SendPacket(new Action(AmmoCollection.LCB_10, 1, 0));

            };
        }

        private bool LeaderAttacking()
        {
            if (Api._user.players.PlayersInGroup.Find(x => x.ID == Api._user.setting.FollowUserID).Target != null)
            {
                return true;
            }

            return false;
        }

        public async Task StartFollow()
        {
            EventRegister();
            bool isInGroup = false;
            Player? player = Api._user.players.AllPlayers.Find(x => x.ID == Api._user.setting.FollowUserID);
            while (Running)
            {

                while (player == null)
                {

                    await Task.Delay(1000);
                    Console.WriteLine($"Waiting for player with id : {Api._user.setting.FollowUserID}");
                    player = Api._user.players.AllPlayers.Find(x => x.ID == Api._user.setting.FollowUserID);

                }

                Console.WriteLine($"Player found with id : {Api._user.setting.FollowUserID}");
                await Task.Delay(100);

                while (player != null)
                {
                    if (!isInGroup)
                    {
                        SendPacket(new SendGroup(player.Username));
                        isInGroup = true;
                    }

                    if (Api._user.shipInfo.Hp < (Api._user.shipInfo.MaxHp / 100) * 25)
                    {
                        RepairRep();
                    }






                    //We need to update it again
                    player = Api._user.players.AllPlayers.Find(x => x.ID == Api._user.setting.FollowUserID);
                    if (player == null) break;

                    //if (CalculateDistance(player.X, player.Y, Api._user.Position.X, Api._user.Position.Y) > 900) break;
                    Random rnd = new Random();
                    if (
                        (Api._user.Position.Moving == false &&
                         CalculateDistance(player.X, player.Y, Api._user.Position.X, Api._user.Position.Y) >= 250) ||

                        (CalculateDistance(player.X, player.Y, Api._user.Position.X, Api._user.Position.Y) >= 650)
                    )
                    {
                        Console.WriteLine($"Following {Api._user.setting.FollowUserID}");
                        FlyToCorndinates(player.X, player.Y);
                        await Task.Delay(100);
                    }
                }





                if (player == null)
                {
                    Console.WriteLine($"Lost player {Api._user.setting.FollowUserID}");
                    Console.WriteLine("Searching for the closest Port");
                    var closestPort = ClosestGate();
                    if (closestPort != null)
                    {
                        Console.WriteLine($"Found the closest port");
                        if (CalculateDistance(Api._user.Position.X, Api._user.Position.Y, closestPort.X,
                                closestPort.Y) <
                            1500)
                        {
                            Console.WriteLine(
                                $"The user seems to jumped to a portal - {CalculateDistance(Api._user.Position.X, Api._user.Position.Y, closestPort.X, closestPort.Y)} is < 1500");
                            while (Api._user.Position.X != closestPort.X && Api._user.Position.Y != closestPort.Y)
                            {
                                Console.WriteLine($"Going to the portal x:{closestPort.X} y:{closestPort.Y}");
                                if (Api._user.Position.Moving == false)
                                {
                                    FlyToCorndinates(closestPort.X, closestPort.Y);
                                }

                                await Task.Delay(1000);
                            }

                            Console.WriteLine("Jumping the portal");
                            SendPacket(new JumpPortal());
                            Api.CleanLists();
                            await Task.Delay(5000);
                        }
                    }

                }

                await Task.Delay(100);
            }
        }







        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }

}
