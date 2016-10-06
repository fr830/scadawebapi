using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class Utils
    {
        public static DateTime GetThisMonday(DateTime time)
        {
            // Get this Monday from current date
            while (time.DayOfWeek != DayOfWeek.Monday)
            {
                time = time.AddDays(-1);
            }

            return new DateTime(time.Year, time.Month, time.Day);
        }
    }
}