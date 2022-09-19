using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.Game.Objects;

namespace Krypton_Core.Collections.User
{
    public class Mines
    {
        public List<Mine> MinesAround = new();



        public void AddToMines(Mine mine)
        {
            lock (MinesAround) 
            {
                MinesAround.Add(mine);
            }
        }

        public void RemoveFromMines(string hash)
        {
            lock(MinesAround)
            {
                MinesAround.RemoveAll(x => x.Hash == hash);
            }
        }
    }
}
