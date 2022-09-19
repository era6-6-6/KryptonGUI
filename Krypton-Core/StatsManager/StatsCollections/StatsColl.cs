using Krypton_Core.StatsManager.obj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.StatsManager.StatsCollections
{
    public class StatsColl
    {
        public List<NpcStats> Npcs = new();
        public List<BoxStats> Boxes = new();




        public void AddNpc(string username)
        {
            lock(Npcs)
            {
                if(Npcs.Find(x => x.Name == username) == null)
                {
                    Npcs.Add(new NpcStats(username));
                }
                else
                {
                    Npcs.Find(x => x.Name == username).Killed++;
                }
            }
        }
        public void AddBox(string Type)
        {
            lock(Boxes)
            {
                if(Boxes.Find(x  => x.Type == Type) == null)
                {
                    Boxes.Add(new BoxStats(Type));
                }
                else
                {
                    Boxes.Find(x => x.Type == Type).Collected++;
                }

            }
        }
    }
}
