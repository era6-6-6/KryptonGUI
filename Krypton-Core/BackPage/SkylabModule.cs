using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;

using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Krypton_Core.BackPage.Converter;
using Krypton_Core.BackPage.Data;

namespace Krypton_Core.BackPage
{
    public class SkylabModule
    {
        private Api Api { get; init; }
        public SkylabData SkylabData { get; set; }
        public SkylabModule(Api api)
        {
            this.Api = api;
            SkylabData = new SkylabData();
            

        }

        public async void PrintSkylabData()
        {

            await ReadSkylabAsync();
            Console.WriteLine("SkylabData");
            Console.WriteLine(SkylabData.SolarModuleInfo.Level);
            Console.WriteLine(SkylabData.PrometidRefineryInfo.Level);
            Console.WriteLine(SkylabData.PromeriumAmount);
            
        }
        public async Task ReadSkylabAsync()
        {
            var skylab =  Api.ReturnClient().Get($"https://{Api._user.userData.Server}.darkorbit.com/indexInternal.es?action=internalSkylab");

            EvaluateSkylabAsync(skylab);
            

        }
        public async Task<bool> UpgradeSkylabAsync(string module)
        {
            Urls.BaseUrl = $"https://{Api._user.userData.Server}.darkorbit.com";
            var techFactory = Api.ReturnClient().Get($"https://{Api._user.userData.Server}.darkorbit.com/indexInternal.es?action=internalSkylab");
            var reloadToken = Regex.Match(techFactory, "reloadToken=(.*?)'").Groups[1].Value;
            await Task.Delay(1500);
            Console.WriteLine(string.Format(Urls.UpgradeSkylab, Urls.BaseUrl, module, reloadToken));
            var result = Api.ReturnClient().Get(string.Format(Urls.UpgradeSkylab, Urls.BaseUrl, module, reloadToken));
            
            EvaluateSkylabAsync(result);

            if (SkylabData.GetByString(module) != null)
            {
                return SkylabData.GetByString(module).Upgrading;
            }

            return false;
        }
        private void EvaluateSkylabAsync(string skylabSource)
        {
            try
            {
                SkylabData.BaseModuleInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.BaseModuleName}");
                if (SkylabData.BaseModuleInfo.Upgrading)
                {
                    SkylabData.BaseModuleInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.BaseModuleName);
                }
                SkylabData.BaseModuleInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.BaseModuleName);

                SkylabData.SolarModuleInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.SolarModuleName}");
                if (SkylabData.SolarModuleInfo.Upgrading)
                {
                    SkylabData.SolarModuleInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.SolarModuleName);
                }
                SkylabData.SolarModuleInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.SolarModuleName);

                SkylabData.StorageModuleInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.StorageModuleName}");
                if (SkylabData.StorageModuleInfo.Upgrading)
                {
                    SkylabData.StorageModuleInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.StorageModuleName);
                }
                SkylabData.StorageModuleInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.StorageModuleName);

                SkylabData.XenoModuleInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.XenoModuleName}");
                if (SkylabData.XenoModuleInfo.Upgrading)
                {
                    SkylabData.XenoModuleInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.XenoModuleName);
                }
                SkylabData.XenoModuleInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.XenoModuleName);

                SkylabData.PrometiumCollectorInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.PrometiumCollectorName}");
                if (SkylabData.PrometiumCollectorInfo.Upgrading)
                {
                    SkylabData.PrometiumCollectorInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.PrometiumCollectorName);
                }
                SkylabData.PrometiumCollectorInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.PrometiumCollectorName);

                SkylabData.EnduriumCollectorInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.EnduriumCollectorName}");
                if (SkylabData.EnduriumCollectorInfo.Upgrading)
                {
                    SkylabData.EnduriumCollectorInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.EnduriumCollectorName);
                }
                SkylabData.EnduriumCollectorInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.EnduriumCollectorName);

                SkylabData.TerbiumCollectorInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.TerbiumCollectorName}");
                if (SkylabData.TerbiumCollectorInfo.Upgrading)
                {
                    SkylabData.TerbiumCollectorInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.TerbiumCollectorName);
                }
                SkylabData.TerbiumCollectorInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.TerbiumCollectorName);

                SkylabData.PrometidRefineryInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.PrometidRefineryName}");
                if (SkylabData.PrometidRefineryInfo.Upgrading)
                {
                    SkylabData.PrometidRefineryInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.PrometidRefineryName);
                }
                SkylabData.PrometidRefineryInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.PrometidRefineryName);

                SkylabData.DuraniumRefineryInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.DuraniumRefineryName}");
                if (SkylabData.DuraniumRefineryInfo.Upgrading)
                {
                    SkylabData.DuraniumRefineryInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.DuraniumRefineryName);
                }
                SkylabData.DuraniumRefineryInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.DuraniumRefineryName);

                SkylabData.PromeriumRefineryInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.PromeriumRefineryName}");
                if (SkylabData.PromeriumRefineryInfo.Upgrading)
                {
                    SkylabData.PromeriumRefineryInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.PromeriumRefineryName);
                }
                SkylabData.PromeriumRefineryInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.PromeriumRefineryName);

                SkylabData.SepromRefineryInfo.Upgrading = skylabSource.Contains($"timers_{SkylabData.SepromRefineryName}");
                if (SkylabData.SepromRefineryInfo.Upgrading)
                {
                    SkylabData.SepromRefineryInfo.TimeLeft = GetSkylabTimeLeft(skylabSource, SkylabData.SepromRefineryName);
                }
                SkylabData.SepromRefineryInfo.Level = GetSkylabModuleLevel(skylabSource, SkylabData.SepromRefineryName);
            }
            catch { }



            SkylabData.IsSending = skylabSource.Contains("timers_activeTransport");

        }
     

        private TimeSpan GetSkylabTimeLeft(string pageSource, string module)
        {
            var regFound = Regex.Match(pageSource, "tmp\\.init\\(\\s*\'" + module + "\',\\s*(.*?),\\s*(.*?)\\s*\\);");
            var dateEndSeconds = int.Parse(regFound.Groups[2].Value);
            return dateEndSeconds.TimestampToDate().Subtract(DateTime.Now);
        }


        private int GetSkylabModuleLevel(string pageSource, string module)
         {
         
             var r = Regex.Match(pageSource,
                 module +
                 "\'\\);\"\">[a-zA-z0-9\\\"_<>=\\-\\/\\s]*(.*?)<\\/div>[a-zA-z0-9\\\"_<>=\\-\\/\\s]*skylab_font_level(_inactive)?\">(.*?)<\\/div>");
             if (!r.Success)
             {
                 return 0;
             }
         
             return int.Parse(r.Groups[3].Value);
         }

        


    }

    public class Interface
    {
        public string Name { get; set; }
        public double Count { get; set; }
        public double MaxCount { get; set; }
        public bool Upgrading { get; set; }
        public bool Level { get; set; }
    }
}
