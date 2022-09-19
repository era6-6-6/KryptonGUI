using Krypton_Core.Collections.Collectables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.TimeManagers
{
    public class MemorizedBoxManager 
    {
        User user { get; set; }
        private List<BoxHandler> boxes = new();
        private int Seconds = 10;
        public MemorizedBoxManager(Api api)
        {
            user = api._user;
            Task.Run(async () => await HandleTick());
        }
        
        public void Add(Box box)
        {
            BoxHandler handle = new BoxHandler(box, DateTime.Now);
            boxes.Add(handle);
                                
            
        }
        //Don't touch this!!!!!
        private async Task HandleTick()
        {
            while(true)
            {
                try
                {
                    await Task.Delay(4000);
                    foreach (var box in boxes.ToList())
                    {
                        if (box.Added.AddSeconds(10) < DateTime.Now)
                        {
                            user.Boxes.MemorizedBoxes.Remove(box.Box);
                            boxes.Remove(box);
                            box.Dispose();
                            continue;
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public void TryToRemoveBeforeTick(Box box)
        {
            try
            {
                lock (boxes)
                {
                    var RemoveBox = boxes.Find(x => x.Box == box);
                    _ = boxes.Remove(RemoveBox);
                    
                }
            }            
            catch (Exception ex)
            {

            }
               
        }
        
       

       
    }
    public class BoxHandler : IDisposable
    {
        public Box? Box { get; set; }
        public DateTime Added { get; set; }
        
        public BoxHandler(Box box , DateTime time)
        {
            Box = box;
            Added = time;
        }

        public void Dispose()
        {
            Box = null;
            GC.Collect();
        }
    }
}
