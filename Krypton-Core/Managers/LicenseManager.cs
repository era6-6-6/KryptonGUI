using System.Collections.ObjectModel;
using Krypton_Core.Settings;
using Krypton_Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Krypton_Core.Managers
{
    public class LicenseManager
    {
        private TcpClient? tcpClient { get; set; }
        Stream? stream { get; set; }
        public bool canRun { get; set; }
        private string? Response { get; set; }
        int ID { get; set; }
        public string Username { get; set; }
        public int MaxSessions { get; set; }
        public DateTime ExpireAT { get; set; }
        public static List<Api> apis = new List<Api>();
        public event EventHandler<LicenseManager>? onLogin;
        public event EventHandler<Api>? Stop;
        public event EventHandler<Api>? Start;
        Thread listener {get;set;}

        public event EventHandler<Api>? onDarkorbitLogin;


        public LicenseManager()
        {
            
            

            
        }

        private async Task Ping()
        {
            while (true)
            {
                try
                {
                    SendPacket("010|");
                    await Task.Delay(100);
                }catch
                {
                    Console.WriteLine("Error in ping");
                }
            }
        }
 

        private async Task SendUsers()
        {
            //while (true)
            //{
            //    try
            //    {
            //        if (apis.Count <= 0)
            //        {
            //            await Task.Delay(100);
            //            continue;
            //        }
            //        await Task.Delay(50);
            //        SendPlayerInfo();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex);
            //       //await Recconect();
            //    }
            //}
        }
        private async Task Recconect()
        {
            Login(LoggedAs.LoggedAS , LoggedAs.HashedPassword);
            await Task.Delay(100);
            
        }
        private void Parse(string packet)
        {
            var pck = packet.Split('|');

            switch (pck[0])
            {
                case "004":
                    ID = int.Parse(pck[2]);
                    Username = pck[3];
                    MaxSessions = int.Parse(pck[4]);
                    try
                    {

                        ExpireAT = Convert.ToDateTime(pck[6],
    System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(pck[6]);
                        Console.WriteLine(ex);
                    }
                    break;
                case "007":
                    {
                        Console.WriteLine("Starting");
                        var api = apis.FirstOrDefault(x => x._user.Name == pck[1]);
                        if (api == null)
                        {
                            return;
                        }

                        Start?.Invoke(this, api);
                        break;

                    }
                case "008":
                    {
                        Console.WriteLine("Stopping");
                        var api = apis.FirstOrDefault(x => x._user.Name == pck[1]);
                        if (api == null)
                        {
                            return;
                        }
                        Stop?.Invoke(this, api);

                        break;

                    }
                    case "011":
                    {
                        var api = apis.FirstOrDefault(x => x._user.Name == pck[1]);
                        lock(api._user)
                        {
                            api._user.setting = Newtonsoft.Json.JsonConvert.DeserializeObject<SettingsUser>(pck[2]);
                            Console.WriteLine("Settings Loaded");
                            Console.WriteLine(api._user.setting.General.TargetMapID);
                            api._user.setting.AmmoSettings.Ammo = (string)typeof(Collections.Game.Ammo.AmmoCollection).GetField(api._user.setting.AmmoSettings.Ammo).GetValue(this);
                            Console.WriteLine(api._user.setting.AmmoSettings.Ammo);
                        }
                        break;
                    }
                    case "012":
                    {
                      
                        break;
                    }

            }
        }
        private void ListenForPackets()
        {
            while (true)
            {
                try
                {


                    var bytes = new byte[4096];
                    var read = stream.Read(bytes);
                    var packet = Encoding.ASCII.GetString(bytes);
                    Parse(packet);
                    Thread.Sleep(50);
                }
                catch
                {
                    // Console.WriteLine(ex);
                }

            }
        }


        public static void AddToApi(Api api)
        {
            apis.Add(api);
        }

        public static void RemoveFromApi(Api api)
        {
            apis.Remove(api);
        }

        private bool CanRun()
        {
            return true;
        }
        public bool NewAccount(Api api)
        {
            SendPacket(Packet.CreatePacket("accadd", $"{api._user.userData.Username}|{api._user.Password}"));
            var buff = new byte[1024];
            if (stream == null) throw new Exception("stream is null");

            if (CanRun())
            {
                AddToApi(api);
                return true;
            }
            return false;

        }

        private void SendPacket(string packet)
        {
            if(stream.CanWrite && stream.CanRead)
            {
            
                stream?.Write(Encoding.ASCII.GetBytes(packet));
            }
            else
            {
                //throw new Exception("Stream is null");
                return;
            }
        }
        private async void SendPlayerInfo()
        {
            foreach (var user in apis.ToList())
            {
                try
                {
                    var userJson = user._user;
                    var str = Newtonsoft.Json.JsonConvert.SerializeObject(userJson);
                    SendPacket($"004|{user._user.Name}|{str}");

                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex);
                    

                }
            }


        }





        public bool Login(string username, string password)
        {

            LoggedAs.LoggedAS = username;
            LoggedAs.HashedPassword = password;
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(ConstMembers.RemoteServer, ConstMembers.Port);
            }
            catch (Exception ex)
            {
                
            }
            if (tcpClient.Connected)
            {
                stream = tcpClient.GetStream();
            }
            if (stream != null)
            {
                listener = new Thread(new ThreadStart(ListenForPackets));

                listener.IsBackground = true;
                listener.Start();

                var packet = Packet.CreatePacket("login", $"{username}|{password}");
                stream.Write(Encoding.ASCII.GetBytes(packet));
                Task.Run(async () => await Ping());
                Task.Run(async () => await SendUsers());


                if (CanRun())
                {

                    Console.WriteLine(Response);
                    onLogin?.Invoke(this, this);
                    return true;
                }
                else
                {
                    tcpClient.Client.Disconnect(false);
                    tcpClient = null;
                    return false;
                }

            }
            return false;


        }

    }
}
