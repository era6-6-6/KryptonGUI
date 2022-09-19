using Krypton_Core.BackPage.Data.enumAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Krypton_Core.BackPage.Data
{
    public class GalaxyGateModule
    {
        private Api Api { get; init; }
        public GateData? GateData { get; set; }
        private bool useMultiplier = false;
        private DateTime _nextRunGalaxyGate = DateTime.Now;
        public bool Run = false;
        #region Gate GatesBuilt
        private int GatesBuiltAlpha = 0;
        private int GatesBuiltBeta = 0;
        private int GatesBuiltGamma = 0;
        private int GatesBuiltDelta = 0;
        private int GatesBuiltEpsilon = 0;
        private int GatesBuiltZeta = 0;
        private int GatesBuiltKappa = 0;
        private int GatesBuiltLambda = 0;
        private int GatesBuiltHades = 0;
        private int GatesBuiltKuiper = 0;
        #endregion
        public GateItemsReceived GateItemsReceived { get; set; }

        public GalaxyGateModule(Api api)
        {
            Api = api;
            GateItemsReceived = new();
            

        }

        public async Task ReadGatesAsync()
        {
            Urls.BaseUrl = $"https://{Api._user.userData.Server}.darkorbit.com";
            Console.WriteLine(string.Format(Urls.GateInfo, Urls.BaseUrl, Api._user.userData.UserID,
                Api._user.userData.SID));
            var resultString = Api.ReturnClient().Get(string.Format(Urls.GateInfo, Urls.BaseUrl,
                Api._user.userData.UserID, Api._user.userData.SID));

            XmlSerializer serializer = new XmlSerializer(typeof(GateData));

            using (var reader = new StringReader(resultString))
            {
                GateData = (GateData)serializer.Deserialize(reader) as GateData;
            }

            await Task.Delay(20);
            Console.WriteLine("GG INFO");
            Console.WriteLine(GateData.Money);
            Console.WriteLine(GateData.Gates.Zeta().CurrentWave);
        }
        private bool IsMultiplierAvailable(GalaxyGate gate)
        {
            var gateName = gate.GetFullName().ToLower();

            var data = GateData.MultiplierInfo.MultiplierInfo.FirstOrDefault(x => x.Mode.ToLower() == gateName);
            if (data == null)
            {
                return false;
            }
            return data.Value >= 0;
        }
        public async Task<GateSpinData> SpinGateAsync(GalaxyGate gate, bool useMultiplier, int spinamount)
        {
            if (GateData == null) await ReadGatesAsync();
            Urls.BaseUrl = $"https://{Api._user.userData.Server}.darkorbit.com";
            var spinUrl = string.Empty;
            if (useMultiplier && IsMultiplierAvailable(gate))
            {
                if (spinamount == 1)
                {
                    spinUrl = string.Format(Urls.SpinGateMultiplier, Urls.BaseUrl, Api._user.userData.UserID, Api._user.userData.SID,
                       (int)gate, gate.GetFullName().ToLower());

                    if (GateData.Samples > 0)
                    {
                        spinUrl = string.Format(Urls.SpinGateSampleMultiplier, Urls.BaseUrl, Api._user.userData.UserID, Api._user.userData.SID,
                            (int)gate, gate.GetFullName().ToLower());
                    }
                }
                else
                {
                    spinUrl = string.Format(Urls.SpinGateMultiplierAmount, Urls.BaseUrl, Api._user.userData.UserID, Api._user.userData.SID,
                    (int)gate, gate.GetFullName().ToLower(), spinamount);

                    if (GateData.Samples > 0)
                    {
                        spinUrl = string.Format(Urls.SpinGateSampleMultiplierAmount, Urls.BaseUrl, Api._user.userData.UserID, Api._user.userData.SID,
                            (int)gate, gate.GetFullName().ToLower(), spinamount);
                    }
                }
            }
            else
            {
                if (spinamount == 1)
                {
                    spinUrl = string.Format(Urls.SpinGate, Urls.BaseUrl, Api._user.userData.UserID, Api._user.userData.SID,
                        (int)gate, gate.GetFullName().ToLower());

                    if (GateData.Samples > 0)
                    {
                        spinUrl = string.Format(Urls.SpinGateSample, Urls.BaseUrl, Api._user.userData.UserID, Api._user.userData.SID,
                            (int)gate, gate.GetFullName().ToLower());
                    }
                }
                else
                {
                    spinUrl = string.Format(Urls.SpinGateAmount, Urls.BaseUrl, Api._user.userData.UserID, Api._user.userData.SID,
                        (int)gate, gate.GetFullName().ToLower(), spinamount);

                    if (GateData.Samples > 0)
                    {
                        spinUrl = string.Format(Urls.SpinGateSampleAmount, Urls.BaseUrl, Api._user.userData.UserID, Api._user.userData.SID,
                            (int)gate, gate.GetFullName().ToLower(), spinamount);
                    }
                }
            }

            var resultString = string.Empty;
            try
            {
                resultString = Api.ReturnClient().Get(spinUrl);

                var serializer = new XmlSerializer(typeof(GateSpinData));

                GateSpinData result;

                using (var reader = new StringReader(resultString))
                {
                    result = (GateSpinData)serializer.Deserialize(reader) as GateSpinData;
                }
                EvaluateGateSpin(result, spinamount);

                return result;
            }
            catch (Exception e)
            {
                return default(GateSpinData);
            }


        }

        private void EvaluateGateSpin(GateSpinData spin, int spinamount)
        {
            try
            {
                GateItemsReceived.TotalSpins += spinamount;
                GateData.Samples = spin.Samples;
                GateData.EnergyCost.Text = spin.EnergyCost.Text;
                GateData.Money = spin.Money;

                GateData.MultiplierInfo = spin.MultiplierInfo;

                foreach (var spinItem in spin.Items.GetAllItems())
                {
                    if (spinItem.Type == "part" && !spinItem.Duplicate)
                    {
                        GateItemsReceived.GateParts++;
                        var gate = GateData.Gates.Gates.Find(x => x.Id == spinItem.GateId);
                        if (gate != null)
                        {
                            gate.Total = spinItem.Total;
                            gate.Current = spinItem.Current;
                        }
                    }

                    if (spinItem.Type == "battery")
                    {
                        switch (spinItem.ItemId)
                        {
                            case 2:
                                GateItemsReceived.X2 += spinItem.Amount;
                                break;
                            case 3:
                                GateItemsReceived.X3 += spinItem.Amount;
                                break;
                            case 4:
                                GateItemsReceived.X4 += spinItem.Amount;
                                break;
                            case 5:
                                GateItemsReceived.SAB += spinItem.Amount;
                                break;
                        }
                    }

                    if (spinItem.Type == "ore" && spinItem.ItemId == 4)
                    {
                        GateItemsReceived.Xenomit += spinItem.Amount;
                    }

                    if (spinItem.Type == "rocket")
                    {
                        switch (spinItem.ItemId)
                        {
                            case 3:
                                GateItemsReceived.PLT2021 += spinItem.Amount;
                                break;
                            case 11:
                                GateItemsReceived.ACM += spinItem.Amount;
                                break;
                        }
                    }

                    if (spinItem.Type == "logfile")
                    {
                        GateItemsReceived.LogDisks += spinItem.Amount;
                    }

                    if (spinItem.Type == "voucher")
                    {
                        GateItemsReceived.RepairCredits += spinItem.Amount;
                    }

                    if (spinItem.Type == "nanohull")
                    {
                        GateItemsReceived.NanoHull += spinItem.Amount;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
     
        private String getOptionforABG()
        {
            int i = 1;
            switch (i)
            {
                case 0:
                    return "option1";
                case 1:
                    return "option2";
                case 2:
                    return "option3";
                case 3:
                    return "option4";
                case 4:
                    return "option5";
                default:
                    return "null";
            }
        }

       

        private bool use3MultiplierABG(int a_curr, int a_max, int b_curr, int b_max, int g_curr, int g_max)
        {
            if (a_max - a_curr == 3) return b_curr <= b_max * 0.2 && g_curr <= g_max * 0.2;
            if (b_max - b_curr == 3) return a_curr <= a_max * 0.2 && g_curr <= g_max * 0.2;
            if (g_max - b_curr == 3) return a_curr <= a_max * 0.2 && b_curr <= b_max * 0.2;
            return false;
        }
        public void Stopping_gate_mode(String value)
        {
            switch (value)
            {
                case "can_not_get_more_parts":
                   
                    break;
                case "no_uridium/ee_left":
                    
                    break;
                case "minimum_uridium_reached":
                   
                    break;
                case "no_ee_left":
                   
                    break;
                case "max_spin_cost":
                   
                    break;
            }
            _nextRunGalaxyGate = DateTime.Now.AddMinutes((double)500);
        }
        private GalaxyGate GetSelectedGate()
        {
            if (Api.BackGroudSettings.Gates.ABG)
            {
                return GalaxyGate.Alpha;
            }

            if (Api.BackGroudSettings.Gates.Delta)
            {
                return GalaxyGate.Delta;
            }

            if (Api.BackGroudSettings.Gates.Epsilon)
            {
                return GalaxyGate.Epsilon;
            }

            if (Api.BackGroudSettings.Gates.Zeta)
            {
                return GalaxyGate.Zeta;
            }

            if (Api.BackGroudSettings.Gates.Kappa)
            {
                return GalaxyGate.Kappa;
            }

            if (Api.BackGroudSettings.Gates.Lambda)
            {
                return GalaxyGate.Lambda;
            }

            if (Api.BackGroudSettings.Gates.Hades)
            {
                return GalaxyGate.Hades;
            }

            if (Api.BackGroudSettings.Gates.Kuiper)
            {
                return GalaxyGate.Kuiper;
            }

            return GalaxyGate.None;
        }
        public async Task PlaceGateAsync(GalaxyGate gate)
        {
           
            await PlaceGateAsyncNew(gate);
            setGatesBuilt(gate);
           
            await ReadGatesAsync();
            
        }
        public async Task<bool> PlaceGateAsyncNew(GalaxyGate gate)
        {
            Urls.BaseUrl = $"https://{Api._user.userData.Server}.darkorbit.com";
           var placed = Api.ReturnClient().Get(string.Format(Urls.PlaceGate, Urls.BaseUrl, Api._user.userData.UserID,
                Api._user.userData.SID, (int)gate));

            return !placed.Contains("not_enough_parts");
        }
        public void setGatesBuilt(GalaxyGate gate)
        {
            switch ((int)gate)
            {
                case 1:
                    GatesBuiltAlpha++;
                    break;
                case 2:
                    GatesBuiltBeta++;
                    break;
                case 3:
                    GatesBuiltGamma++;
                    break;
                case 4:
                    GatesBuiltDelta++;
                    break;
                case 5:
                    GatesBuiltEpsilon++;
                    break;
                case 6:
                    GatesBuiltZeta++;
                    break;
                case 7:
                    GatesBuiltKappa++;
                    break;
                case 8:
                    GatesBuiltLambda++;
                    break;
                case 13:
                    GatesBuiltHades++;
                    break;
                case 19:
                    GatesBuiltKuiper++;
                    break;
            }
        }



        public async Task ExecuteSpinAsync()
        {
            if (GateData == null) await ReadGatesAsync();
            var currentGate = GateData.Gates.Get(GetSelectedGate());

            if (Api.BackGroudSettings.Gates.ABG)
            {
                var currentGateA = GateData.Gates.Get(GalaxyGate.Alpha);
                var currentGateB = GateData.Gates.Get(GalaxyGate.Beta);
                var currentGateG = GateData.Gates.Get(GalaxyGate.Gamma);

                if (getOptionforABG() == "option1")
                {
                    if (Api.BackGroudSettings.Gates.PlaceGate)
                    {
                        if ((currentGateA.Prepared && currentGateA.Ready) || (currentGateB.Prepared && currentGateB.Ready) || (currentGateG.Prepared && currentGateG.Ready))
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                        if (currentGateA.Ready && !currentGateA.Prepared)
                            await PlaceGateAsync(GalaxyGate.Alpha);
                        if (currentGateB.Ready && !currentGateB.Prepared)
                            await PlaceGateAsync(GalaxyGate.Beta);
                        if (currentGateG.Ready && !currentGateG.Prepared)
                            await PlaceGateAsync(GalaxyGate.Gamma);
                    }
                    else
                    {
                        if (currentGateA.Ready || currentGateB.Ready || currentGateG.Ready)
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                    }

                }
                else if (getOptionforABG() == "option2")
                {
                    if (Api.BackGroudSettings.Gates.PlaceGate)
                    {
                        if ((currentGateA.Prepared && currentGateA.Ready) && (currentGateB.Prepared && currentGateB.Ready) && (currentGateG.Prepared && currentGateG.Ready))
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                        if (currentGateA.Ready && !currentGateA.Prepared)
                            await PlaceGateAsync(GalaxyGate.Alpha);
                        if (currentGateB.Ready && !currentGateB.Prepared)
                            await PlaceGateAsync(GalaxyGate.Beta);
                        if (currentGateG.Ready && !currentGateG.Prepared)
                            await PlaceGateAsync(GalaxyGate.Gamma);
                    }
                    else
                    {
                        if (currentGateA.Ready && currentGateB.Ready && currentGateG.Ready)
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                    }
                }

                else if (getOptionforABG() == "option3")
                {
                    if (Api.BackGroudSettings.Gates.PlaceGate)
                    {
                        if (currentGateA.Prepared && currentGateA.Ready)
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                        if (currentGateA.Ready && !currentGateA.Prepared)
                            await PlaceGateAsync(GalaxyGate.Alpha);
                        if (currentGateB.Ready && !currentGateB.Prepared)
                            await PlaceGateAsync(GalaxyGate.Beta);
                        if (currentGateG.Ready && !currentGateG.Prepared)
                            await PlaceGateAsync(GalaxyGate.Gamma);
                    }
                    else
                    {
                        if (currentGateA.Ready)
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                    }
                }
                else if (getOptionforABG() == "option4")
                {
                    if (Api.BackGroudSettings.Gates.PlaceGate)
                    {
                        if (currentGateB.Prepared && currentGateB.Ready)
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                        if (currentGateA.Ready && !currentGateA.Prepared)
                            await PlaceGateAsync(GalaxyGate.Alpha);
                        if (currentGateB.Ready && !currentGateB.Prepared)
                            await PlaceGateAsync(GalaxyGate.Beta);
                        if (currentGateG.Ready && !currentGateG.Prepared)
                            await PlaceGateAsync(GalaxyGate.Gamma);
                    }
                    else
                    {
                        if (currentGateB.Ready)
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                    }
                }

                else if (getOptionforABG() == "option5")
                {
                    if (Api.BackGroudSettings.Gates.PlaceGate)
                    {
                        if (currentGateG.Prepared && currentGateG.Ready)
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                        if (currentGateA.Ready && !currentGateA.Prepared)
                            await PlaceGateAsync(GalaxyGate.Alpha);
                        if (currentGateB.Ready && !currentGateB.Prepared)
                            await PlaceGateAsync(GalaxyGate.Beta);
                        if (currentGateG.Ready && !currentGateG.Prepared)
                            await PlaceGateAsync(GalaxyGate.Gamma);
                    }
                    else
                    {
                        if (currentGateG.Ready)
                        {
                            Stopping_gate_mode("can_not_get_more_parts");
                            return;
                        }
                    }
                }

            }
            else
            {
                if (Api.BackGroudSettings.Gates.PlaceGate)
                {
                    if (currentGate.Prepared && currentGate.Ready)
                    {
                        Stopping_gate_mode("can_not_get_more_parts");
                        return;
                    }
                    if (currentGate.Ready && !currentGate.Prepared)
                        await PlaceGateAsync(GetSelectedGate());
                }
                else
                {
                    if (currentGate.Ready)
                    {
                        Stopping_gate_mode("can_not_get_more_parts");
                        return;
                    }
                }
            }


            if (GateData.EnergyCost.Text > GateData.Money && GateData.Samples <= 0)
            {
                Stopping_gate_mode("no_uridium/ee_left");
                return;
            }
            if (GateData.Money <= (int)Api.BackGroudSettings.Gates.MinUridium)
            {
                Stopping_gate_mode("minimum_uridium_reached");
                return;
            }
            if (Api.BackGroudSettings.Gates.UseOnlyEE && GateData.Samples <= 0)
            {
                Stopping_gate_mode("no_ee_left");
                return;
            }
            if (!(GateData.EnergyCost.Text <= (int)Api.BackGroudSettings.Gates.MaxUriCost))
            {
                Stopping_gate_mode("max_spin_cost");
                return;
            }


            //if (rbBuildABG.Checked)
            //    Log("Spinning ABG...");
            //else if (rbBuildKuiper.Checked)
            //    Log("Spinning Kuiper...");
            //else
            //    Log($"Spinning {GetSelectedGate().GetFullName()}...");

            var multiplierinfo = GateData.MultiplierInfo;
            foreach (var mi in multiplierinfo.MultiplierInfo)
            {
                if (mi.Mode.Contains(GetSelectedGate().GetFullName().ToLower()))
                {
                    //smart multiplayer
                    if (true)
                    {
                        if (Api.BackGroudSettings.Gates.ABG)
                        {
                            if (use3MultiplierABG(GateData.Gates.Get(GalaxyGate.Alpha).Current,GateData.Gates.Get(GalaxyGate.Alpha).Total, GateData.Gates.Get(GalaxyGate.Beta).Current, GateData.Gates.Get(GalaxyGate.Beta).Total, GateData.Gates.Get(GalaxyGate.Beta).Current, GateData.Gates.Get(GalaxyGate.Beta).Total))
                            {
                                if (2 <= mi.Value)
                                {
                                    useMultiplier = true;
                                }
                                else
                                {
                                    useMultiplier = false;
                                }
                            }
                            else
                            {
                                if (1 <= mi.Value)
                                {
                                    useMultiplier = true;
                                }
                                else
                                {
                                    useMultiplier = false;
                                }
                            }
                        }
                        else
                        {
                            if ((GateData.Gates.Get(GetSelectedGate()).Total - GateData.Gates.Get(GetSelectedGate()).Current) == 3)
                            {
                                if (2 <= mi.Value)
                                {
                                    useMultiplier = true;
                                }
                                else
                                {
                                    useMultiplier = false;
                                }
                            }
                            else
                            {
                                if (1 <= mi.Value)
                                {
                                    useMultiplier = true;
                                }
                                else
                                {
                                    useMultiplier = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (getMulplier() <= mi.Value)
                        {
                            useMultiplier = true;
                        }
                        else
                        {
                            useMultiplier = false;
                        }
                    }
                }
            }
            //energy charge
            var spin = await SpinGateAsync(GetSelectedGate(), useMultiplier, Api.BackGroudSettings.Gates.Click);

            foreach (var allItem in spin.Items.GetAllItems())
            {
                if (allItem.Duplicate)
                {
                    Console.WriteLine(
                    $"Received duplicate gate part > received multiplier");
                }
                else
                {
                    Console.WriteLine($"Received {allItem.ToString()}");
                }
            }

            
        }
        private int getMulplier()
        {
            int i = 0;
            switch (i)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3:
                    return 4;
                case 4:
                    return 5;
                default:
                    return 1;
            }
        }
    }
}
