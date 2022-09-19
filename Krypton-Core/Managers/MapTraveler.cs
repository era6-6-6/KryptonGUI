using System.Reflection.Metadata.Ecma335;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
using Krypton_Core.Collections.Game.Maps;
using Krypton_Core.Collections.Game.Objects;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Write;
using Krypton_Core.Logic;
using Unglide;

namespace Krypton_Core.Managers
{
    public class MapTraveler : LogicInterface
    {
        public Api Api { get; set; }
        public bool Ready { get; set; } = false;

        public Graph<int, string> graph = new();
        private string? ErrorMessage { get; set; } = "";
        private enum CloseReason
        {
            NOGATE , DELAY
        }
        public MapTraveler(Api api, Tweener tweenn) : base(api, tweenn)
        {
            
            Api = api;
            RegisterEvents();
           
            for (int i = 1; i <= 29; i++)
            {
                //WE add all nodes to 29
                graph.AddNode(i);
            }

            graph.Connect(1, 2, 1, "");
            graph.Connect(2, 3, 1, "");
            graph.Connect(2, 1, 1, "");
            graph.Connect(2, 4, 1, "");
            graph.Connect(3, 2, 1, "");
            graph.Connect(3, 4, 1, "");
            graph.Connect(3, 7, 1, "");
            graph.Connect(4, 2, 1, "");
            graph.Connect(4, 12, 1, "");
            graph.Connect(4, 13, 1, "");
            graph.Connect(4, 3, 1, "");
            graph.Connect(5, 6, 1, "");
            graph.Connect(6, 7, 1, "");
            graph.Connect(6, 5, 1, "");
            graph.Connect(6, 8, 1, "");
            graph.Connect(7, 6, 1, "");
            graph.Connect(7, 8, 1, "");
            graph.Connect(7, 3, 1, "");
            graph.Connect(8, 6, 1, "");
            graph.Connect(8, 11, 1, "");
            graph.Connect(8, 14, 1, "");
            graph.Connect(8, 7, 1, "");
            graph.Connect(9, 10, 1, "");
            graph.Connect(10, 11, 1, "");
            graph.Connect(10, 9, 1, "");
            graph.Connect(10, 12, 1, "");
            graph.Connect(11, 10, 1, "");
            graph.Connect(11, 12, 1, "");
            graph.Connect(11, 8, 1, "");
            graph.Connect(12, 10, 1, "");
            graph.Connect(12, 11, 1, "");
            graph.Connect(12, 15, 1, "");
            graph.Connect(12, 4, 1, "");
            graph.Connect(13, 4, 1, "");
            graph.Connect(13, 15, 1, "");
            graph.Connect(13, 14, 1, "");
            graph.Connect(13, 16, 1, "");
            graph.Connect(14, 8, 1, "");
            graph.Connect(14, 13, 1, "");
            graph.Connect(14, 15, 1, "");
            graph.Connect(14, 16, 1, "");
            graph.Connect(15, 12, 1, "");
            graph.Connect(15, 14, 1, "");
            graph.Connect(15, 13, 1, "");
            graph.Connect(15, 16, 1, "");

            graph.Connect(16, 25, 1, "");
            graph.Connect(16, 17, 1, "");
            graph.Connect(16, 21, 1, "");
            graph.Connect(16, 13, 1, "");
            graph.Connect(16, 14, 1, "");
            graph.Connect(16, 15, 1, "");

            graph.Connect(17, 16, 1, "");
            graph.Connect(17, 29, 1, "");
            graph.Connect(17, 19, 1, "");
            graph.Connect(17, 18, 1, "");
            graph.Connect(18, 17, 1, "");
            graph.Connect(18, 20, 1, "");
            graph.Connect(19, 17, 1, "");
            graph.Connect(19, 20, 1, "");
            graph.Connect(21, 16, 1, "");
            graph.Connect(21, 18, 1, "");
            graph.Connect(21, 29, 1, "");
            graph.Connect(21, 23, 1, "");
            graph.Connect(18, 21, 1, "");
            graph.Connect(18, 24, 1, "");
            graph.Connect(23, 21, 1, "");
            graph.Connect(23, 24, 1, "");
            graph.Connect(24, 23, 1, "");
            graph.Connect(224, 22, 1, "");

            graph.Connect(25, 16, 1, "");
            graph.Connect(25, 29, 1, "");
            graph.Connect(25, 26, 1, "");
            graph.Connect(25, 27, 1, "");
            graph.Connect(26, 25, 1, "");
            graph.Connect(26, 28, 1, "");
            graph.Connect(27, 25, 1, "");
            graph.Connect(27, 28, 1, "");

            graph.Connect(28, 27, 1,"");
            graph.Connect(28, 26, 1, "");
            graph.Connect(20, 18, 1, "");
            graph.Connect(20, 19, 1, "");

        }
        private void RegisterEvents()
        {
            Api.Sr.NoCloseGate += (s, e) =>
            {
                ErrorMessage = "NoClosePort";
                Console.WriteLine("no port fired");
            };
            Api.Sr.JumpNotReady += (s, e) =>
            {
                ErrorMessage = "JumpDelay";
                Console.WriteLine("jump delay");
            };
        }
        

        /// <summary>
        /// Travel from the current map to the nextmap id
        /// </summary>
        /// <param name="MapID">Current MapID</param>
        /// <param name="nextMapId">NextMapID</param>
        public async Task<bool> Travel(int nextMapId)
        {
            bool Jumped = false;
            bool Close = false;
            bool JumpSucces = false;
            var mapid = Api._user.userData.MapID;
            while (!Jumped || !Close)
            {
                if (mapid != Api._user.userData.MapID) return true;

                Portal? port = null;
                lock (Api._user.BasesPorts.gates)
                {
                    port = Api._user.BasesPorts.gates.Find(x => x.MapID == nextMapId);
                }

                if (port == null)
                {
                    await Task.Delay(1000);
                    continue;
                }

               
                while (Api._user.Position.X != port.X || Api._user.Position.Y != port.Y)
                {
                    FlyToCorndinates(port.X, port.Y);
                    await Task.Delay(1000);
                    Console.WriteLine($"Player X:{Api._user.Position.X} PlayerY: {Api._user.Position.Y} PortX: {port.X} PortY: {port.Y}");
                    if(Api._user.userData.MapID == nextMapId) return true;
                    
                }

                while (!JumpSucces)
                {
                    await Task.Delay(2000);
                    if (Api._user.social.PortReady)
                    {
                        SendPacket(new JumpPortal());
                    }
                    else
                    {
                        continue;
                    }

                    await Task.Delay(1000);

                    if (ErrorMessage == "NoClosePort")
                    {
                        FlyToCorndinates(port.X, port.Y);
                        await Task.Delay(1000);
                        ErrorMessage = "";
                        JumpSucces = false;
                        continue;

                    }
                    else if(ErrorMessage == "JumpDelay")
                    {
                        await Task.Delay(1000);
                        ErrorMessage = "";
                        JumpSucces = false;
                        continue;
                        
                    }
                    else
                    {
                        Api.CleanLists();
                        Api._user.userData.Ready = false;
                        await Api.ResetConnection(this);
                        JumpSucces = true;
                        Jumped = true;

                    }
                }

            }
            Api._user.userData.Ready = false;
            await Task.Delay(5000);
            
            return true;



        }
        public async void FlyWithoutMap(Portal Port)
        {
            if (Api._user.Travel == true) return;

            if (Api._user.userData.MapID == Api._user.setting.General.TargetMapID) return;

            Portal? port = Port;

            if (port == null)
            {
                //await Api.ResetConnection(this);
                return;
            }


            Console.WriteLine($"fly to coordonates : X:{port.X} - Y:{port.Y}");
            FlyToCorndinates(port.X, port.Y);
            int temp = 0;
            while ((Api._user.Position.ActualX != port.X && Api._user.Position.ActualY != port.Y) && temp < 5)
            {
                Console.WriteLine($"The user is not on the X / Y of the portal");
                Thread.Sleep(1000);
                if (CalculateDistance(Api._user.Position.X, Api._user.Position.Y, port.X, port.Y) < 1000)
                {
                    temp++;
                }

            }
            while (!Api._user.social.PortReady)
            {
                Console.WriteLine($"Api._user.social.PortReady : {Api._user.social.PortReady}");
                Thread.Sleep(1000);
            }

            Api._user.Travel = true;
            Api.CleanLists();
            await Api.Jump();
            Api._user.userData.Ready = false;
            Api._user.social.PortReady = false;

            Console.WriteLine($"! JUMP !");




            Thread.Sleep(1000);
            Api._user.Travel = false;
        }
        public void TravelToZeta()
        {
            Api._user.TravelAcross = $"Next map: {MapCollection.Maps.FirstOrDefault(x => x.Key == 71).Value} TargetMap: {MapCollection.Maps.FirstOrDefault(x => x.Key == Api._user.setting.General.TargetMapID).Value}";
            var port = Api._user.BasesPorts.gates.Find(x => x.ID == 150000372);
            FlyWithoutMap(port);
        }
        public async Task TravelerModule()
        {

            //Api._user.TravelAcross = $"Next map: {MapCollection.Maps.FirstOrDefault(x => x.Key == 2).Value} TargetMap: {MapCollection.Maps.FirstOrDefault(x => x.Key == Api._user.setting.TargetMapID).Value}";
            //await Travel(1, 2);
            Api.TravelMode = true;
            while (Api._user.userData.MapID != Api._user.setting.General.TargetMapID)
            {
                Console.WriteLine($"current map id : {Api._user.userData.MapID} | TargetMapID id : {Api._user.setting.General.TargetMapID}");

                ShortestPathResult result = graph.Dijkstra((uint)Api._user.userData.MapID, (uint)Api._user.setting.General.TargetMapID); //result contains the shortest path

                Console.WriteLine($"nextmap id : {result.GetPath().ToArray()[1]}");
                await Travel((int)result.GetPath().ToArray()[1]);
                
            }

            Api.TravelMode = false;
        }

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }
}