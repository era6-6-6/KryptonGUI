using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Krypton_Core.BackPage.Data;

namespace Krypton_Core.BackPage.Settings
{
    public class BackGroudSettings
    {
        public UpgradeInterface UpgradeInterface = new();
        private Api Api { get; set; }
        public BackGroudSettings(Api api)
        {
            Api = api;
        }

        public bool SpinGates { get; set; } = false;
        public GateSettings Gates { get; set; } = new();



        private void UpgradeSkylabModule(bool up , SkylabData.ModuleInfo module)
        {
            if (up)
            {
                UpgradeInterface.AddToUpgrade(module);
            }
            else
            {
                UpgradeInterface.RemoveFromUpgrade(module.Name);
            }
        }
    }

    public class GateSettings
    {
        public bool Spin = true;
        public bool ABG = false;
        public bool Delta = false;
        public bool Epsilon = false;
        public bool Zeta = true;
        public bool Kappa = false;
        public bool Lambda = false;
        public bool Hades = false;
        public bool Kuiper = false;

        public bool PlaceGate = false;

        public int MinUridium { get; set; } = 0;
        public bool UseOnlyEE = false;
        public int MaxUriCost { get; set; } = 150;
        public int Click { get; set; } = 1;
    }

    public class UpgradeInterface
    {
        public List<SkylabData.ModuleInfo> ModulesToUpgrade { get; } = new();

        public void AddToUpgrade(SkylabData.ModuleInfo module)
        {
           
            if (module.Level >= 20)
            {
                return;
            }

            lock (ModulesToUpgrade)
            {
                SkylabData.ModuleInfo? item = ModulesToUpgrade?.Find(x => x.Name == module.Name);
                if(item != null) return;
                ModulesToUpgrade?.Add(module);
            }
        }

        public void RemoveFromUpgrade(string name)
        {
            lock (ModulesToUpgrade)
            {
                SkylabData.ModuleInfo? item = ModulesToUpgrade.Find(x => x.Name == name);
                if(item == null) return;
                else
                {
                    ModulesToUpgrade.Remove(item);
                }
            }
        }
    }
}
