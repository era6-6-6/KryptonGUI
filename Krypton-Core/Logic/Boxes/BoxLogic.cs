using Krypton_Core.Managers;
using Krypton_Core.Collections.Collectables;
using Krypton_Core.Commands.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unglide;
using Krypton_Core.Collections.Game.Players;

namespace Krypton_Core.Logic.Boxes
{
    public class BoxLogic : LogicInterface
    {
        MapTraveler mapTraveler;
        public bool Running { get; set; } = false;
        public BoxLogic(Api api, Tweener tween) : base(api, tween)
        {
            mapTraveler = new MapTraveler(Api, tween);
        }

        public async Task BoxStartMethod()
        {
            Api._user.Position.Moving = false;
            Box? box = null;
             mapTraveler.TravelerModule();
            SendPacket(new InvokePet());
            while (Running)
            {
                while (!Api._user.userData.Ready)
                {
                    Console.WriteLine("Account not ready yet");
                    await Task.Delay(100);
                }
                
                if (!Api._user.Position.Moving)
                {
                    var pos = Krypton_Core.Collections.Game.Maps.MapSize.SizeOfTheMap(Api._user.userData.MapID);
                    RandomMove((int)pos.X , (int)pos.Y);

                    await Task.Delay(200);
                    

                }
                await Task.Delay(100);

                box = ClosestBox();
                while (box != null)
                {
                    FlyToCorndinates(box.X, box.Y);
                    await Task.Delay(250);
                    if (Api._user.Position.X == box.X && Api._user.Position.Y == box.Y)
                    {
                        //Api._user.packetManager?.Send(new CollectBox(box.Hash, box.X, box.Y, box.X, box.Y));
                        Api._user.packetManager?.Send(new CollectBox(box.Hash , Api._user.Position.X , Api._user.Position.Y , box.X , box.Y));
                        await Task.Delay(750);
                        Api._user.Boxes.CloseBoxes.RemoveAll(x => x.Hash == box.Hash);

                        box = null;
                        box = ClosestBox();
                    }

                }
            }
            var neareastGates = ClosestGate();
            FlyToCorndinates(neareastGates.X, neareastGates.Y);
        }

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }
}
