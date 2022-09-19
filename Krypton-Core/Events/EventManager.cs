using Krypton_Core.Collections.Collectables;
using Krypton_Core.Collections.Game.Bases;
using Krypton_Core.Collections.Game.Objects;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Read;
using Krypton_Core.Commands.Write;
using Krypton_Core.InternalPacketsMethods;
using Krypton_Core.Managers;
using Krypton_Core.TimeManagers;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.Game.Npcs;
using Krypton_Core.Settings;
using Krypton_Core.Collections.User;
using Krypton_Core.Commands.Read.Group;
using Krypton_Core.Utils;
using System.Drawing;
using Krypton_Core.Logic;
using Unglide;

#pragma warning disable CS8602

namespace Krypton_Core.Events
{
    public class EventManager : LogicInterface
    {
        //pacekt handlers

        public bool First = true;
        Api apiCore { get; set; }
        private NpcCollection npcSet;
        MemorizedBoxManager Mem;


        public EventManager(Api apiCore , Tweener tween) : base(apiCore , tween)
        {
            npcSet = new NpcCollection();
            this.apiCore = apiCore;
            Mem = new(apiCore);
        }

        private bool first = true;
      
        public void RegisterEvents()
        {
            try
            {
                //onposition confirm
                apiCore._user.packetManager.onPositionConfirm += (s , e) => 
                {
                    OnPositionConfirm(e);
                };
                apiCore._user.packetManager.HeroHpUpdated += (s, e) =>
                {
                    UpdateHeroHp(e);
                };
                apiCore._user.packetManager.OnSpeedChange += (s, e) =>
                {
                    apiCore._user.shipInfo.Speed = e.Speed;
                    if (apiCore._user.userData.Ready == false)
                    {
                        apiCore._user.userData.Ready = true;
                    }
                    if (apiCore._user.BasesPorts.gates.Count > 0 && LoginSettings.FlyToSafeZoneAfterStart && first)
                    {
                        var nearestGate = apiCore._user.BasesPorts.RepairAt.OrderBy(box => CalculateDistance(new Point(box.X, box.Y))).FirstOrDefault();
                        FlyToCorndinates(nearestGate.X , nearestGate.Y);
                        first = false;

                    }
                    Task.Run(async () => await CheckPorts());
                };
                //HeroInit
                apiCore._user.packetManager.HeroInit += (s, e) =>
                {
                    HeroInit(e);
                };
                //gateInit
                apiCore._user.packetManager.GateInitialize += (s, e) =>
                {
                    GateInitialize(e);
                    
                };
                //baseinit
                apiCore._user.packetManager.Base += (s, e) =>
                {
                    BaseInitialize(e);


                };
                //ShipMove
                apiCore._user.packetManager.onShipMove += (s, e) =>
                {
                    Tween(e);
                };
                //BoxRemove
                apiCore._user.packetManager.onBoxCollected += (s, e) =>
                {
                    var box = apiCore._user.Boxes.CloseBoxes.Find(a => a.Hash == e.Hash);
                    
                    apiCore._user.Boxes.AddMemorizedBox(e.Hash);
                    apiCore._user.Boxes.Remove(e.Hash);
                    if (e.Hash == apiCore._user.Box.Hash)
                    {
                        apiCore._user.Box = null;
                    }

                    Mem.Add(box);
                    

                };
                //boxinit
                apiCore._user.packetManager.BoxInited += (s, e) =>
                {
                    BoxInitialize(e);
                    

                };
                //shipinit
                apiCore._user.packetManager.ShipInit += (s, e) =>
                {
                    ShipInitialize(e);

                };
                apiCore._user.packetManager.onRemoveShip += (s, e) =>
                {
                    RemoveShip(e);

                };
                apiCore._user.packetManager.Captcha += (s, e) =>
                {
                    //TODO: Captcha logic
                };
                //disconnected
                apiCore._user.packetManager.Disconnected += (s, e) =>
                {
                    Console.WriteLine("disconnection hitted");
                    Reconnect();
                };
                //lost connection
                apiCore._user.packetManager.LostConnection += (s, e) =>
                {
                    //TODO:recconect
                    Console.WriteLine("losst connection hitted");
                    Reconnect();
                };
                //repaor
                apiCore._user.packetManager.ShipDestroyed += (s, e) =>
                {
                    //TODO: repair
                    Repair(e);

                };
                apiCore._user.packetManager.ShipUpdate += (s, e) =>
                {
                    UpdateShip(e);
                };
                apiCore._user.packetManager.HeroUpdated += (s, e) =>
                {
                    UpdateHero(e);

                };
                apiCore._user.packetManager.onPetInit += (s, e) =>
                {
                    PetInit(e);

                };
                apiCore._user.packetManager.ShipExplode += (s, e) =>
                {
                    apiCore._user.players.Remove(e.UserID);
                };
                
                apiCore._user.packetManager.onPoints += (s, e) =>
                {
                    InitPoints(e);
                };
                apiCore._user.packetManager.onInitResources += (s, e) =>
                {
                    InitResources(e);
                };
                
            }
            catch (Exception ex)
            {
                //test
                Console.WriteLine(ex.ToString());
            }



        }

        private void UpdateHeroHp(HeroHp e)
        {
            apiCore._user.shipInfo.Hp = e.Hitpoints;
            apiCore._user.shipInfo.MaxHp = e.MaxHp;
            apiCore._user.shipInfo.NanoHp = e.Nano;
            apiCore._user.shipInfo.MaxNanoHp = e.NanoMax;
        }

        private void OnPositionConfirm(PositionConfirm e)
        {
            apiCore._user.Position.ActualX = e.ActualX;
            apiCore._user.Position.ActualY = e.ActualY;
        }

        private void InitResources(InitResources e)
        {
            Box box = new Box();
            if(e.Type == 8)
            {
                box.type = "ore_8";
            }
            else
            {
                return;
            }
            box.X = e.X;
            box.Y = e.Y;
            box.Hash = e.Hash;
            apiCore._user.Boxes.Add(box);
        }

        private void InitPoints(POInit e)
        {
            Krypton_Core.Collections.User.Barrier bar = new Krypton_Core.Collections.User.Barrier();
            bar.points = e.points;
            bar.Type = e.type;
            bar.Name = e.Name;
            apiCore._user.Barriers.Add(bar);
            apiCore.PathFinder.MakeBarriers(bar.points , bar.Type);

        }

       

        private void UpdateHero(HeroUpdate e)
        {
            
            if(e.UserID  == apiCore._user.Target.ID)
            {
                apiCore._user.Target.Hp = e.Hitpoints;
                apiCore._user.Target.Shd = e.Shield;
                apiCore._user.Target.isTaken = e.Taken;
                apiCore._user.Target.MaxShd = e.MaxShd;
                apiCore._user.Target.MaxHP = e.MaxHp;
            }
            else
            {
                try
                {
                    lock (apiCore._user.players.PlayersInGroup)
                    {
                        Player? player = apiCore._user.players.PlayersInGroup.Find(x => x.Target.ID == e.UserID);
                        if (player != null)
                        {
                            player.Target.Hp = e.Hitpoints;
                            player.Target.Shd = e.Shield;
                            player.Target.MaxShd = e.MaxShd;
                            player.Target.MaxHP = e.MaxHp;
                        }
                    }
                }
                catch (Exception exception)
                {
                    apiCore.InvokeError(exception.ToString(), false, false);
                    throw;
                }
            }
        }

        private void PetInit(PetInit e)
        {
            Pet pet = new Pet();
            pet.Id = e.userID;
            pet.Username = e.Username;
            pet.Level = e.Level;
            pet.factionID = e.FactionID;
            pet.X = e.X;
            pet.Y = e.Y;
            apiCore._user.players.AddPet(pet);
        }

        private async void Repair(RepairInfo e)
        {
            apiCore._user.Destroyed = true;
            await apiCore.RemoveFromTween();
            SendPacket(e.RepairOnPlaceAvaible ? new Repair(apiCore._user.setting.General.RepairAtPlace, apiCore._user) : new Repair(1, apiCore._user));
            apiCore.PushLog("Reviving ship...");
            apiCore._user.userData.Ready = false;
            apiCore._user.statistics.TotalDeaths++;
            await apiCore.ResetConnection(this);
        }
        private async void Reconnect()
        {
            if (apiCore._user.LogoutInvoked == true) return;
            if(apiCore._user == null || apiCore == null) return;
            if (apiCore._user.Travel) return;
            await apiCore.RemoveFromTween();
            apiCore._user.social.PortReady = false;
            apiCore.PushLog("Can't use ports...");
            await apiCore.ResetConnection(this);

            await Task.Run(async () =>
            {
                await Task.Delay(15000);
                apiCore.PushLog("Able to use ports again...");
                apiCore._user.social.PortReady = true;
            });

        }

        private void UpdateShip(UpdateHpShd e)
        {

            var user = apiCore._user.shipInfo;
            if (e.UserID == apiCore._user.userData.UserID)
            {
                user.Hp = e.HP;
                user.Shd = e.Shd;
               
                return;
            }
            if (apiCore._user.Target == null) return;
            if (e.UserID != apiCore._user.Target.ID) return;
            apiCore._user.Target.Hp = e.HP;
            apiCore._user.Target.Shd = e.Shd;
            
            

        }

        private void RemoveShip(RemoveShip e)
        {
            apiCore._user.players.Remove(e.UserID);
            if (apiCore._user.Target == null) return;
            if (e.UserID == apiCore._user.Target.ID)
            {
                apiCore._user.Target = null;
            }


        }

        private void Tween(PlayerPosChange e)
        {
            if(apiCore._user.players.Pet != null)
            {
                if(e.UserID == apiCore._user.players.Pet.Id)
                {
                    apiCore.Tweener.Tween(apiCore._user.players.Pet, new { X = e.X, Y = e.Y }, e.Duration);
                }
            }
            
            Player? player = apiCore._user.players.AllPlayers.Find(x => x.ID == e.UserID);
            if (player == null && apiCore._user.Target != null)
            {

                if(e.UserID == apiCore._user.Target.ID)
                {
                    apiCore.Tweener.Tween(apiCore._user.Target, new { X = e.X, Y = e.Y }, e.Duration);
                }
                else
                {
                    return;
                }
            }
            if(player == null) return;
            apiCore.Tweener.Tween(player, new { X = e.X, Y = e.Y }, e.Duration);
        }

        private void ShipInitialize(PlayersInit e)
        {
            Player player = new()
            {
                ID = e.UserID,

                Username = e.Username,
                X = e.X,
                Y = e.Y,
                isNpcs = e.IsNpC,
                FactionID = e.FactionID
            };
            
            try
            {
                if (player.isNpcs)
                {
                    player.TempId = NpcCollection.Npcs.FirstOrDefault(x => x.Item2 == player.Username).Item3;
                    player.Settings = apiCore.npcSet.NpcsSettings.Find(x => x.Item1 == player.TempId).Item2;
                }

              
                    


                if (apiCore._user.setting.General.NpcsToKill.Contains(player.Username))
                {
                    apiCore._user.players.Add(player, apiCore._user.userData.FactionID, true);
                }
                else
                {
                    apiCore._user.players.Add(player, apiCore._user.userData.FactionID, false);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                apiCore._user.players.Add(player, apiCore._user.userData.FactionID, false);
            }
            

        }

        private void BoxInitialize(BoxInit e)
        {
            var box = new Box
            {
                X = e.X,
                Y = e.Y,
                type = e.Type,
                Hash = e.Hash
            };
            apiCore._user.Boxes.Add(box);
            apiCore._user.Boxes.ConvertMemBoxToClose(box.Hash);
            Mem.TryToRemoveBeforeTick(box);            
        }

        private void BaseInitialize(ClanBaseOnMap e)
        {
            Base BaseOnMap = new Base();
            BaseOnMap.OwnerClan = e.Owner;
            BaseOnMap.Name = e.Name;
            BaseOnMap.Id = e.BaseID;
            BaseOnMap.FactionId = e.FactionID;
            BaseOnMap.X = e.X;
            BaseOnMap.Y = e.Y;
            BaseOnMap.bool1 = e.bool1;
            BaseOnMap.bool2 = e.bool2;
            BaseOnMap.AssetId = e.AssetId;
            BaseOnMap.bool3 = e.bool3;
            BaseOnMap.bool4 = e.bool4;
            BaseOnMap.bool5 = e.bool5;

            lock (apiCore._user.BasesPorts.Bases)
            {
                apiCore._user.BasesPorts.Bases.Add(BaseOnMap);
            }

        }

        private void GateInitialize(GateInit e)
        {
            Portal port = new Portal();
            port.X = e.X;
            port.ID = e.GateID;
            port.Y = e.Y;
            port.TypeID = e.TypeID;

            apiCore._user.BasesPorts.AddPorta(port);
            

        }

        private void HeroInit(HeroInitialize data)
        {
           
                SendPacket(new InitPacket(0));
                SendPacket(new InitPacket(2));
          

            var user = apiCore._user.shipInfo;
            var userData = apiCore._user.userData;
            var stats = apiCore._user.statistics;
            var position = apiCore._user.Position;
            var social = apiCore._user.social;

            //user
            user.MaxHp = data.MaxHP;
            user.Hp = data.HP;
            user.MaxShd = (int)data.MaxShield;
            user.Shd = data.Shield;
            user.CargoSpace = data.Unknown1;
            user.MaxCargoSpace = data.CargoCapacity;
            user.ShipName = data.Shipname;
            
           

            //stats
            stats.Uridium = data.Uridium;
            stats.Credits = data.Credits;
            stats.Experience = data.XP;
            stats.Premium = data.Premium;
            stats.Level = data.Level;
            stats.Honor = data.Honor;
            stats.Cloacked = data.Cloaked;
            stats.RankId = (int)data.Rank;
            user.Speed = (int)(data.Speed);


            userData.MapID = data.Map;
            userData.MapName = Krypton_Core.Collections.Game.Maps.MapCollection.Maps.FirstOrDefault(x => x.Key == userData.MapID).Value;
            userData.FactionID = (short)data.FactionID;
            userData.Username = data.Username;
            


            //position
            lock (Api._user.Position)
            {
                //TODO:!!!!!!!!
                apiCore._user.Position.X = data.X;
                apiCore._user.Position.Y = data.Y;

            }


           

            //userdata
            
            userData.MapID = data.Map;

            //social info (map traveler)
            social.ClanTag = data.ClanTag;
           
            Task.Run(async () =>
            {
                await Task.Delay(15000);
                social.PortReady = true;
            });
            
            Task.Run(async () => await CheckPorts());

            
            apiCore._user.Destroyed = false;
          
            //Task.Run(async () => await CheckPorts());





        }

        private void SendPacket(Command com)
        {
            apiCore._user.packetManager.Send(com);
        }

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }

        private async Task CheckPorts()
        {
            if (!apiCore._user.events.ReloadComplete)
            {
                return;
            }
            apiCore._user.events.ReloadComplete = false;
            await Task.Delay(4000);
            //&& !(apiCore._user.userData.MapID != 71 || apiCore._user.userData.MapID != 72 || apiCore._user.userData.MapID != 73)
            if (apiCore._user.BasesPorts.gates.Count == 0 && apiCore._user.userData.MapID < 100)
            {
                First = true;
                await apiCore.ResetConnection(this);
            }
            else
            {
              
                apiCore.PathFinder.CreateMap();
            }
            apiCore._user.events.ReloadComplete = true;
        }





    }
}
