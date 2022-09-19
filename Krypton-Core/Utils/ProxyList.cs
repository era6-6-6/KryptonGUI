using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Utils
{
    public class ProxyList
    {
        /// <summary>
        /// Retrieve a proxy from the list
        /// </summary>
        /// <returns>One proxy from the list.</returns>
        public static Tuple<string, int, string, string> ReturnOneProxy()
        {
            var tupleList = new List<Tuple<string, int, string, string>>
            {
                //RA
                new("144.168.217.89"  ,  8781 , "xscuzuek" , "xn5nyjyddfn5"),
                new("144.168.217.244"  ,  8936 , "xscuzuek" , "xn5nyjyddfn5"),
                new("185.199.228.220"  ,  7300 , "xscuzuek" , "xn5nyjyddfn5"),
                new("185.199.231.45"  ,     8382 , "xscuzuek" , "xn5nyjyddfn5"),
                new("144.168.217.164"  ,    8856 , "xscuzuek" , "xn5nyjyddfn5"),
                new("144.168.217.134"  ,  8826 , "xscuzuek" , "xn5nyjyddfn5"),
                new("144.168.217.88"  ,  8780 , "xscuzuek" , "xn5nyjyddfn5"),
                new("144.168.217.201"  ,  8893 , "xscuzuek" , "xn5nyjyddfn5"),
                new("144.168.217.122"  ,  8814 , "xscuzuek" , "xn5nyjyddfn5"),
                

            };

            Random rnd = new Random();
            int returnNumber = rnd.Next(tupleList.Count);
            return tupleList[returnNumber];
        }

    }
}
