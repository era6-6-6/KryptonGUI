using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Managers.Internal
{
    public class LogManager
    {
        public DateTime Time { get; private set; }
        public string Message { get; private set; }

        enum Type
        {
            ERROR , INFO , WARNING
        }

        public LogManager(string message)
        {
            AsighMessage(message);
        }
        private void AsighMessage(string message)
        {
            Time = DateTime.Now;
            Message = message;
        }
    }
}
