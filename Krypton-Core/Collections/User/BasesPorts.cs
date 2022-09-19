using Krypton_Core.Collections.Game.Bases;
using Krypton_Core.Collections.Game.Objects;
using Krypton_Core.Collections.Game.Gates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.Game.Maps;
using System.Collections.Concurrent;

namespace Krypton_Core.Collections.User
{
    public class BasesPorts
    {
        public List<Portal>? gates { get; set; } = new();
        public List<Portal>? RepairAt { get; set; } = new();

        public SynchronizedCollection<Base>? Bases { get; set; } = new();


        public void Clear()
        {
            
            lock (gates)
            {
                gates.Clear();
            }
            lock (RepairAt)
            {
                RepairAt.Clear();
            }
            lock (Bases)
            {
                Bases.Clear();
            }
            
        }
        public void AddPorta(Portal p)
        { 
            p.MapID = PortsIds.GetNextMapID(p.ID);
            lock (gates)
            {
                gates?.Add(p);
            }
            if(p.TypeID == 1)
            {
                lock (RepairAt)
                {
                    RepairAt?.Add(p);
                }
            }


        }
    }
}
