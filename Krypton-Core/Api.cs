using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using Unglide;
using Krypton_Core.InternalPacketsMethods;
using Krypton_Core.Commands.Write;
using Krypton_Core.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Castle.Components.DictionaryAdapter;
using Krypton_Core.BackPage;
using Krypton_Core.BackPage.Data;
using Krypton_Core.BackPage.Settings;
using Krypton_Core.Collections.Game.Npcs;
using Managers;
using Krypton_Core.Utils;
using Krypton_Core.Managers.Internal;
using Krypton_Core.Managers.PathFinding;
using Krypton_Core.Settings;

namespace Krypton_Core
{

    public enum EventReason
    {
        BACKGROUND , MESSAGE
    }

    //should be called only 1 time when login
    public class Api : IDisposable
    {
        public event EventHandler<EventReason>? OnCall;
        public event EventHandler<ErrorManager.ErrorManager>? Error; 

        public static List<Api> apis = new List<Api>();
        public User? _user { get; set; }
        public SortingClass? Sr { get; set; }
        private HttpClient? client { get; set; }
        public Tweener? Tweener { get; set; }

        #region Logic Bools

        private bool SkylabRunner { get; set; } = false;
        private bool GGclickerRunner { get; set; } = false;
        private bool GetShipDataRunner { get; set; } = false;

        #endregion
        public NpcCollection? npcSet { get; set; }
        public PathFinderManager? PathFinder;

        public event EventHandler<Api>? onBotStop; 
        public SkylabModule? Skylab { get; set; }
        public GalaxyGateModule? GalaxyGate { get; set; }
        public BackGroudSettings? BackGroudSettings { get; set; }

        public int RunTime { get; set; } = 0;
        public event EventHandler<EventArgs>? onHangarReady;
        public bool UserExist = false;
        public Logic.LogicMethods? logic { get; set; }
        public event EventHandler<LogManager>? onLogMessage;
        public event EventHandler<string>? LoginFailed;
        [JsonIgnore]
        public static List<User> Users = new List<User>();
        public Api(string username , string password)
        {
            // test
            if (Users.Find(x => x.Name == username) == null)
            {
                _user = new User(username, password);
                Users.Add(_user);
            }
            else
            {
                UserExist = true;
                Destroy();
                return;
            }
           
            npcSet = new();
            Tweener = new Tweener();
            Sr = new SortingClass(this);
            CleanLists();
            apis.Add(this);

            Task.Run(async () => await TweenMethod());
            Skylab = new(this);
            BackGroudSettings = new(this);
            GalaxyGate = new(this);
            SkylabRunner = true;
            GGclickerRunner = true;
            GetShipDataRunner = true;

            PathFinder = new(this);
            Task.Run(async () => await UpgradeSkylab());
            Task.Run(async() =>await ExecuteSpin());
            

        }

        public void DisableAccount()
        {
            GGclickerRunner = false;
            SkylabRunner = false;
            GetShipDataRunner = false;
            
        }

        public void OnBotStop()
        {
            onBotStop?.Invoke(this, this);
        }

        public async Task RemoveFromTween()
        {
            try
            {
                Tweener.TargetCancel(_user.Position);
                await Task.Delay(200);
                _user.Position.Moving = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
            
        }
        public HttpClient ReturnClient()
        {
            return client;

        }
        private void Destroy()
        {
            Dispose();
            GC.SuppressFinalize(this);
            GC.Collect();

        }

        public void InvokeError(string message, bool fatal = false, bool useWindow = true)
        {
            Error?.Invoke(this , new ErrorManager.ErrorManager(message , fatal , useWindow));
        }

        public void ChangeConfig(short config)
        {
            _user.events.CanChangeConfig = false;
            SendPacket(new MessageSend($"S|CFG|{config}|{_user.userData.UserID}|{_user.userData.SID}"));
            
            Task.Run(async () => await Task.Delay(5300)).ContinueWith(task => _user.events.CanChangeConfig = true);

        }

        public void CallEvent(object sender ,EventReason reason)
        {
            OnCall?.Invoke(sender , reason);
        }

        private async Task ExecuteSpin()
        {
            while (GGclickerRunner)
            {
                try
                {
                    await Task.Delay(50);
                    if (BackGroudSettings.SpinGates)
                    {
                        await GalaxyGate.ExecuteSpinAsync();
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception e)
                {
                    InvokeError(e.ToString() , false , false);
                    await Task.Delay(10000);
                }
               
            }
        }
        public void PushLog(string message)
        {

            onLogMessage?.Invoke(this,new LogManager(message));
        }

        private async Task UpgradeSkylab()
        {
            while (SkylabRunner)
            {
                try
                {
                    await Task.Delay(100_000);
                    if (BackGroudSettings.UpgradeInterface.ModulesToUpgrade.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                            foreach (var item in BackGroudSettings.UpgradeInterface.ModulesToUpgrade)
                            {
                                if (item.Upgrading)
                                {
                                    continue;
                                    
                                }
                                if (item.Level >= Skylab.SkylabData.BaseModuleInfo.Level)
                                {
                                    if (Skylab.SkylabData.BaseModuleInfo.Upgrading == false)
                                    {
                                        await Skylab.UpgradeSkylabAsync(Skylab.SkylabData.BaseModuleInfo.Name);
                                    }

                                    continue;
                                }
                                 
                                await Skylab.UpgradeSkylabAsync(item.Name);
                                await Task.Delay(1000);
                            }
                        
                    }

                }
                catch (Exception e)
                {
                    InvokeError(e.Message ,false , false);
                    
                }

                

            }

        }

        private async Task TweenMethod()
        {
             while(true)
             {
                    try
                    {
                        if(true)
                        {
                            var sw = new Stopwatch();
                            sw.Start();
                            await Task.Delay(13);
                            Tweener.Update(sw.ElapsedMilliseconds);
                            sw.Stop();
                        }
                        else
                        {
                            try
                            {
                                Tweener.TargetCancel(_user);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }

                            Thread.Sleep(4000);
                        }
                    }catch(Exception ex)
                    {
                        InvokeError(ex.ToString(), false, false);
                    }
             }
            
        }
        
        public async Task Jump()
        {
            await Task.Delay(1000);
            SendPacket(new JumpPortal());
        }

        public async Task ReconnectAfterJump(int mapId = 0)
        {
            if (mapId == 0)
            {
                await GetMapID();
            }
            else
            {
                _user.userData.MapID = mapId;
            }
            await GetIP();
            CleanLists();
            _user.packetManager?.Reconnect();


        }

        private void SendPacket(Command com)
        {
            _user.packetManager?.Send(com);
        }
        


        public async Task ResetConnection(object sender)
        {
            try
            {

                await RemoveFromTween();
                await GetIP();
                _user.packetManager?.Reconnect();
               
            }
            catch (Exception ex)
            {
                InvokeError(ex.ToString() , false , false);
            }

        }
        bool FirstInit = true;
        public async Task GetHangarInfo()
        {
            while (GetShipDataRunner)
            {
                _user.Ammo.Ammos.Clear();
                var textMain = GetHangar();

                Match match =
                    Regex.Match(EncodingDecoding.Base64Decode(textMain), "{\"hangarID\":\"([0-9]+)\",\"hangar_is_active\":true,\"general\":{\"ship\":{\"I\":(.*?),\"HP\":(.*?),\"L\":(.*?),\"SM\":\"(.*?)\",\"M\"");
                await Task.Delay(500);

              

                var text = GetHangarText(EncodingDecoding.Base64Encode("{\"params\":{\"hi\":" + $"{match.Groups[1].Value}" + "}}"));
                AmmoSortClass? sort = new AmmoSortClass(this, EncodingDecoding.Base64Decode(text));
                if(_user.Ammo.LCB10 < 2000)
                {
                    
                    if(_user.statistics.Credits < 100000)
                    {
                        BuyAmmo(1000);
                    }
                    else
                    {
                        BuyAmmo(10000);
                        PushLog("Bought 10 000 x1");
                        _user.Ammo.LCB10 += 10000;
                    }
                }
                if (FirstInit)
                {
                    var ship = new HangarParser(EncodingDecoding.Base64Decode(textMain));
                    _user.OwnedShips = ship.Ships;
                    onHangarReady?.Invoke(this, EventArgs.Empty);
                }
                
                await Task.Delay(120_000);

            }

        }
        public async Task ChangeHangar(int id)
        {
            
            var result = client.Get($"https://{_user.userData.Server}.darkorbit.com/indexInternal.es?action=internalDock");
            
            var match = Regex.Match(result, $"href=\"(.*?)\"\n                        >\n                        {id}\n");
            await Task.Delay(2000);
            if (!match.Success) 
                return;
            
            client.Get($"https://{_user.userData.Server}.darkorbit.com/{match.Groups[1].Value}");
            

        }

        private async Task GetSkylabInfo()
        {
            while (SkylabRunner)
            {
                try
                {
                    await Task.Delay(30000);
                    await GalaxyGate.ReadGatesAsync();
                    Skylab.PrintSkylabData();
                    
                }
                catch (Exception e)
                {
                    InvokeError(e.ToString() , false , false);
                    
                }
            }
        }
        private void BuyAmmo(int count)
        {
            client.Post($"https://{_user.userData.Server}.darkorbit.com/ajax/shop.php", $"action=purchase&category=battery&itemId=ammunition_laser_lcb-10&amount={count}&level=-1&selectedName=");
        }
        private string GetHangarText(string postReq)
        {
            var result = client.Post($"https://{_user.userData.Server}.darkorbit.com/flashAPI/inventory.php", $"action=getHangar&params={postReq}");
            return result;
        }
        private string GetHangar()
        {
            var result = client.Post($"https://{_user.userData.Server}.darkorbit.com/flashAPI/inventory.php", "action=getHangarList&params=e30=");
            return result;
            
        }


        public async void StartSession()
        {
            Login:
            PushLog("logging in ");
            Console.WriteLine("--StartSession--");
            if (_user.userData.Password == null)
            {
                CrashLogManager.Send(_user.logMsg, $"Password is not set for '{_user.Name}'", CrashLogManager.Type.FATAL);
                throw new Exception("Password is not set");
            }
            
            try
            {
                await GetData();

            }
            catch (Exception ex)
            {
                // TODO: @era - Cant find map id reason (can't put a map cause we can't see the form)
                // TODO: @era - wrong name and password too
                InvokeError(ex.ToString(), false, false);
                PushLog(ex.Message);
                if (ex.ToString().Contains("503"))
                {
                    if (LoginSettings.WaitOn503Error)
                    {
                        await Task.Delay(LoginSettings.Error503Delay);
                        goto Login;
                    }
                }
                LoginFailed?.Invoke(this, ex.Message);
                return;
                
                
            }
            await Task.Delay(1000);

            
            _user.packetManager = new PacketManager(this);
            RegisterEvents();

            _user.packetManager.Send(new VersionRequest("564751d804ce868b7a8f0a82e0e9c9e6"));

            _user.packetManager.Send(new Login(_user.userData.UserID, _user.userData.InstanceId, "564751d804ce868b7a8f0a82e0e9c9e6", _user.userData.SID, 0));
            _user.Online = true;
            PushLog("Logged in");

        }
        private void RegisterEvents()
        {
            _user.packetManager.PacketSorter.RegisterEvents();
            _user.packetManager.PetEvents.RegisterEvents();
            _user.packetManager.GroupEvents.RegisterEvents();
        }
        public void CleanLists()
        {
            _user.players.Clean();
            _user.BasesPorts.Clear();
            _user.Boxes.Clear();
            _user.Barriers.Points.Clear();
        }
        public bool SuccesfullLogin { get; set; } = false;
        public bool TravelMode { get; set; } = false;

        private async Task GetData()
        {
            await Task.Delay(1000);
            await Login();
            await GetMapID();
            await GetInfo();
            await GetIP();
            Task.Run(async() => await GetHangarInfo());
        }


        private async Task Login()
        {
            _user.userData.Ready = false;
            await Task.Delay(1000);
            
            client = new HttpClient();
            var response = client.Get("https://www.darkorbit.com" , useProxy: true);
            _user.Response = response;
            if (_user.Response.Contains("var RecaptchaOptions"))
            {
                PushLog("Captcha detected!");
                LoginFailed?.Invoke(this , "Captcha detected");
                return;
            }



            Match match = Regex.Match(response.ToString(), "class=\"bgcdw_login_form\" action=\"(.*)\">");

            if (match.Success)
            {

                
                client.Post(WebUtility.HtmlDecode(match.Groups[1].Value), $"username={System.Web.HttpUtility.UrlDecode(_user.Name)}&password={System.Web.HttpUtility.UrlEncode(_user.userData.Password)}" , true);


            }
            match = Regex.Match(client.lastUrl, "https://(.*?).darkorbit.com");
            if (match.Success)
            {
                _user.userData.Server = match.Groups[1].Value;
                SuccesfullLogin = true;
                PushLog("Login to darkorbit account was succesfull");
            }
            else
            {
                PushLog("Login failed");
                return;
            }
            
            Task.Run(async () => await GetSkylabInfo());


        }



        public async Task<string> GetIP(int? mapId = null)
        {
            await Task.Delay(100);
            CrashLogManager.Send(_user.logMsg, $"Requesting all maps of '{_user.userData.Username}'", CrashLogManager.Type.MESSAGE);
            var response = client.Get($"https://{_user.userData.Server}.darkorbit.com/spacemap/xml/maps.php");
            if (mapId == null)
            {
                var match = Regex.Match(response,
                    $"<map id=\"{_user.userData.MapID}\">\n       <gameserverIP>(.*)</gameserverIP>\n    </map>");

                if (match.Success)
                {
                    _user.userData.MapIP = match.Groups[1].Value;
                    return match.Groups[1].Value;
                    
                }
            }else
            {
                var match = Regex.Match(response,
                   $"<map id=\"{mapId}\">\n       <gameserverIP>(.*)</gameserverIP>\n    </map>");
                PushLog("Got server ip succesfully");
                return match.Groups[1].Value;

            }
            return "";
            

        }
        public async Task GetMapID()
        {
            await Task.Delay(100);
            var response = client.Get($"https://{_user.userData.Server}.darkorbit.com/indexInternal.es?action=internalMapRevolution");
            var match = Regex.Match(response, "\"mapID\": \"(.*?)\",\"");
            _user.Response = response;

            if (match.Success)
            {
                
                _user.userData.MapID = int.Parse(match.Groups[1].Value);
                PushLog($"Got mapID: {_user.userData.MapID}");
                Console.WriteLine(_user.userData.MapID);
               
            }
            else
            {
                
                PushLog("Wrong name or password");
            }


        }
        private async Task GetInfo()
        {
            await Task.Delay(100);
            string mapResponse = client.Get($"https://{_user.userData.Server}.darkorbit.com/indexInternal.es?action=internalMapRevolution");
            var match = Regex.Match(mapResponse, "{\"pid\":([0-9]+),\"uid\":([0-9]+)[\\w,\":]+sid\":\"([0-9a-z]+)\"");


            _user.userData.UserID = int.Parse(match.Groups[2].ToString());
            _user.userData.SID = match.Groups[3].ToString();

            match = Regex.Match(mapResponse, "mapID\": \"([0-9]*)\"");
            _user.userData.MapID = int.Parse(match.Groups[1].ToString());
            match = Regex.Match(mapResponse, "\"spacemap\",\"pid\": \"([0-9]+)\",\"boardLink\":");
            _user.userData.InstanceId = int.Parse(match.Groups[1].Value);
            
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            //    _user = null;
            //    Sr = null;
            //    client = null;
            //    Tweener = null;
            //    npcSet = null;
            //    PathFinder = null;
            //    Skylab = null;
            //    GalaxyGate = null;
            //    BackGroudSettings = null;
            //    logic = null;
            DisableAccount();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        ~Api()
        {
            Console.WriteLine("Api was disposed and dissapeared from resources!");
        }




    }
}