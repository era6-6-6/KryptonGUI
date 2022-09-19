using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Write;
using Krypton_Core.Managers;
using Managers;
using System.Drawing;
using Unglide;
using Action = Krypton_Core.Commands.Write.Action;


namespace Krypton_Core.Logic.NpcLogic
{
    public class NpcLogicAsync : LogicInterface
    {
        public bool logicRunning = false;
        Thread? checkHp;

        MapTraveler mapTraveler;


        public NpcLogicAsync(Api api, Tweener tween) : base(api, tween)
        {
            mapTraveler = new(Api, tween);
        }

        [Obsolete]
        public async Task Start()
        {
            Thread thread = new Thread(new ThreadStart(NpcLogicStartMethod));
            thread.Suspend();


        }

        public async void NpcLogicStartMethod()
        {
            Log("Npc Killer Started succesfully");
            Player? npc;


            while (logicRunning)
            {
                try
                {

                    while (!Api._user.userData.Ready)
                    {
                        Console.WriteLine("Acount not ready yet");
                        await Task.Delay(100);
                    }
                    if (!CheckMap())
                    {
                   
                        await Task.Delay(7000);
                    }
                    if (!Api._user.Position.Moving)
                    {
                        PointF mapSize = Krypton_Core.Collections.Game.Maps.MapSize.SizeOfTheMap(Api._user.userData.MapID);
                        RandomMove((int)mapSize.X, (int)mapSize.Y);
                    }
                   

                    await Task.Delay(50);

                    npc = ClosestNpc();

                    if (npc == null) continue;
                    else
                    {
                        Api.PushLog($"Clicked: {npc.Username}");
                        SelectEnemy(npc);
                        Api._user.Target = npc;
                        await Task.Delay(100);
                        if (npc.isTaken)
                        {
                            Api._user.players.NpcsToShoot.Remove(npc);
                            continue;
                        }

                        //AttackNpc(Api._user.setting.AmmoSettings.Ammo);
                        Log($"Attacking: {npc.Username} ");
                        


                    }
                    await Task.Delay(50);

                    FlyToNpc(npc);
                    await Task.Delay(100);


                    if (((Api._user.shipInfo.Hp / Api._user.shipInfo.MaxHp) * 100) < ((Api._user.shipInfo.Hp / Api._user.shipInfo.MaxHp) * 25))
                    {
                        var ports = ClosestGate();

                        FlyToCorndinates(ports.X, ports.Y);
                        while (Api._user.shipInfo.Hp < Api._user.shipInfo.MaxHp)
                        {
                            await Task.Delay(1000);
                            RepairRep();
                        }
                    }



                    AssignAngle();
                    SendPacket(new Action(Api._user.setting.Configs.KillFormation, 0, 1));

                    AssignAngle();
                    while (Api._user.Target != null)
                    {
                        if (CalculateDistance(npc.X, npc.Y) < 700)
                        {
                            if (checkHp == null || !checkHp.IsAlive)
                            {
                                checkHp = new Thread(new ThreadStart(CheckHp));
                                checkHp.Start();
                            }
                        }

                        await Task.Delay(50);
                        if (!Api._user.setting.General.Drag)
                        {
                            if (!Api._user.Position.Moving)
                                Circle(npc);

                        }
                        else
                        {
                            Drag(npc);

                        }


                        await Task.Delay(50);
                        if (Api._user.Target.Hp == 0)
                        {
                            Api._user.players.Remove(Api._user.Target.ID);
                            Api._user.Target = null;
                        }

                    }
                    SendPacket(new Action(Api._user.setting.Configs.RunFormation, 0, 1));
                    
                   
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            
            }
            await Task.Delay(50);
            var port = ClosestGate();
            FlyToCorndinates(port.X, port.Y);

        }

        private void CheckHp()
        {
            try
            {
                if (Api._user.Target == null) return;
                var hp = Api._user.Target?.Hp;
                Thread.Sleep(5000);
                if (Api._user.Target == null) return;
                if (hp == Api._user.Target?.Hp)
                {
                    Api._user.players.Remove(Api._user.Target.ID);
                    Api._user.Target = null;

                }
            }
            catch (Exception e)
            {
                CrashLogManager.Send(Api._user.logMsg, $"Error while checking {Api._user.Name} HP", CrashLogManager.Type.ERROR);
                CrashLogManager.Send(Api._user.logMsg, e.Message, CrashLogManager.Type.ERROR);
            }
        }

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }
}
