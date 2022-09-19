using Krypton_Core.Collections.Collectables;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Logic.Pala
{
    public class PaladiumLogic : LogicInterface
    {
        public bool Running { get; set; } = false;
        public PaladiumLogic(Api api) : base(api , api.Tweener)
        {

        }

        public async Task PalladiumStartLogic()
        {
            bool petInvoke = false;
            Box? box = null;
            
            while(Running)
            {
                await Task.Delay(50);
               
                while (!Api._user.userData.Ready)
                {
                    
                   
                    await Task.Delay(100);
                }
                if (Api._user.userData.MapID != 93)
                {
                    while (Api._user.BasesPorts.gates.Count <= 0)
                    {
                        await Task.Delay(100);
                    }
                    petInvoke = false;
                    Api._user.events.PallaRelog = true;
                    await ChangeHangar(4);
                    Api._user.events.PallaRelog = false;
                    await Task.Delay(2000);
                    continue;
                }
                if (Api._user.setting.PET.UsePet)
                {
                    if (!petInvoke)
                    {
                        SendPacket(new InvokePet());
                    }
                    await Task.Delay(200);
                    petInvoke = true;
                    SendPacket(new PetSelectModule((short)Api._user.setting.PET.PetModule));
                }

                

                if (!Api._user.Position.Moving)
                {
                    RandomMove(12000, 25000, 19000, 25000);

                    await Task.Delay(200);


                }
                await Task.Delay(100);

                box = ClosestBox();
                while (box != null)
                {
                    FlyToCorndinates(box.X, box.Y);
                    await Task.Delay(250);
                    if (Api._user.shipInfo.CargoSpace >= Api._user.shipInfo.MaxCargoSpace)
                    {
                        box = null;
                    }

                    if (box != null && Api._user.Position.X == box.X && Api._user.Position.Y == box.Y)
                    {
                       
                        Api._user.packetManager?.Send(new CollectRes(box.Hash));
                        await Task.Delay(750);
                        Api._user.Boxes.CloseBoxes.RemoveAll(x => x.Hash == box.Hash);
                        Console.WriteLine("palla Collected");
                        box = null;
                        box = ClosestBox();
                        
                    }

                }
                if (Api._user.shipInfo.CargoSpace >= Api._user.shipInfo.MaxCargoSpace)
                {
                    Api._user.events.PallaRelog = true;
                    while (Api._user.userData.MapID != 92)
                    {
                        await ChangeHangar(2);
                        Console.WriteLine("Failed to change hangar");
                        await Task.Delay(2000);
                    }
                    //TODO: SELL
                    await SellPalla();
                    
                    Api._user.events.PallaRelog = false;
                    continue;
                }
            }


        }
        

        private async Task ChangeHangar(int id)
        {
            SendPacket(new Logout());
            while(!Api._user.events.LoggedOff)
            {
                
                await Task.Delay(1000);
                
            }
            
            await Api.ChangeHangar(id);
           // await Api.ResetConnection(false);
            Api._user.events.LoggedOff = false;
            Console.WriteLine("Hangar changed");
            
        }
        private async Task SellPalla()
        {
            await Task.Delay(1000);
            var cout = Api._user.statistics.PalladiumCount % 15;
            var toSell = Api._user.statistics.PalladiumCount - cout;
            SendPacket(new SellPalla(toSell));
            await Task.Delay(1000);
        }

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }
}
