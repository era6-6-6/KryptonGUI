using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Managers
{
    internal class VersionManager
    {
        public string? ActualVersion = "375ffec195258a143cf77813ba3965b0";
        public bool IsValid = false;

        public VersionManager(string version)
        {
            //LoadActualVersion();
            //if(version == ActualVersion)
            //{
            //    IsValid = true;
            //}
            //else
            //{
            //    IsValid = false;
            //    AutoUpdate(version);
            //}
        }
        public void AutoUpdate(string version)
        {
            if (!File.Exists($"{Environment.CurrentDirectory}/version.txt"))
            {
                var file = File.Create($"{Environment.CurrentDirectory}/version.txt");
                file.Close();
            }
            using (StreamWriter wr = File.CreateText($"{Environment.CurrentDirectory}/version.txt"))
            {
                System.Console.WriteLine("[Version] AutoUpdate Done");
                wr.Write(version);
            }
        }

        public void LoadActualVersion()
        {
            try
            {
                using (StreamReader read = File.OpenText($"{Environment.CurrentDirectory}/version.txt"))
                {
                    if (read.ReadLine() != null)
                    {
                        string? v = read.ReadLine();
                        ActualVersion = v;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
