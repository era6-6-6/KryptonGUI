using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class UserData
    {
        public string? Username { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        
        public int UserID { get; set; }
        public short FactionID { get; set; }
        public string? SID { get; set; }
        
        public string? Server { get; set; }
        public int MapID { get; set; }
        [JsonIgnore]
        public string? MapIP { get; set; }
        public string? MapName { get; set; }
        public int InstanceId { get; set; }
        public bool Ready { get;
            set; } = false;

    }
}
