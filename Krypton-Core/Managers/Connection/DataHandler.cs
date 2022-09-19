using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Managers.Connection
{
    public class DataHandler 
    {
        
        private Api api;
        NetworkStream stream { get; init; }
        TcpClient client;
        public DataHandler(NetworkStream ns, TcpClient cl , Api api)
        {
            this.api = api;
            stream = ns;
            client = cl;
            
        }
        public static int Count = 0;

      

    }
}
