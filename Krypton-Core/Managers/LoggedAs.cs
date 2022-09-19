using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Managers
{
    public class LoggedAs
    {
        public static string? LoggedAS { get; set; }
        public static int MaxSessions { get; set; } = 9999;
        public static string? ExpireAt { get; set; }
        public static string? HashedPassword { get; set; }
    }
           



}
