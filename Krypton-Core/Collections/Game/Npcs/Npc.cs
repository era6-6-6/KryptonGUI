using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Npcs
{
    public class Npc : Players.Player
    {
        public bool isTaken { get; set; } = false;
    }
}
