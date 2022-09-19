using Krypton_Core.Packets.Bytes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Commands.Read;

    public class Message
    {
        public string HpMessage { get; set; }
        public Message(EndianBinaryReader param1)
        {
            HpMessage =  param1.ReadString();
            
        }

    }

