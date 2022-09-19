using Krypton_Core.Collections.Game.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class Barriers
    {

        public List<Barrier> Points = new List<Barrier>();

        //rewrite it to rectangle later
        public void CreateBarriers()
        {
            
        }
        public void Add(Barrier point)
        {
            lock (Points)
            {
                Points.Add(point);
            }
        }
    }
    public class Barrier
    {
        public string? Type { get; set; }
        public string? Name { get; set; }
        public short Shape { get; set; }
        public List<int> points = new List<int>();
        
    }
}
