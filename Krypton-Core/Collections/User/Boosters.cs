using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.Game.Items;

namespace Krypton_Core.Collections.User
{
    public class Boosters
    {
        public List<Booster> BoostersOwned = new();
        public List<BoosterValue> BoostersValue = new();


        public void AddBoosters(List<Booster> boosters)
        {
            lock (BoostersOwned)
            {
                BoostersOwned = boosters;
            }
        }

        public void AddValues(List<BoosterValue> values)
        {
            lock (BoostersValue)
            {
                BoostersValue = values;
            }
        }
    }
}
