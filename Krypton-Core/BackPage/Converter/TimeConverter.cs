﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.BackPage.Converter
{
    public static class TimeConverter
    {
        public static DateTime TimestampToDate(this int unixTimeStamp)
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time = time.AddSeconds(unixTimeStamp).ToLocalTime();
            return time;
        }

        public static string FormatReadable(this TimeSpan ts)
        {
            return $"{(int)ts.TotalHours:00}:{ts.Minutes:00}:{ts.Seconds:00}";
        }
    }
}
