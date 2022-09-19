using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Items
{
    public class Booster
    {
        public string BoosterType { get; set; }
        public string lootID { get; set; }
        public double Value { get; set; }
        public int TimeInSeconds { get; set; }
        public string TimeInString { get; set; } = "inicializing";


      

        public void StartCounter()
        {
            Task.Run(async
                () => await CountTime());
        }


        private async Task CountTime()
        {
            while (TimeInSeconds != 0)
            {
                TimeInSeconds--;
                await Task.Delay(1000);
                
                TimeInString = TimeSpan.FromSeconds(TimeInSeconds).Hours +":" + $"{TimeSpan.FromSeconds(TimeInSeconds).Minutes:N00}" + $":{TimeSpan.FromSeconds(TimeInSeconds).Seconds:N00}";}
            }
        }

}
