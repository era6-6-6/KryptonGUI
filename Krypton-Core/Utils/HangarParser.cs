using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Krypton_Core.Utils
{
    public class HangarParser
    {

        public List<Ship> Ships = new List<Ship>();

        public HangarParser(string input)
        {
            var match = Regex.Match(input, "{\"lootId\":\"(.*?)\",\"owned\":(.*?),\"fav\":(.*?),\"price\":(.*?),\"hangarId\":(.*?),\"factionBased\":(.*?),\"eventItem\":(.*?),\"eventActive\":(.*?),\"remodel\":(.*?)}");
            while(match.Success)
            {
                if (match.Groups[2].Value != "1")
                {
                    match = match.NextMatch();
                    continue;
                }
                Console.WriteLine("Ship: " + match.Groups[1].Value + "HID: " + match.Groups[5].Value);
                Ships.Add(new Ship(match.Groups[1].Value.Replace("ship_", ""), int.Parse(match.Groups[5].Value.Replace("\"",""))));
                match = match.NextMatch();
            }
           

           
        }
    }

    public class Ship
    {
        public string? Name { get; set; }
        public int HangarID { get; set; }

        public Ship(string name , int hangarId)
        {
            Name = name;
            HangarID = HangarID;
        }
    }

}
