using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.BackPage.Data.enumAtributes
{
    public class FullNameAttribute : System.Attribute
    {
        public string FullName { get; set; }

        public FullNameAttribute(string fullName)
        {
            FullName = fullName;
        }

    }
}
