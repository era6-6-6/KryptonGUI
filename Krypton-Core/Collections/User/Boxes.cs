using Krypton_Core.Collections.Collectables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.User
{
    public class Boxes
    {
        public List<Box> CloseBoxes = new List<Box>();
        

        public List<Box> MemorizedBoxes = new List<Box>();

        public void Clear()
        {
           
            lock (CloseBoxes)
            {
                CloseBoxes.Clear();
            }
            lock (MemorizedBoxes)
            {
                MemorizedBoxes.Clear();
            }
        }
        public void AddMemorizedBox(string hash)
        {
            lock(CloseBoxes)
            {
                var box = CloseBoxes.Find(x => x.Hash == hash);
                if(box == null)
                {
                    return;
                }
                else
                {
                    lock(MemorizedBoxes)
                    {
                        MemorizedBoxes.Add(box);
                        CloseBoxes.Remove(box);
                    }
                }
                    
            }
        }
        public void ConvertMemBoxToClose(string hash)
        {
            lock (MemorizedBoxes)
            {
                foreach (Box box in MemorizedBoxes)
                {
                    if (box.Hash == hash)
                    {
                        lock (CloseBoxes)
                        {
                            CloseBoxes.Add(box);
                        }
                        MemorizedBoxes.Remove(box);
                        break;
                    }
                }
            }
        }
        
            
        public void Add(Box box)
        {
            lock (CloseBoxes)
            {
                CloseBoxes.Add(box);
            }
        }
        public void Remove(Box box)
        {
            lock (CloseBoxes)
            {
                CloseBoxes.Remove(box);   
            }
        }
        public void Remove(string hash)
        {
            
            var box = CloseBoxes.Find(x => x.Hash == hash);
            
            if(box != null)
            {
                lock(CloseBoxes)
                {
                    CloseBoxes.Remove(box);
                }
            }
        }
        
        
    }
}
