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
 
        public static double? GetHourlyValues(string TagName, IEnumerable<DataPoint> datalist)
        {
            if (TagName == null || datalist.Count() == 0) return null;

            return (from p in datalist where p.TagName == TagName select (p.Value != null ? (double?)Math.Round((double)p.Value, 2) : 0)).FirstOrDefault();
        }
    }
}