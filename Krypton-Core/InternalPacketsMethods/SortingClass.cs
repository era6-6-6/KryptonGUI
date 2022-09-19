using System.Security.Cryptography;
using Krypton_Core.Collections.Game.Ammo;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Write;
using Krypton_Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.User;
using Action = Krypton_Core.Commands.Write.Action;

namespace Krypton_Core.InternalPacketsMethods
{
    public class SortingClass 
    {
        private Api Api { get; set; }
        public int AttackerID;
        public event EventHandler<Player> onShipAttack;
        public event EventHandler<EventArgs>? onServerRestart;
        public event EventHandler<EventArgs>? JumpNotReady;
        public event EventHandler<EventArgs>? NoCloseGate;
        public SortingClass(Api api)
        {
            Api = api;
        }
        public async void Sort(string message)
        {
            if (message.Contains("URI"))
            {
                string[] rew = message.Split('|');
                Api._user.statistics.CollectedUridium += double.Parse(rew[4]);
                Api._user.statistics.Uridium += double.Parse(rew[4]);
                Api.PushLog($"You received {double.Parse(rew[4])} Uridium.");


            }
            else if(message.Contains("0|i|"))
            {
                var rew = message.Split('|');

                int a = 0;
                var id = int.TryParse(rew[2] , out a);
                if(a != 0)
                {
                    Api._user.userData.MapID = a;
                }
                else
                {
                    await Api.GetMapID();
                }
            }
            else if(message.Contains("server_restart_n_seconds"))
            {
                var mess = message.Split("|");
                var number = int.Parse(mess[4]);
                if(number <= 60)
                {
                    onServerRestart?.Invoke(this, EventArgs.Empty);
                    return;
                }
                    
            }
            else if (message.Contains("msg_pet_out_of_fuel"))
            {
                Api._user.players.Pet = null;
                if (Api._user.setting.PET.BuyFuel)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Api._user.packetManager?.Send(new BuyPetFuel());
                        await Task.Delay(1000);
                    }
                }
            }
            else if (message.Contains("HON"))
            {
                string[] rew = message.Split('|');
                Api._user.statistics.CollectedHonor += double.Parse(rew[4]);
                Api._user.statistics.Honor += double.Parse(rew[4]);
                Api.PushLog($"You received {double.Parse(rew[4])} Honor.");
            }
            //"0|S|CFG|1"
            else if (message.Contains("0|S|CFG|"))
            {
                string[] rew = message.Split('|');
                Api._user.shipInfo.Config = uint.Parse(rew[3]);
               
            }
            else if (message.Contains("EP"))
            {
                string[] rew = message.Split('|');
                Api._user.statistics.CollectedXP += double.Parse(rew[4]);
                Api._user.statistics.Experience += double.Parse(rew[4]);
                Api.PushLog($"You received {double.Parse(rew[4])} Experience.");
            }
            else if (message.Contains("CRE"))
            {
                string[] rew = message.Split('|');
                Api._user.statistics.CollectedCredits += double.Parse(rew[4]);
                Api._user.statistics.Credits += double.Parse(rew[4]);
                Api.PushLog($"You received {double.Parse(rew[4])} Credits.");
            }
            //TODO: Fix async reader
            else if (message.Contains("HL"))
            {
                string[] rew = message.Split('|');
                Api._user.shipInfo.Hp = int.Parse(rew[6]);
            }
            else if (message.Contains("XEN"))
            {
                string[] rew = message.Split('|');
                Api._user.statistics.CollectedEE += int.Parse(rew[4]);
            }
            //0|n|LSH|150202308|172444810
            //else if (message.Contains("LSH"))
            //{
            //    string[] rew = message.Split("|");
            //    if (int.Parse(rew[4]) == Api._user.userData.UserID)
            //    {
            //       // Api._user.events.Shooting = true;
            //    }
            //}
            else if (message.Contains("LSH"))
            {
                //string[] rew = message.Split('|');
                //var id = int.Parse(rew[4]);
                //var userid = int.Parse(rew[3]);
                //Console.WriteLine("ID:" + id);
                //if (id == 0) return;
                //if(id == Api._user.setting.FollowUserID) return;
                //else
                //{
                //    Player? Target = Api._user.players.AllPlayers.Find(x => x.ID == id);
                //    if (Target == null)
                //    {
                //        return;
                //    }
                //    else if(userid == Api._user.setting.FollowUserID)
                //    {
                //        onShipAttack?.Invoke(this, Target);
                //    }
                    
                //}
               
            }
            else if(message.Contains("0|l"))
            {
                Api.PushLog("Logged off");
                Api._user.events.LoggedOff = true;
            }
            else if(message.Contains($"SLA|0|{Api._user.userData.UserID}"))
            {
                
            }
            else if(message.Contains("MIN"))
            {
                Console.WriteLine("Got mine packet");
                return;
            }
            else if (message.Contains("jump15"))
            {
                JumpNotReady?.Invoke(this , EventArgs.Empty);
            }
            else if (message.Contains("jumpgate_failed_no_gate"))
            {
                NoCloseGate?.Invoke(this , EventArgs.Empty);
            }
          
            else if(message.Contains("EMP"))
            {
                var mesg = message.Split('|');
                if(Api._user.Target != null)
                {
                    lock(Api._user.Target)
                    {
                        if(Api._user.Target.ID == int.Parse(mesg[3]))
                        {
                            Api._user.Target = null;
                        }
                    }
                }
            }
            else if (message.Contains("captcha_"))
            {
                Console.WriteLine("hitted by captcha!");
                Api.PushLog($"Got captcha");
            }
        }




    }
}
