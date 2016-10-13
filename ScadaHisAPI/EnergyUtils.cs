using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class EnergyUtils
    {
        public static List<DataPoint> EnergyHistory(DateTime start, DateTime end, string[] FactoryList)
        {
            string[] TagNameList = FactoryToPowerTag(FactoryList, ".1H");

            return ScadaHisDao.AnalogSummaryFullRetrieval(start, end, TagNameList).ToList();
        }

        public static List<DataPoint> PowerHistory30M(DateTime start, DateTime end, string[] FactoryList)
        {
            string[] TagNameList = FactoryToPowerTag(FactoryList, ".30M");

            return ScadaHisDao.AnalogSummaryFullRetrieval(start, end, TagNameList).ToList();
        }

        public static double? GetEnergyByHour(IEnumerable<DataPoint> list, DateTime time, string FactoryName)
        {
            var data = (from p in list where (p.TagName.ToUpper().Contains(FactoryName.ToUpper()) && p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalHours < 1 && p.Value.HasValue) select (double)p.Value);

            if (data.Count<double>() == 0) return null;

            return Math.Round(data.Sum(), 2);
        }

        public static double? GetTotalEnergyByHour(IEnumerable<DataPoint> list, DateTime time)
        {
            var data = (from p in list where (p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalHours < 1 && p.Value.HasValue) select (double)p.Value);

            if (data.Count<double>() == 0) return null;

            return Math.Round(data.Sum(), 2);
        }

        public static double? GetEnergyByDay(IEnumerable<DataPoint> list, DateTime time, string FactoryName)
        {
            var data = (from p in list where (p.TagName.ToUpper().Contains(FactoryName.ToUpper()) && p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalDays < 1 && p.Value.HasValue) select (double)p.Value);

            if (data.Count<double>() == 0) return null;

            return Math.Round(data.Sum(), 2);
        }

        public static double? GetTotalEnergyByDay(IEnumerable<DataPoint> list, DateTime time)
        {
            var data = (from p in list where (p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalDays < 1 && p.Value.HasValue) select (double)p.Value);

            if (data.Count<double>() == 0) return null;

            return Math.Round(data.Sum(), 2);
        }

        public static string[] FactoryToPowerTag(string[] FactoryList, string strTagTime)
        {
            List<string> TagNameList = new List<string>();

            foreach (string factory in FactoryList)
            {
                if (factory != null)
                {
                    foreach (string tag in _Define.PowerTagNames)
                    {
                        if (tag.ToUpper().Contains(factory.ToUpper()))
                        {
                            TagNameList.Add(tag + strTagTime);
                        }
                    }
                }
            }

            return TagNameList.ToArray();
        }


    }
}