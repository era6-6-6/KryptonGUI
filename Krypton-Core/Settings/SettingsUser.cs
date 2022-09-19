using Krypton_Core.Collections.Game.Ammo;
using System.Text.RegularExpressions;

namespace Krypton_Core.Settings;

public class SettingsUser
{
    public int FollowUserID = 172450601;

    public General General { get; set; } = new();
    public AmmoSettings AmmoSettings { get; set; } = new();
    public Configs Configs { get; set; } = new();
    public AutoBuy AutoBuy { get; set; } = new();
    public PET PET { get; set; } = new();
    public Render Render { get; set; } = new();
    public GroupSettings GroupSet { get; set; } = new();
    public GUI GUI { get; set; } = new();

}

public class GUI
{
    public string? ImageSource { get; set; } = null;
    public bool UseDarkorbitBacground { get; set; }
}

public class GroupSettings
{
    public bool? Leader { get; set; } = false;
    public List<GroupInter> Members { get; set; } = new();
    public int LeaderId { get; set; }
}
 public class General
    {
        public int RepairAt { get; set; } = 30;
        public int TargetMapID { get; set; } = 2;
        public string? Image { get; set; } = null;
        public short RepairAtPlace { get; set; } = 1;
        public bool RunFromEnemies { get; set; } = true;
        public bool Drag { get; set; } = false;
        public int RunFromEnemiesRadius { get; set; } = 500;
        public bool StopCircle { get; set; } = false;
        public List<string> NpcsToKill = new List<string>();

        public List<string> BoxesToCollect = new List<string>();

        
 
    }
    public class AmmoSettings
    {
        public string Ammo { get; set; } = AmmoCollection.LCB_10;
        public bool AutoSab { get; set; } = false;
    }
    public class Configs
    {
        public short KillingCnf { get; set; } = 1;
        public short RunningConfig { get; set; } = 1;
        public short RepairConfig { get; set; } = 1;

        public string KillFormation { get; set; } = "1";
        public string RunFormation { get; set; } = "1";
        public string RepairFormation { get; set; } = "1";
    }

    public class AutoBuy
    {
        public bool AutoBuyAmmo { get; set; } = false;
        public string AmmoType = AmmoCollection.LCB_10;
        public bool AutoBuyRocket { get; set; } = false;
        public string RocketType = AmmoCollection.PLT_2026;
    }

    public class PET
    {
        public bool UsePet { get; set; } = false;
        public short? PetModule { get; set; }
        public bool UsePetLinkWhenRunning { get; set; } = false;
        public bool BuyFuel { get; set; }
    }
    public class Render
    {
        public bool Npcs { get; set; } = true;
        public string? NpcColor { get; set; } = null;
        public bool Boxes { get; set; } = true;
        public string? BoxesColor { get; set; } = null;
        public bool Bases { get; set; } = true;
        public string? BasesColor { get; set; } = null;
        public bool Ports { get; set; } = true;
        public string? PortsColor { get; set; } = null;
        public bool EnemyPlayers { get; set; } = true;
        public string? EnemyPlayersColor { get; set; } = null;
        public bool AllyPlayers { get; set; } = true;
        public string? AllyPlayersColor { get; set; } = null;
        public bool Rectangle { get; set; } = true;
        public string? RectangleColor { get; set; } = null;
    }




