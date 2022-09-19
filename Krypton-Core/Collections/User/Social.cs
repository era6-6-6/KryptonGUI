using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class Social
    {
        #region Social
        public int ClanID { get; set; }
        public string? ClanTag { get; set; }
        public uint FactionID { get; set; } = 3;
        public bool PortReady { get; set; }
        #endregion
    }
}
