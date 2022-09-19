using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class Events
    {
        internal bool PallaRelog;

        public bool LoggedOff { get; set; }

        public bool Shooting { get; set; } = false;
        public bool ReloadComplete { get; set; } = true;

        public bool CanChangeConfig { get; set; } = true;

    }
}
