using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unglide;

namespace Krypton_Core.Logic.Gates
{
    public class Zeta : LogicInterface
    {
        private Player KillLast { get; set; }
        MapTraveler Traveler { get; set; }
        public bool Running { get; set; } = false;
        public bool LowHeroHp { get; private set; }
        public bool WaveCompleted { get; private set; }

        private Thread checkHp;

        public Zeta(Api api, Tweener tween) : base(api, tween)
        {
            Traveler = new MapTraveler(Api, Api.Tweener);
        }

        public void Run()
        {

            Thread CheckHpThread = new Thread(new ThreadStart(CheckHpOfHero));
            Thread checkPortsThread = new Thread(new ThreadStart(CheckPort));
            
            while (Running)
            {
                Traveler.TravelToZeta();
                if (!CheckHpThread.IsAlive)
                {
                    CheckHpThread.IsBackground = true;
                    CheckHpThread.Start();
                }
                if(!checkPortsThread.IsAlive)
                {
                    checkPortsThread.IsBackground = true;
                    checkPortsThread.Start();
                }
                if (!CheckMap())
                {
                    //Traveler.TravelToZeta();
                }
                if (WaveCompleted)
                {
                    var port = Api._user.BasesPorts.gates.Find(x => x.ID == 150000263 || x.ID == 150000264 || x.ID == 150000265);
                    Traveler.FlyWithoutMap(port);
                    WaveCompleted = false;
                }
                if (LowHeroHp)
                {
                    RepairHp();
                }

                if (!Api._user.Position.Moving)
                {
                    var mapSize = Collections.Game.Maps.MapSize.SizeOfTheMap(Api._user.userData.MapID);
                    RandomMove((int)mapSize.X, (int)mapSize.Y);

                }

                Thread.Sleep(50);
                
                    Api._user.Target = ClosestNpcReturn();
                

                if (Api._user.Target == null) continue;

                Thread.Sleep(50);
                if (Api._user.Target != null)
                {

                    /*
                        When traveling to npc pos and it changes its position the bot keeps flying to old npc position

                        if (Api._user.Target != null || CalculateDistance(Api._user.Target.X, Api._user.Target.Y) >= 1000))
                        {

                        }
                    */

                    SelectEnemy(Api._user.Target);
                    Api.PushLog($"Clicked: {Api._user.Target.Username}");
                    Api._user.Target = Api._user.Target;
                    FlyToNpc(Api._user.Target);
                    Thread.Sleep(50);
                }
                if (LowHeroHp)
                {
                    RepairHp();
                }


                if (Api._user.Target != null)
                {
                    //AttackNpc("ammunition_laser_lcb-10");
                    Api.PushLog($"Attacking: {Api._user.Target.Username}");
                    Thread.Sleep(100);
                }



                AssignAngle();
                //string? tempName = Api._user.Target.Username;
               
                    while (Api._user.Target != null)
                    {

                        if (CalculateDistance(Api._user.Target.X, Api._user.Target.Y) < 700)
                        {
                            if (checkHp == null || !checkHp.IsAlive)
                            {
                                checkHp = new Thread(new ThreadStart(CheckHp));
                                checkHp.Start();
                            }
                        }
                    try
                    {

                        if (!Api._user.setting.General.Drag)
                        {
                            if (!Api._user.Position.Moving || CalculateDistance(Api._user.Target.X, Api._user.Target.Y) < 700)
                                Circle(Api._user.Target);
                        }
                        else
                        {
                            Drag(Api._user.Target);
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                        if (LowHeroHp)
                        {
                            RepairHp();
                        }

                        if (!Running) Api._user.Target = null;
                        Thread.Sleep(210);
                    }
               
                //Api._user.StatsCollection.AddNpc(tempName);
                //Api.PushLog($"Killed: {tempName}");


               
            }
        }
        private void CheckPort()
        {
            while(true)
            {
                try
                {
                    if(Api._user.BasesPorts.gates.Count <= 0)
                    {
                        WaveCompleted = false;
                        Thread.Sleep(1000);
                        continue;
                    }
                    else
                    {
                        WaveCompleted = true;
                    }
                }
                catch
                {
                    
                }
            }
        }

    



    
        
        private void RepairHp()
        {
            
            
            while (Api._user.shipInfo.Hp < Api._user.shipInfo.MaxHp)
            {
                Circle(22500 / 2 , 13500 / 2);
                RepairRep();
                Thread.Sleep(1000);
            }
        }

        private void CheckHpOfHero()
        {
            while (Running)
            {
                Thread.Sleep(1000);

                while ((double)Api._user.shipInfo.Hp / Api._user.shipInfo.MaxHp * 100 < Api._user.setting.General.RepairAt)
                {

                    LowHeroHp = true;
                    Thread.Sleep(150);
                }
                LowHeroHp = false;
            }
        }
        private void CheckHp()
        {
            try
            {

                if(Api._user.Target.ID == 0)
                {
                    Api._user.players.Remove(0);
                    Api._user.Target = null;
                }
                if (Api._user.Target == null) return;
                var hp = Api._user.Target?.Hp;
                Thread.Sleep(5000);
                if (Api._user.Target == null) return;
                if (hp == Api._user.Target?.Hp)
                {
                   
                    Api._user.Target = null;
                    
                }
            }
            catch (Exception e)
            {

            }
        }



        protected override Player ClosestNpcReturn()
        {
            var npc = Api._user.players.Npcs.ToList().OrderBy(npc => CalculateDistance(new System.Drawing.Point(npc.X, npc.Y))).FirstOrDefault();
            if(npc == null)
            {
                if (KillLast == null)
                {
                    return null;
                }
                else
                {
                    return KillLast;
                }
            }
                if(npc.Username.Contains("Devou"))
                {
                    KillLast = npc;
                    Api._user.players.Remove(npc.ID);
                    npc = null;
                    npc = Api._user.players.Npcs.ToList().OrderBy(npc => CalculateDistance(new System.Drawing.Point(npc.X, npc.Y))).FirstOrDefault();
                    return npc;
                }
                else
                {
                return npc;
                }
            return null;
               
        }
                
            

            
        
    }
}
