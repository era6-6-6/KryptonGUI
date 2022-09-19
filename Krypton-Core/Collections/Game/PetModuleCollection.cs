using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game
{
    public  class PetModuleCollection
    {

        public const short Pasive = 2;
        public const short defense = 3;
        public const short BoxCollector = 4;
        public const short ResourceCollector = 5;

        public static List<Tuple<string, short>> PetModules = new()
        {
           new("Pasive" , 2),
           new("Defense" , 3),
           new("BoxCollector" , 4),
           new("ResourceCollector" , 5)
        };
    
            
    }
}
