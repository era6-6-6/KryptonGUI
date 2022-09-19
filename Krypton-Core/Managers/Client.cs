
using Krypton_Core.Commands.Write;
using Krypton_Core.Managers.Connection;
using Krypton_Core.Packets.Bytes;
using Managers;
using System.Diagnostics;
using System.Drawing.Text;
using System.Net.Sockets;


namespace Krypton_Core.Managers
{
    public abstract class Client
    {
        public bool RunningStream { get; set; }
        public object? locker = new object();
        public Api? Api { get; private set; }
        public bool? Running { get; set; } = true;
        public TcpClient? tcpClient { get; private set; }
        public NetworkStream? stream { get; set; }
        public string? IP { get; private set; }
        public int Port { get; private set; }
        public EventHandler<EventArgs>? OnConnected;
        public EventHandler<EventArgs>? Disconnected;

        public Client(Api api)
        {
            this.Api = api;
        }



        //bot connect to game server
        public void Connect(string ip, int port)
        {
            this.IP = ip;
            this.Port = port;
            

            tcpClient = new TcpClient();
            
            try
            {
                CrashLogManager.Send(Api?._user.logMsg, $"Connecting {Api?._user.Name} to the spacemap...", CrashLogManager.Type.MESSAGE);
                tcpClient.Connect(this.IP, this.Port);
            }
            catch (SocketException e)
            {
                Thread.Sleep(5000);
                Console.WriteLine(e);
                Disconnected?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (tcpClient.Connected)
            {
                stream = tcpClient.GetStream();
                Task.Run(async () => await Run());
                RunningStream = true;

                OnConnected?.Invoke(this, EventArgs.Empty);
                
            }
        }
        public async Task Disconnect()
        {
            try
            {
                tcpClient?.Client.DisconnectAsync(true);
                tcpClient?.Client.Close();
                tcpClient?.Close();
                stream?.Close();
                await Task.Delay(10);
            }
            catch (Exception e)
            {
                
                
            }
            
        }
        public async void Reconnect()
        {
            await Task.Delay(1000);
            if(!Api._user.events.ReloadComplete) return;
            try
            {
                Api._user.events.ReloadComplete = false;
                await Disconnect();
                Api?.CleanLists();
                Connect(Api?._user.userData.MapIP, 5001);
                Send(new VersionRequest(""));

                Send(new Login(Api._user.userData.UserID, Api._user.userData.InstanceId, "", Api._user.userData.SID, 0));
                Api._user.events.ReloadComplete = true;

            }
            catch(Exception ex)
            {
                Api.InvokeError(ex.ToString(), false, false);
                Console.WriteLine(ex);
            }





        }
        public void Send(Command command)
        {
            Send(command.param1.Message.ToArray());

        }

        public void Send(byte[] buffer)
        {
            if(Api._user.LogoutInvoked) return;
            if (!RunningStream) return;
            try
            {
                stream?.Write(buffer, 0, buffer.Length);
                stream?.Flush();
            }
            catch(Exception ex)
            {
                Api.InvokeError(ex.ToString(), false, false);
                Console.WriteLine(ex);
                

            }

        }

        protected async Task Run()
        {
            try
            {
                while (IsConnected())
                {
                    if (stream.DataAvailable)
                    {
                        ParsePackets();
                    }
                }
                Disconnected?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Api.InvokeError(ex.ToString(), false, false);
                RunningStream = false;
            }

        }

        private void ParsePackets()
        {
            
            try
            {
                if (stream.Socket.Available > 3)
                {
                    var buffer = new byte[3];
                    stream.Read(buffer, 0, 3);
                    var reader = new EndianBinaryReader(EndianBitConverter.Big, new MemoryStream(buffer));

                    int streamLength = (int)((uint)reader.ReadByte() << 16 | (ushort)reader.ReadInt16());
                    if (streamLength > 0)
                    {
                        buffer = new byte[streamLength];
                        int offset = 0, rcvd = 0;
                        while (offset + 1 < streamLength)
                        {
                            if (streamLength > 80_000)
                            {
                                rcvd = stream.Read(buffer, offset, streamLength);
                                offset += rcvd;
                            }
                            else
                            {
                                rcvd = stream.Read(buffer, offset, streamLength - offset);
                                offset += rcvd;
                            }
                           Console.WriteLine("stream length : " + streamLength);
                        }
                        Parse(new EndianBinaryReader(EndianBitConverter.Big, new MemoryStream(buffer)));
                    }
                }
            }
            catch (Exception e)
            {
                return;
                
            }
            
        }




        protected bool IsConnected()
        {
            if (tcpClient == null || tcpClient.Client == null || !stream.CanWrite || !stream.CanRead)
                return false;
            try
            {
                return !(tcpClient.Client.Poll(1, SelectMode.SelectRead) && tcpClient.Client.Available == 0);

            }
            catch (SocketException) { return false; }
        }

        public abstract void Parse(EndianBinaryReader reader);
    }
}