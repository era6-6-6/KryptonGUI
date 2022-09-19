using AStar.Options;
using AStar;
using Krypton_Core.Collections.User;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using AStar.Heuristics;
using Krypton_Core.Collections.Game.Objects;

namespace Krypton_Core.Managers.PathFinding
{
    public class PathFinderManager
    {
        private Api? Api { get; set; }
        private Point MapSize { get; set; } = new(); // PathFinder coordinates
        public List<Point> Points { get; set; } // DarkOrbit coordinates
        public List<BarrierPath>? Barriers { get; set; } = new(); // DarkOrbit coordinates
        private short[,]? Tiles;


        public PathFinderManager(Api api)
        {
            Api = api;
        }

        private void GetSize()
        {
            var mapPoint = Collections.Game.Maps.MapSize.SizeOfTheMap(Api._user.userData.MapID);
            MapSize = ConvertToOurSize(new Point((int)mapPoint.X, (int)mapPoint.Y));
        }

        public void MakeBarriers(List<int> points , string name = null)
        {
            MakeBarriers(points[0] , points[1] , points[2] , points[3] , points[4] , points[5] , points[6] , points[7] , name);
        }
        

        private void MakeBarriers(int x1, int y1 , int x2 , int y2 , int x3 , int y3 , int x4, int y4 , string name=null)
        {
            var X = x1-100;
            var Y = y1-100;
            var width = Math.Abs(x1 - x3) + 300;
            var height = Math.Abs(y1 - y4) + 300;

            Rectangle barrier = new Rectangle();
            barrier.X = X ;
            barrier.Y = Y ;
            barrier.Width = width ;
            barrier.Height = height;
            lock (Barriers)
            {
                Barriers.Add(new BarrierPath(barrier , name));
                Console.WriteLine($"Barrier {barrier.X}|{barrier.Y} added");
            }
           
           
        }

        public Point To;
        
        public void FindPath(Point from, Point to)
        {
            To = to;


           
            //convetion
            from = ConvertToOurSize(from);
            to = ConvertToOurSize(to);
            Console.WriteLine(to);

            var pathfinderOptions = new PathFinderOptions
            {
                PunishChangeDirection = false,
                UseDiagonals = true,
                SearchLimit = 200000,
                
            };


            Console.WriteLine(pathfinderOptions.SearchLimit);
            var worldGrid = new WorldGrid(Tiles);
            var pathfinder = new PathFinder(worldGrid, pathfinderOptions);

            Points = pathfinder.FindPath(new System.Drawing.Point(from.X, from.Y), new System.Drawing.Point(to.X, to.Y))
                .ToList()
                .Select(ConvertToDarkorbit).ToList();



            Console.WriteLine("Path length: " + Points.Count);

        }

        
        public void CreateMap()
        {
            GetSize();
            lock (Barriers)
            {


                Tiles = new short[MapSize.X, MapSize.Y];
                for (int i = 0; i < MapSize.X; i++)
                {
                    for (int j = 0; j < MapSize.Y; j++)
                    {
                        Tiles[i, j] = 1;
                        foreach (var barrier in Barriers.ToList().Select(b =>
                                     new System.Drawing.Rectangle(
                                         ConvertToOurSize(new Point(b.Rec.X, b.Rec.Y)),
                                         ConvertPointToSize(ConvertToOurSize(new Point(b.Rec.Width, b.Rec.Height)))
                                     )
                                 ))
                        {
                            if (CheckIfSomethingIsInside(barrier, new Point(j, i)))
                            {
                                Tiles[i, j] = 0;
                            }
                            

                           
                        }

                    }

                }
               
            }
        }

        private Size ConvertPointToSize(Point p)
        {
            return new Size(p.X, p.Y);
        }


        private Point ConvertToOurSize(Point point)
        {
            return new Point(point.X / 100, point.Y / 65);
        }

        private Point ConvertToDarkorbit(Point point)
        {
            return new Point(point.X * 100, point.Y * 65);
        }

       

        private bool CheckIfSomethingIsInside(System.Drawing.Rectangle rec, System.Drawing.Point point)
        {
            return rec.Contains(point);
        }



    }
}
