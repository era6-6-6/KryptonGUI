using Krypton_Core.Collections.Collectables;
using Krypton_Core.Collections.Game.Maps;
using Krypton_Core.Collections.Game.Objects;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Commands.Write;
using Krypton_Core.Managers;
using Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unglide;
using Action = Krypton_Core.Commands.Write.Action;

namespace Krypton_Core.Logic
{
    public abstract class LogicInterface
    {
        public Api Api { get; set; }
        public Tweener tweener { get; set; }
        public bool Running { get; set; }

        public LogicInterface(Api api, Tweener tween)
        {
            Api = api;
            tweener = tween;
        }

        #region closes and calculation
        protected double CalculateDistance(int x, int y)
        {
            return CalculateDistance(Api._user.Position.X, Api._user.Position.Y, x, y);
        }
        protected double CalculateDistance(Point point)
        {
            return CalculateDistance(point.X, point.Y);
        }

        protected bool IsAnyNpcAround()
        {
            if(Api._user.players.NpcsToShoot.Count > 0)
            {
                return true;
            }
            return false;
        }

        protected bool IsAnyBoxAround()
        {
            lock (Api._user.Boxes.CloseBoxes)
            {
                return Api._user.Boxes.CloseBoxes.Count != 0;
            }
        }

        public Box? ClosestBox()
        {
            Box? nearestBox;
            nearestBox = Api._user.Boxes.CloseBoxes.ToList().MinBy(box => CalculateDistance(new Point(box.X, box.Y)));
            if (nearestBox != null)
            {
                if (!Api._user.setting.General.BoxesToCollect.Contains(nearestBox.type))
                {
                    return null;
                }
            }
            return nearestBox;
        }
        public Portal ClosestGate()
        {
            var nearestGate = Api._user.BasesPorts.RepairAt.OrderBy(box => CalculateDistance(new Point(box.X, box.Y))).FirstOrDefault();
            return nearestGate;
        }


        protected Player? ClosestNpc()
        {
            Player? npc = null;
            npc = Api._user.players.NpcsToShoot.ToList().OrderBy(npc => CalculateDistance(new Point(npc.X, npc.Y))).FirstOrDefault();
           
            if (npc != null)
            {
                if (npc.isTaken)
                {
                    lock (Api._user.players.NpcsToShoot)
                    {
                        Api._user.players.NpcsToShoot.RemoveAll(x => x.isTaken == true);
                    }

                    return null;
                }
                var tempNpc = Collections.Game.Npcs.NpcCollection.Npcs.Find(x => x.Item2 == npc.Username);
                if (!Api._user.setting.General.NpcsToKill.Contains(tempNpc.Item2.ToString()))
                {
                    //Api._user.players.Remove(npc.ID);
                    return null;
                }
            }
           

            return npc;
        }

#endregion


        /// <summary>
        /// Calculate distance between points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public double CalculateDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }




        #region Methods with packets

        protected void ChangeFormation(string formation)
        {
            SendPacket(new Action(formation , 0 , 1));
            Api.PushLog($"Formation changed to: {formation}");
        }
        protected bool CheckHp()
        {
            if (Api._user.shipInfo.Hp / Api._user.shipInfo.MaxHp * 100 <= Api._user.setting.General.RepairAt)
            {

                return true;
            }
            else
            {
                Console.WriteLine((Api._user.shipInfo.Hp / Api._user.shipInfo.MaxHp * 100));
                return false;
            }
        }

        
        public void RepairRep()
        {
            var port = ClosestGate();
            {
                FlyToCorndinates(port.X, port.Y);
            }
            
            SendPacket(new Action("equipment_extra_repbot_rep", 1, 1));
            
        }
        public void ReleasePet()
        {
            SendPacket(new InvokePet());
            Api.PushLog("Pet invoked");
        }
        public void PetModule(short module)
        {
            SendPacket(new PetSelectModule(module));
        }
        protected void SelectEnemy(Player npc)
        {
            SendPacket(new SelectEnemy(npc.ID, npc.X, npc.Y, Api._user.Position.X, Api._user.Position.Y));
        }

        #endregion




        /// <summary>
        /// Calculate distance between player and object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// 
        #region Travel
        protected async Task Travel()
        {
            MapTraveler mapTraveler = new MapTraveler(Api , tweener);
            
            await mapTraveler.TravelerModule();
        }

        public bool CheckMap()
        {
            if(Api._user.userData.MapID != Api._user.setting.General.TargetMapID)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion


        #region Fly methods

      
        private void FlyWithAnimation(int x, int y)
        {
            Api._user.Position.TargetX = x;
                Api._user.Position.TargetY = y;

                Api._user.packetManager?.Send(new Move((uint)Api._user.Position.TargetX, (uint)Api._user.Position.TargetY, (uint)Api._user.Position.X, (uint)Api._user.Position.Y));
                Api._user.Position.Moving = true;


                double distance = Math.Sqrt(Math.Pow((Api._user.Position.TargetX - Api._user.Position.X), 2) + Math.Pow((Api._user.Position.TargetY - Api._user.Position.Y), 2));
                double duration = (distance / Api._user.shipInfo.Speed);
                float durationMs = (float)duration * 1000;
                
                Api.Tweener.TargetCancel(Api._user.Position);
                Api.Tweener.Tween(Api._user.Position, new { X = Api._user.Position.TargetX, Y = Api._user.Position.TargetY }, durationMs)
                    .OnComplete(new System.Action(() => Api._user.Position.Moving = false));

        }
        public void FlyToNpc(Player npc)
        {
            FlyWithAnimation(npc.X, npc.Y);
        }
        public void FlyToCorndinates(int x, int y)
        {
            FlyWithAnimation(x, y);
        }

        public void RandomMove(int MapX, int MapY)
        {
            Random rnd = new Random();
            FlyToCorndinates(rnd.Next(0, MapX), rnd.Next(0, MapY));
        }
        public void RandomMove(int minX , int maxX , int minY , int maxY)
        {
            Random rnd = new Random();
            FlyToCorndinates(rnd.Next(minX,maxX), rnd.Next(minY, maxX));
        }

        #endregion




        #region Drag and Circle

        protected PointF CircleAround(float radius, float AngleInDegrees, PointF origin)
        {
            float x = (float)(radius * Math.Cos(AngleInDegrees * Math.PI / 180F)) + origin.X;
            float y = (float)(radius * Math.Sin(AngleInDegrees * Math.PI / 180f)) + origin.Y;
            return new PointF(x, y);
        }

        
        
        float angle = 0f; // angle of my own ship

        private float CalculateAngle(float x1 , float x2 , float y1 , float y2)
        {
            float xDiff = x2 - x1;
            float yDiff = y2 - y1;
            return (float)((float)Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
        }
        public void AssignAngle()
        {
            if (Api._user.Target == null) return;
            angle = CalculateAngle(Api._user.Target.X, Api._user.Position.X, Api._user.Target.Y, Api._user.Position.Y);
           if(angle < 0)
            {
                angle *= -1;
            }
            
            Console.WriteLine("angle:" + angle);
        }

        public void Circle(Player npc)
        {
            if (npc == null) return;
            var pos = CircleAround(npc.Settings.Radius, angle, new PointF(npc.X, npc.Y));
            if (angle < 360)
            {
                angle += 12f;
            }
            else
            {
                angle = 0;
            }

            FlyToCorndinates((int)pos.X, (int)pos.Y);

        }
        public void Circle(int x, int y)
        {
            var pos = CircleAround(5000, angle, new PointF(x, y));
            if (angle < 360)
            {
                angle += 6.5f;
            }
            else
            {
                angle = 0;
            }
            FlyToCorndinates((int)pos.X, (int)pos.Y);
        }
        private bool TopRadiation = false;
        private bool DownRadiation = true;
        private bool LeftRadiation = true;
        private bool RightRad = false;

        public void Drag(Player npc)
        {
            var currentMap = MapSize.SizeOfTheMap(Api._user.userData.MapID);
            if (Api._user.Position.X < 500)
            {
                LeftRadiation = true;
                RightRad = false;
            }
            if (Api._user.Position.X > currentMap.X - 500)
            {
                RightRad = true;
                LeftRadiation = false;
            }
            if (Api._user.Position.Y < 500)
            {
                TopRadiation = true;
                DownRadiation = false;

            }
            if (Api._user.Position.Y > currentMap.Y - 500)
            {
                DownRadiation = true;
                TopRadiation = false;
            }
            if (npc.Hp < (npc.Hp / npc.MaxHP * 22))
            {
                FlyToCorndinates(npc.X + 100, npc.Y);
            }
            else if (TopRadiation && LeftRadiation)
            {
                FlyToCorndinates(npc.X + 380, npc.Y + 380);
            }
            else if (TopRadiation && RightRad)
            {
                FlyToCorndinates(npc.X - 380, npc.Y + 380);
            }
            else if (DownRadiation && LeftRadiation)
            {
                FlyToCorndinates(npc.X + 380, npc.Y - 380);

            }
            else if (DownRadiation && RightRad)
            {
                FlyToCorndinates(npc.X - 380, npc.Y + 380);
            }

        }

        #endregion

        public void SendPacket(Command command)
        {
            Api._user.packetManager?.Send(command);
        }

        public void AttackNpc(Player npc)
        {
            Api._user.packetManager?.Send(new Attack(npc.ID, Api._user.Position.X, Api._user.Position.Y));
            
        }

        protected void ChangeAmmo(string ammo)
        {
            SendPacket(new Action(ammo , 0 , 0));
        }

        protected void Log(string message)
        {
            CrashLogManager.Send(Api._user.logMsg, message, CrashLogManager.Type.MESSAGE);
        }

        protected async Task Delay(int ms)
        {
            await Task.Delay(ms);
        }



        #region Override methods
        protected abstract Player ClosestNpcReturn();
        #endregion



    }

}
