
using Krypton_Core.Commands;
using Krypton_Core.Commands.Read;
using Krypton_Core.Commands.Read.Group;
using Krypton_Core.Commands.Write;
using Krypton_Core.Events;
using Krypton_Core.InternalPacketsMethods;
using Krypton_Core.Packets.Bytes;
using Managers;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Channels;
using Krypton_Core.Collections.Game.Objects;
using Krypton_Core.PacketReaderNew;
using Ping = Krypton_Core.Commands.Write.Ping;

namespace Krypton_Core.Managers
{
    public class PacketManager : Client
    {

        private DateTime LastPacketSent { get; set; }

        #region Events

        public event EventHandler<ShipExplode>? ShipExplode;

        public event EventHandler<RepairInfo>? ShipDestroyed;

        public event EventHandler<HeroInitialize>? HeroInit;

        public event EventHandler<GateInit>? GateInitialize;

        public event EventHandler<PlayersInit>? ShipInit;

        public event EventHandler<HeroUpdate>? HeroUpdated;

        public event EventHandler<PositionConfirm>? onPositionConfirm;

        public event EventHandler<BoxCollected>? onBoxCollected;

        public event EventHandler<BoxInit>? BoxInited;

        public event EventHandler<VersionVerify>? onVersionVerify;

        public event EventHandler<InitResources>? onInitResources;

        public event EventHandler<UpdateHpShd>? ShipUpdate;

        public event EventHandler<EventArgs>? LostConnection;



        public event EventHandler<RemoveShip>? onRemoveShip;

        public event EventHandler<ClanBaseOnMap>? Base;
        public event EventHandler<ReadResources>? onResRead;

        public event EventHandler<EventArgs>? Captcha;

        public event EventHandler<PetInitUp>? onPetInitUp;

        public event EventHandler<PlayerPosChange>? onShipMove;
        public event EventHandler<PetInit>? onPetInit;

        public event EventHandler<POInit>? onPoints;
        public event EventHandler<SpeedConfirm>? OnSpeedChange; 

        //Group
        public event EventHandler<GroupRequest>? GroupRequestInitied;

        public event EventHandler<GroupMembers>? GroupInicialize;
        public event EventHandler<RemovePlayerFromGroup>? RemovePlayerFromGroup;
        public event EventHandler<EventArgs>? onGroupDestroyed;
        public event EventHandler<GroupInfoChanged>? onChangeGroupInfo;

        //hero shipData
        public event EventHandler<HeroHp>? HeroHpUpdated;




    #endregion


        //public User _user { get; set; }


        Thread? _ping { get; set; }

        public EventManager PacketSorter { get; set; }
        public PetEvents PetEvents { get; set; }
        public  GroupEvents GroupEvents { get; set; }
        

        public PacketManager(Api api) : base(api)
        {
            PacketSorter = new EventManager(Api , Api.Tweener);
            PetEvents = new PetEvents(Api);
            GroupEvents = new(Api);

            Connect(Api._user.userData.MapIP, 5001);
            OnConnected += (s, e) =>
            {
                Task.Run(async () => await CheckConnection());
            };
            

            //Task.Run(async () => await CheckEvents());
        }

        //private async Task CheckEvents()
        //{
        //    while (true)
        //    {
        //        await Task.Delay(2000);

        //        if (LastPacketSent <= DateTime.Now)
        //        {
        //            Disconnected?.Invoke(this, EventArgs.Empty);
        //        }
        //    }
        //}
        //bool trashAvaible = true;




        public async Task CheckConnection()
        {
            while(RunningStream)
            {
                await Task.Delay(2000);
                if (LastPacketSent.AddSeconds(10) <= DateTime.Now)
                {
                    RunningStream = false;
                    Disconnected?.Invoke(this, EventArgs.Empty);
                }
                else
                    continue;
                 
            }
        }

        public override void Parse(EndianBinaryReader reader)
        {
            //id 49

            try
            {

                LastPacketSent = DateTime.Now;
                var cache = reader;
                var id = cache.ReadInt16();

                

                LastPacketSent = DateTime.Now.AddSeconds(20);
                switch (id)
                {
                    case HeroHp.ID:
                        var hero = new HeroHp(cache);
                        HeroHpUpdated?.Invoke(this ,hero);
                        break;
                    case CaptchaBox.ID:
                    {
                        var cap = new CaptchaBox(cache);
                        Api?.PushLog($"Got captcha");
                        break;
                        
                    }
                    case HeroInitialize.ID:
                        {
                            var heroInit = new HeroInitialize(cache);
                            HeroInit?.Invoke(this, heroInit);
                            if (_ping == null)
                            {
                                _ping = new Thread(PingLoop);
                                _ping.Start();

                                Task.Run(async () =>
                                {
                                    while (true)
                                    {
                                        await Task.Delay(200);
                                        Api._user.Ping = (int) PingTimeAverage(Api?._user.userData.MapIP, 4);
                                    }
                                });
                                
                                
                            }
                            break;
                        }
                    case ItemInfo.ID:
                        {
                            var Items = new ItemInfo(cache);
                            break;
                        }
                   
                    case UpdateShield.ID:
                        {
                            var upShd = new UpdateShield(cache);
                            if (upShd.Shield < 0) return;
                            Api._user.shipInfo.Shd = upShd.Shield;
                            Api._user.shipInfo.MaxShd = upShd.MaxShield;
                            break;
                        }
                    case GateInit.ID:
                        {
                            GateInit GInit = new GateInit(cache);
                            if (_ping == null)
                            {
                                _ping = new Thread(PingLoop);
                                _ping.Start();
                            }

                            GateInitialize?.Invoke(this, GInit);
                           
                            break;
                        }
                    case PetInitUp.ID:
                        {
                            onPetInitUp?.Invoke(this , new PetInitUp(cache));
                            break;
                        }

                    case ClanBaseOnMap.ID:
                        {
                            ClanBaseOnMap CBS = new ClanBaseOnMap(cache);
                            Base?.Invoke(this, CBS);
                            

                            break;

                        }
                    case PetInit.ID:
                        {
                            var pet = new PetInit(cache);
                            onPetInit?.Invoke(this, pet);
                            break;


                        }

                    case RemoveShip.ID:
                        {
                            try
                            {
                                var rm = new RemoveShip(cache);


                                onRemoveShip?.Invoke(this, rm);
                            }
                            catch (Exception ex)
                            {
                                Api.InvokeError(ex.ToString(), false, false);
                            }

                            break;
                        }
                    case PlayersInit.ID:
                        {
                            var playerInit = new PlayersInit(cache);
                            ShipInit?.Invoke(this, playerInit);

                            break;
                        }
                    case ReadResources.ID:
                        {
                            break;
                        }
                    case ResourceInfo.ID:
                        {
                            
                            ResourceInfo res = new(cache);
                            Api._user.shipInfo.CargoSpace = (int)res.total;
                            Api._user.statistics.PalladiumCount = (int)res.resources.Find(x => x.Type == 8).Count;

                            break;
                           
                        }
                    case 90:
                        PlayerPosChange ps = new(cache);
                        onShipMove?.Invoke(this, ps);
                        break;
                    case 252:
                        CrashLogManager.Send(Api?._user.logMsg, $"'{Api?._user.Name}' is destroyed", CrashLogManager.Type.MESSAGE);
                        var Repair = new RepairInfo(cache);
                        ShipDestroyed?.Invoke(this, Repair);
                        break;
                    case -13036:
                        CrashLogManager.Send(Api?._user.logMsg, $"'{Api?._user.Name}' lost connection.", CrashLogManager.Type.WARNING);
                        //LostConnection?.Invoke(this, EventArgs.Empty);
                        break;
                    case UpdateHpShd.ID:
                        var up = new UpdateHpShd(cache, Api);
                        ShipUpdate?.Invoke(this, up);

                        break;
                    case InitResources.ID:
                        {
                            var initRes = new InitResources(cache);
                            onInitResources?.Invoke(this, initRes);

                            break;

                        }
                    case VersionVerify.ID:
                        {
                            onVersionVerify?.Invoke(this, new VersionVerify(cache));
                            break;
                        }
                    case PositionConfirm.ID:
                        {
                            var pos = new PositionConfirm(cache , Api , Api.Tweener);
                            onPositionConfirm?.Invoke(this, pos);
                            break;
                        }
                    case 1:
                        var mess = new Commands.Read.Message(cache);
                        Console.WriteLine(mess.HpMessage);
                        Api?.Sr.Sort(mess.HpMessage);
                        
                        break;
                    case BoxInit.ID:
                        var init = new BoxInit(cache);
                        BoxInited?.Invoke(this, init);
                        break;
                    case BoxCollected.ID:
                        BoxCollected a = new BoxCollected(cache);
                        onBoxCollected?.Invoke(this, a);
                        break;
                    case HeroUpdate.ID:
                        var heroUp = new HeroUpdate(cache);
                        HeroUpdated?.Invoke(this, heroUp);

                        break;
                    case GroupInfoChanged.ID:
                    {
                        GroupInfoChanged info = new(cache);
                        onChangeGroupInfo?.Invoke(this , info);
                        break;
                    }
                    
                    case POInit.ID:
                        
                        var poi = new POInit(cache);
                        onPoints?.Invoke(this, poi);
                        
                        break;

                    case GroupRequest.ID:
                        
                        var grpRequest = new GroupRequest(cache);
                        GroupRequestInitied?.Invoke(this, grpRequest);
                        //var init = new BoxInit(cache);
                        //BoxInited?.Invoke(this, init);
                        break;
                    case GroupDestroyed.ID:
                    {
                        
                        onGroupDestroyed?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                  
                    case 192:
                    {
                        Console.WriteLine("packet id 192");
                        break;
                        
                    }
                    case 190:
                    {
                        Console.WriteLine("PAcket id 190");
                        break;
                    }
                    case -14028:
                    {
                        Console.WriteLine("Packet id -14028" );
                        break;
                    }
                    case SpeedConfirm.ID:
                    {
                        SpeedConfirm speed = new(cache);
                        OnSpeedChange?.Invoke(this , speed);
                        
                        break;
                        
                    }
                    case ShooterInfo.ID:
                    {
                        var shooter = new ShooterInfo(cache, Api);
                        
                        break;
                    }
                    case MineInit.ID:
                    {
                        MineInit mine = new MineInit(cache);
                        var Mine = new Mine(mine.Hash, mine.Type, mine.X, mine.Y);
                        Api._user.Mines.AddToMines(Mine);
                        break;
                    }
                    case RemoveMine.ID:
                    {
                        var remove = new RemoveMine(cache);
                        Api._user.Mines.RemoveFromMines(remove.Hash);
                        break;
                    }
                    case GroupMembers.ID:
                    {
                        GroupMembers gr = new(cache);
                        Api._user.players.AddGroupMembers(gr.UsersInGroup);
                        break;
                        
                    }
                    case Commands.Read.RemovePlayerFromGroup.ID:
                    {
                        var Remove = new RemovePlayerFromGroup(cache);
                        RemovePlayerFromGroup?.Invoke(this,Remove);
                        break;
                            
                    }
                    case Commands.Read.ShipExplode.ID:
                        {
                            ShipExplode?.Invoke(this, new Commands.Read.ShipExplode(cache));
                            Console.WriteLine("got packet id 30");
                            break;
                        }
                    //case BoosterInfo.ID:
                    //{
                    //    //BoosterInfo info = new BoosterInfo(cache);
                    //    //Api._user.Boosters.AddBoosters(info.boosters);
                    //    //Api._user.Boosters.AddValues(info.BoostersValue);
                    //    //break;
                        
                    //}


                    default:
                        
                        break;
                }
            }
            catch (Exception e)
            {
                //CrashLogManager.Send(Api?._user.logMsg, $"Error while parsing the TCP Stream !", CrashLogManager.Type.FATAL);
                //CrashLogManager.Send(Api?._user.logMsg, e.Message, CrashLogManager.Type.FATAL);
                //Console.WriteLine(e.Message);
                //Api.InvokeError(e.ToString(), false, false);
                Debug.WriteLine(e);
            }

        }

        private void PingLoop()
        {
            Random a = new Random();
            while (Api._user != null)
            {

                Thread.Sleep(5000);
                if (Api._user.userData.Ready)
                {
                    Send(new Ping());
                }
             
            }
        }

        public double PingTimeAverage(string host, int echoNum)
        {
            long totalTime = 0;
            int timeout = 120;
            System.Net.NetworkInformation.Ping pingSender = new();

            for (int i = 0; i < echoNum; i++)
            {
                PingReply reply = pingSender.Send(host, timeout);
                if (reply.Status == IPStatus.Success)
                {
                    totalTime += reply.RoundtripTime;
                }
            }
            return totalTime / echoNum;
        }
       


    }
}
