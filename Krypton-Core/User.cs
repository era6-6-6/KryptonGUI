using Krypton_Core.Collections.Game.Gates;
using Krypton_Core.Collections.Game.Npcs;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Collections.User;
using Krypton_Core.Managers;
using Krypton_Core.Settings;
using Krypton_Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

using Krypton_Core.Collections.Collectables;
using Krypton_Core.Collections.Game.Items;
using Krypton_Core.StatsManager.StatsCollections;

namespace Krypton_Core
{
    //just interface for user can be called 1 time to api
    public class User
    {
        [JsonIgnore]
        public string? Response { get; set; }
        public string Name { get; private set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public List<Tuple<int, int>> trail = new List<Tuple<int, int>>();

        public bool Destroyed { get; set; } = true;
        public bool LogoutInvoked { get; set; } = false;
        public bool Online { get; set; } = false;

        public int Ping { get; set; } = 0;

        public Player? Target { get; set; }
        [JsonIgnore]
        public Box? Box { get; set; }
        public PacketManager? packetManager { get; set; }
        [JsonIgnore]
        public List<string> LogMessages = new List<string>();
        [JsonIgnore]
        public LogMessage logMsg = new();
        [JsonIgnore]
        public Log log = new();
        [JsonIgnore]
        public List<Ship> OwnedShips = new();
        [JsonIgnore]
        public SettingsUser setting { get; set; }

        #region Collections
        [JsonIgnore]
        public Ammo Ammo = new Ammo();
        [JsonIgnore]
        public Barriers Barriers = new Barriers();
        public BasesPorts BasesPorts = new();
        public Boxes Boxes = new();
        [JsonIgnore]
        public Krypton_Core.Collections.User.Events events = new();

        
        public Boosters Boosters = new();

        public InGameCollection InGameCollection { get; } = new();
        public bool Travel = false;
        public Players players = new();
        public Position Position { get; set; }
        public StatsColl StatsCollection = new();

        [JsonIgnore]
        public Position ActualPosition { get; set; }
        public ShipInfo shipInfo = new();
        public Social social = new();
        public Statistics statistics = new();
        public UserData userData = new();
        public Mines Mines = new();
        

        [JsonIgnore]
        public string TravelAcross { get; internal set; }
        #endregion
        



        public User(string username, string password)
        {
            
                Name = username;
                userData.Username = username;
                userData.Password = password;
                Password = password;
                setting = new SettingsUser();
                Position = new Position();
        }




        #region Static getters

        #endregion



    }
}
