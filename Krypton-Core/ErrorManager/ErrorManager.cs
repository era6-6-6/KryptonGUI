using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.ErrorManager
{
    public class ErrorManager
    {
        public string Message { get; set; }
        public bool Fatal { get; set; } = true;
        public bool UseWindow { get; set; } = true;

        public ErrorManager(string message, bool? fatal = null, bool? userUseWindow = null)
        {
            if (fatal != null)
            {
                Fatal = (bool)fatal;
            }

            if (userUseWindow != null)
            {
                UseWindow = (bool)userUseWindow;
            }

            Message = message;
        }


    }
}
