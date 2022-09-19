using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Managers.Connection
{
    public class Packet : IDisposable
    {
        
        private int Length { get; set; }
        private PacketManager? PacketManager { get; init; }
        private List<byte> bytesList = new List<byte>();
        private NetworkStream stream;
        


        public Packet(int length, NetworkStream stream ,PacketManager manager) 
        {
            Length = length;
            PacketManager = manager;
            this.stream = stream;
            Read();
        }

        private void Read()
        {
            byte[] bytes = new byte[Length];
            ReadStream(stream, bytes);
            PacketManager.Parse(new EndianBinaryReader(EndianBitConverter.Big, new MemoryStream(bytes)));
            Dispose();
        }

        public void ReadStream(NetworkStream reader, byte[] data)
        {
            try
            {
                var offset = 0;
                var remaining = data.Length;
                

                while (remaining > 0)
                {
                    var read = reader.Read(data, offset, remaining);

                    if (read <= 0)
                        throw new EndOfStreamException
                            (String.Format("End of stream reached with {0} bytes left to read", remaining));
                    remaining -= read;
                    offset += read;
                }

                



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
