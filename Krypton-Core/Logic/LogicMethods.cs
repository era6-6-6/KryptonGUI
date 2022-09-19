using Krypton_Core.Collections.Collectables;
using Krypton_Core.Collections.Game.Ammo;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Write;
using Krypton_Core.Settings;
using Unglide;
using Action = Krypton_Core.Commands.Write.Action;

namespace Krypton_Core.Logic
{
    public class LogicMethods : LogicInterface
    {

        public bool Running { get; set; } = false;
        Thread checkHp { get; set; }
        Thread SendGroup { get; set; }
        

        #region Logic Bools
        bool EnemyIsAround { get; set; } = false;
        bool LowHeroHp { get; set; } = false;
        public bool Logout { get; set; }

        #endregion
        public event EventHandler<LogicMethods>? onStop;
        

        public LogicMethods(Api api, Tweener tween) : base(api, tween)
        {
            
            RegisterEvents();
        }

        private void RegisterEvents()
        {
           
          

        }
        private Thread CheckHpThread { get; set; }
        public async Task StartMethod()
        {
            _ = Task.Run(async () => await CheckEnemyAround());
            _ = Task.Run(async () => await CheckHpOfHero());
            _ = Task.Run(async () => await SendGroupRequest());
            _ = Task.Run(async () => await ChangeFormation());
            _ = Task.Run(async () => await AutoRocket());
            _ = Task.Run(async () => await CheckConfig());
            _ = Task.Run(async () => await PetChecker());
            


            while (Running)
            {
                try
                {
                    
                    if (!CheckMap())
                    {
                       await Travel();
                       while (!Api._user.userData.Ready)
                       {
                           await Task.Delay(1000);
                       }
                       //await Task.Delay(3000);
                        
                    }
                    
                    if (LowHeroHp)
                    {
                        while (Api._user.Destroyed)
                        {
                            await Task.Delay(150);
                        }
                        await RepairHp();
                    }

                    if (!Api._user.Position.Moving)
                    {
                        var mapSize = Collections.Game.Maps.MapSize.SizeOfTheMap(Api._user.userData.MapID);
                        RandomMove((int)mapSize.X, (int)mapSize.Y);
                        
                    }

                    await Task.Delay(50);
                    if (IsAnyNpcAround())
                    {
                        Api._user.Target = ClosestNpc();
                        
                    }
                    else if (IsAnyBoxAround())
                    {
                        Api._user.Box = ClosestBox();
                        if (Api._user.Box == null) continue;

                        FlyToCorndinates(Api._user.Box.X, Api._user.Box.Y);
                        if(Api._user.Box == null) continue;
                        
                        while (CalculateDistance(Api._user.Box.X, Api._user.Box.Y) > 50)
                        {
                            await Task.Delay(100);
                        }


                        SendPacket(new CollectBox(Api._user.Box.Hash, Api._user.Position.X, Api._user.Position.Y, Api._user.Box.X, Api._user.Box.Y));
                        int i = 0;
                        while (Api._user.Box != null && i < 10)
                        {
                            await Task.Delay(50);
                            i++;
                        }
                        Api._user.Boxes.Remove(Api._user.Box?.Hash);


                    }

                    if (Api._user.Target == null) continue;

                    await Task.Delay(50);
                    if (Api._user.Target != null)
                    {

                        /*
                            When traveling to npc pos and it changes its position the bot keeps flying to old npc position

                            if (Api._user.Target != null || CalculateDistance(Api._user.Target.X, Api._user.Target.Y) >= 1000))
                            {

                            }
                        */

                        //SelectEnemy(Api._user.Target);
                        Api.PushLog($"Clicked: {Api._user.Target.Username}");
                        Api._user.Target = Api._user.Target;
                        FlyToNpc(Api._user.Target);
                        await Task.Delay(50);
                    }
                    if (LowHeroHp)
                    {
                        await RepairHp();
                    }
                    if (EnemyIsAround)
                    {
                        await FleeFromEnemy();
                    }

                    if (Api._user.Target != null)
                    {
                        
                        while(CalculateDistance(Api._user.Target.X , Api._user.Target.Y) > 600)
                        {
                            await Task.Delay(30);
                        }
                        if (Api._user.Target.isTaken)
                        {
                            Api._user.Target = null;
                            continue;

                        }
                        AttackNpc(Api._user.Target);
                        Api.PushLog($"Attacking: {Api._user.Target.Username}");
                        await Task.Delay(100);
                    }



                    bool init = true;
                    string? tempName = Api._user.Target.Username;
                    ChangeAmmo(Api._user.Target?.Settings.Ammo);
                    while (Api._user.Target != null)
                    {

                       
                        if (CalculateDistance(Api._user.Target.X, Api._user.Target.Y) < 700)
                        {
                            if (CheckHpThread == null || !CheckHpThread.IsAlive)
                            {
                                CheckHpThread = new Thread(new ThreadStart(CheckHp));
                                CheckHpThread.Start();
                            }
                            

                        }
                        

                        if (!Api._user.setting.General.Drag)
                        {
                            if (init)
                            {
                                AssignAngle();
                                init = false;
                            }

                            if (!Api._user.Position.Moving ||
                                CalculateDistance(Api._user.Target.X, Api._user.Target.Y) < 500)
                            {
                                if ((double)Api._user.Target.Hp / Api._user.Target.MaxHP * 100 < 25 && Api._user.setting.General.StopCircle)
                                {
                                    FlyToNpc(Api._user.Target);
                                }
                                else
                                {
                                    Circle(Api._user.Target);
                                }

                                if (IsAnyBoxAround())
                                {
                                    Api._user.Box = ClosestBox();
                                    if (Api._user.Box != null)
                                    {
                                        if (CalculateDistance(Api._user.Box.X, Api._user.Box.Y) < 300)
                                        {
                                            FlyToCorndinates(Api._user.Box.X, Api._user.Box.Y);
                                            while (CalculateDistance(Api._user.Box.X, Api._user.Box.Y) > 50)
                                            {
                                                FlyToCorndinates(Api._user.Box.X, Api._user.Box.Y);
                                                await Task.Delay(200);

                                            }

                                            if (Api._user.Box != null)
                                                SendPacket(new CollectBox(Api._user.Box.Hash, Api._user.Position.X, Api._user.Position.Y,
                                                    Api._user.Box.X,
                                                    Api._user.Box.Y));
                                            int i = 0;
                                            while (Api._user.Box != null && i < 10)
                                            {
                                                await Task.Delay(50);
                                                i++;
                                            }

                                            if (Api._user.Box != null)
                                            {
                                                Api._user.Boxes.Remove(Api._user.Box);
                                                Api._user.Box = null;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            Drag(Api._user.Target);
                        }
                        if (LowHeroHp)
                        {
                            await RepairHp();
                        }
                        
                        if (EnemyIsAround)
                        {
                            await FleeFromEnemy();
                        }
                        if (!Running) Api._user.Target = null;
                        await Task.Delay(210);
                    }

                    Api._user.events.Shooting = false;
                    Api._user.StatsCollection.AddNpc(tempName);
                    Api.PushLog($"Killed: {tempName}");
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    
                }

            }

            if (Logout)
            {
                var port = ClosestGate();
                FlyToCorndinates(port.X, port.Y);
                while (Api._user.Position.X != port.X || Api._user.Position.Y != port.Y)
                {
                    await Task.Delay(50);
                }

                await Task.Delay(5000);
                SendPacket(new Logout());
                Api._user.LogoutInvoked = true;
                var delay = Api._user.statistics.Premium ? 6000 : 22000;
                Console.WriteLine("Loging off in " + delay / 1000 + "s");
                await Task.Delay(delay);
                onStop?.Invoke(this, this);
                Api.OnBotStop();
            }
            else
            {
                FlyToCorndinates(Api._user.Position.X , Api._user.Position.Y);
            }

        }
        private async Task PetChecker()
        {
            while (true)
            {

                await Task.Delay(5000);
                if (!Api._user.setting.PET.UsePet)
                {
                    continue;
                }
                if (Api._user.players.Pet == null)
                {
                    SendPacket(new InvokePet());
                }

                SendPacket(new PetSelectModule((short) Api._user.setting.PET.PetModule));
            }
        }

        private async Task CheckConfig()
        {
            while (true)
            {

                await Task.Delay(2000);
                if(Api._user.events.CanChangeConfig == false) continue;
                if (Api._user.Target != null &&
                    CalculateDistance(Api._user.Target.X, Api._user.Target.Y) < 700)
                {
                    if (Api._user.shipInfo.Config == Api._user.setting.Configs.KillingCnf) continue;
                    Api.ChangeConfig(Api._user.setting.Configs.KillingCnf);
                }
                else if (Api._user.Target != null)
                {
                    if (Api._user.shipInfo.Config == Api._user.setting.Configs.RepairConfig) continue;
                    Api.ChangeConfig(Api._user.setting.Configs.RepairConfig);
                }
                else
                {
                    if (Api._user.shipInfo.Config == Api._user.setting.Configs.RunningConfig) continue;
                    Api.ChangeConfig(Api._user.setting.Configs.RepairConfig);
                }
             
            }
        }

        private bool Done = true;
        private int tries = 0;
        private void CheckHp()
        {
            try
            {
                //TODO: Do this !!!!
                
                Console.WriteLine("Done: " + Done);
                Console.WriteLine("Tries: " + tries);
                var target = Api._user.Target?.ID;
                var hp = Api._user.Target.Hp;
                Thread.Sleep(1500);
                if(Api._user.Target == null || Api._user.Target.ID != target) return;
                if (!Api._user.events.Shooting && Api._user.Target != null && tries < 1)
                {
                    AttackNpc(Api._user.Target);
                    tries++;
                }
                if(hp == Api._user.Target.Hp)
                {
                    Api._user.players.NpcsToShoot.Remove(Api._user.Target);
                    Api._user.players.Remove(Api._user.Target.ID);
                    Api._user.Target = null;
                    tries = 0;
                }
                else if(tries >= 2)
                {
                    Api._user.players.NpcsToShoot.Remove(Api._user.Target);
                    Api._user.players.Remove(Api._user.Target.ID);
                    Api._user.Target = null;
                    tries = 0;
                    
                }
                

                Done = true;
                Console.WriteLine(Done);
            }
            catch (Exception e)
            {
                Done = true;
            }
        }

        private async Task ChangeFormation()
        {
            while (Running)
            {

                await Task.Delay(2000);
                if (Api._user.Target != null)
                {
                    if(Api._user.setting.Configs.KillFormation == Api._user.InGameCollection.ActualFormation) continue;
                    
                    SendPacket(new Action(Api._user.setting.Configs.KillFormation, 1, 1));
                    Api._user.InGameCollection.ActualFormation = Api._user.setting.Configs.KillFormation;
                    await Task.Delay(2000);
                }
                else
                {
                    if (Api._user.setting.Configs.RepairFormation == Api._user.InGameCollection.ActualFormation) continue;
                    SendPacket(new Action(Api._user.setting.Configs.RepairFormation, 1, 1));
                    Api._user.InGameCollection.ActualFormation = Api._user.setting.Configs.RepairFormation;
                    await Task.Delay(2000);
                }
            }
        }

        private async Task FleeFromEnemy()
        {
            var closesGate = ClosestGate();
            FlyToCorndinates(closesGate.X, closesGate.Y);
            Api._user.Target = null;
            while (EnemyIsAround)
            {
                RepairRep();
                await Task.Delay(1000);
            }
        }

        private async Task AutoRocket()
        {
            while (true)
            {
                var delay = 0;
                delay = Api._user.statistics.Premium ? 1500 : 3000;
                await Task.Delay(delay);
                if (Api._user.Target != null)
                {
                    SendPacket(new Action(AmmoCollection.PLT_2026 , 0 , 1));
                }
            }
            
        }

        private async Task RepairHp()
        { 
            var closesGate = ClosestGate();
            FlyToCorndinates(closesGate.X , closesGate.Y);
            Api._user.Target = null;
            while (Api._user.shipInfo.Hp < Api._user.shipInfo.MaxHp)
            {
                RepairRep();
                await Task.Delay(1000);
            }
        }

        private async Task CheckEnemyAround()
        {
            while (Running)
            {
                await Task.Delay(1000);
                while(Api._user.players.EnemyPlayers.Count > 0 && Api._user.setting.General.RunFromEnemies)
                {
                    EnemyIsAround = true;
                    await Task.Delay(150);
                }
                EnemyIsAround = false;
            }
        }
        private async Task CheckHpOfHero()
        {
            while (Running)
            {
                await Task.Delay(1000);

                while((double)Api._user.shipInfo.Hp / Api._user.shipInfo.MaxHp * 100 < Api._user.setting.General.RepairAt)
                {
                   
                    LowHeroHp = true;
                    await Task.Delay(150);
                }
                LowHeroHp = false;
            }
        }
        //TODO: check group requests 
        private bool first = false;
        private async Task SendGroupRequest()
        {
            while (Running)
            {
                await Task.Delay(5000);
                if (Api._user.setting.GroupSet.Leader == false) continue;
                foreach (var user in Api._user.setting.GroupSet.Members)
                {
                    SendPacket(new SendGroup(user.Username));
                    await Task.Delay(1000);

                    
                }
                SendPacket(new GroupAllowRequests());
            }
        }

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }
}
