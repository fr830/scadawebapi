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

        public static List<SumTagName> SearchSumTagname(string[] TagnameList)
        {
            List<SumTagName> result = new List<SumTagName>();
            char sumChar = '+';
            char spaceChar = ' ';

            foreach (string tag in TagnameList)
            {
                if (tag != null && tag.Contains(sumChar))
                {
                    SumTagName sumTag = new SumTagName(tag);
                    sumTag.SumList = new List<string>(tag.Split(new char[]{sumChar, spaceChar}, StringSplitOptions.RemoveEmptyEntries));
                    result.Add(sumTag);
                }
            }

            return result;
        }

        public static DataPoint DataPointSum(IEnumerable<DataPoint> datalist, string[] TagNameList, string NewTagName)
        {
            DataPoint sum = new DataPoint() { DateTime = DateTime.Now, TagName = NewTagName, Value = null, OPCQuality = 0 };

            var data = (from p in datalist where TagNameList.Contains(p.TagName) select p);

            if (data.Count<DataPoint>() > 0)
            {
                sum.DateTime = data.FirstOrDefault().DateTime;
                sum.Value = (from p in datalist where  p.Value.HasValue select p.Value).Sum();
                sum.OPCQuality = data.FirstOrDefault().OPCQuality;
            }

            return sum;
        }
    }
}