using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.BackPage.Data
{
    public class SkylabData
    {
        public class ModuleInfo
        {
            public string Name { get; set; }
            public bool Upgrading { get; set; }

            public TimeSpan TimeLeft { get; set; }

            public int Level { get; set; }

            public ModuleInfo(string name)
            {
                Name = name;
                TimeLeft = TimeSpan.Zero;
            }
        }

        public const string BaseModuleName = "baseModule";
        public const string PrometiumCollectorName = "prometiumCollector";
        public const string EnduriumCollectorName = "enduriumCollector";
        public const string TerbiumCollectorName = "terbiumCollector";
        public const string SolarModuleName = "solarModule";
        public const string StorageModuleName = "storageModule";
        public const string PrometidRefineryName = "prometidRefinery";
        public const string DuraniumRefineryName = "duraniumRefinery";
        public const string PromeriumRefineryName = "promeriumRefinery";
        public const string XenoModuleName = "xenoModule";
        public const string SepromRefineryName = "sepromRefinery";

        public ModuleInfo BaseModuleInfo { get; set; }
        public ModuleInfo PrometiumCollectorInfo { get; set; }
        public ModuleInfo EnduriumCollectorInfo { get; set; }
        public ModuleInfo TerbiumCollectorInfo { get; set; }
        public ModuleInfo SolarModuleInfo { get; set; }
        public ModuleInfo StorageModuleInfo { get; set; }
        public ModuleInfo PrometidRefineryInfo { get; set; }
        public ModuleInfo DuraniumRefineryInfo { get; set; }
        public ModuleInfo PromeriumRefineryInfo { get; set; }
        public ModuleInfo XenoModuleInfo { get; set; }
        public ModuleInfo SepromRefineryInfo { get; set; }

        public bool IsSending { get; set; }

        public int PromeriumAmount { get; set; }
        public int SepromAmount { get; set; }

        public ModuleInfo GetByString(string module)
        {
            switch (module)
            {
                case BaseModuleName:
                    return BaseModuleInfo;
                case PrometiumCollectorName:
                    return PrometiumCollectorInfo;
                case EnduriumCollectorName:
                    return EnduriumCollectorInfo;
                case TerbiumCollectorName:
                    return TerbiumCollectorInfo;
                case SolarModuleName:
                    return SolarModuleInfo;
                case StorageModuleName:
                    return StorageModuleInfo;
                case PrometidRefineryName:
                    return PrometidRefineryInfo;
                case DuraniumRefineryName:
                    return DuraniumRefineryInfo;
                case PromeriumRefineryName:
                    return PromeriumRefineryInfo;
                case XenoModuleName:
                    return XenoModuleInfo;
                case SepromRefineryName:
                    return SepromRefineryInfo;
                default:
                    return null;
            }
        }

        public SkylabData()
        {
            BaseModuleInfo = new ModuleInfo(BaseModuleName);
            PrometiumCollectorInfo = new ModuleInfo(PrometiumCollectorName);
            EnduriumCollectorInfo = new ModuleInfo(EnduriumCollectorName);
            TerbiumCollectorInfo = new ModuleInfo(TerbiumCollectorName);
            SolarModuleInfo = new ModuleInfo(SolarModuleName);
            StorageModuleInfo = new ModuleInfo(StorageModuleName);
            PrometidRefineryInfo = new ModuleInfo(PrometidRefineryName);
            DuraniumRefineryInfo = new ModuleInfo(DuraniumRefineryName);
            PromeriumRefineryInfo = new ModuleInfo(PromeriumRefineryName);
            XenoModuleInfo = new ModuleInfo(XenoModuleName);
            SepromRefineryInfo = new ModuleInfo(SepromRefineryName);
        }
    }
}
