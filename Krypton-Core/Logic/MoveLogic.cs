using Krypton_Core.Collections.Game.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unglide;

namespace Krypton_Core.Logic
{
    public class MoveLogic : LogicInterface
    {
        public MoveLogic(Api api , Tweener tween) : base(api , tween)
        {

        }

        public void MoveStart()
        {
            while(true)
            {
                Thread.Sleep(200);
                if(!Api._user.Position.Moving)
                {
                    Api.PathFinder.FindPath(new Point(Api._user.Position.X , Api._user.Position.Y) , new Point(2000, 2000));


                    foreach (var point in Api.PathFinder.Points.ToList())
                    {
                        Console.WriteLine($"Next position X:{point.X}  Y:{point.Y}");
                        lock (Api.PathFinder.Points)
                        {
                           
                            FlyToCorndinates(point.X, point.Y);
                            Api.PathFinder.Points.Remove(point);
                        }

                        while (Api._user.Position.Moving)
                        {
                            
                            Thread.Sleep(50);
                        }
                        
                    }
                }
            }
        }

        

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }
}
