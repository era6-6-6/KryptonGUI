using Krypton_Core.Collections.Game.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unglide;

namespace Krypton_Core.Logic
{
    public class pushing : LogicInterface
    {

        public pushing(Api api , Tweener twenn) : base(api , twenn)
        {
            
        }


        int total = 1;
        public void Run()
        {
            while(true)
            {
                try
                {  
                    Player? player = Api._user.players.EnemyPlayers.Find(x => x.ID == 170623524);
                    if(player == null)
                    {
                        player = Api._user.players.EnemyPlayers.Find(x => x.ID == 172444901);
                    }
                    Thread.Sleep(100);
                    while(player == null)
                    {
                        Thread.Sleep(200);
                        player = Api._user.players.EnemyPlayers.Find(x => x.ID == 170623524);
                        if(player == null)
                        {
                            player = Api._user.players.EnemyPlayers.Find(x => x.ID == 172444901);
                        }
                    }
                    if (Api._user.players.EnemyPlayers.Contains(player))
                    {
                        Api._user.Target = player;
                        FlyToNpc(Api._user.Target);
                        Thread.Sleep(200);
                        SelectEnemy(player);
                        if (CalculateDistance(player.X, player.Y) < 700)
                        {
                            SendPacket(new Krypton_Core.Commands.Write.Action(Api._user.setting.AmmoSettings.Ammo, 0, 1));
                            while (Api._user.Target != null)
                            {
                                FlyToNpc(Api._user.Target);
                                Thread.Sleep(2000);
                                SendPacket(new Krypton_Core.Commands.Write.Action(Api._user.setting.AmmoSettings.Ammo, 0, 1));
                            }
                            total++;
                        }
                        Console.Clear();
                        Console.WriteLine("total kills" +  total);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }
}
