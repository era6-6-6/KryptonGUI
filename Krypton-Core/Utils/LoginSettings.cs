using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Utils
{
    public class LoginSettings
    {
        public static bool FlyToSafeZoneAfterStart { get; set; } = false;
        public static bool WaitOn503Error { get; set; }
        public static int Error503Delay { get; set; } = 1000;
    }
}
