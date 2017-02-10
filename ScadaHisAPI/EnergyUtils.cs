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
            string[] TagNameList = FactoryToPowerTag(new string[]{FactoryName}, null);

            var data = (from p in list where (TagNameList.Contains(p.TagName) && p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalHours < 1 && p.Value.HasValue) select (double)p.Value);

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
            string[] TagNameList = FactoryToPowerTag(new string[] { FactoryName }, null);

            var data = (from p in list where (TagNameList.Contains(p.TagName) && p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalDays < 1 && p.Value.HasValue) select (double)p.Value);

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
                    if (String.Compare(factory.ToUpper(), _Define.UongBi_ID) == 0)
                    {
                        foreach (string tag in UongBiDefine.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.BanVe_ID) == 0)
                    {
                        foreach (string tag in BanVeDefine.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.DaiNinh_ID) == 0)
                    {
                        foreach (string tag in DaiNinhDefine.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.DongNai3_ID) == 0)
                    {
                        foreach (string tag in DongNai3Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.DongNai4_ID) == 0)
                    {
                        foreach (string tag in DongNai4Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.SongTranh2_ID) == 0)
                    {
                        foreach (string tag in SongTranh2Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }
                }
            }

            return TagNameList.ToArray();
        }


    }
}