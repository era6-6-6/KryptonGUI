using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class InGameCollection
    {
        public string ActualFormation { get; set; } =
            Krypton_Core.Collections.Game.Collections.FormationCollection.DEFAULT_FORMATION;
        public bool PetEnabled { get; set; }
    }
}
