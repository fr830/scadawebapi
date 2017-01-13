using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScadaHisAPI.Defines;

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
                    if (String.Compare(factory.ToUpper(), _Define.MongDuong1_ID) == 0)
                    {
                        foreach (string tag in MongDuong1Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.VinhTan2_ID) == 0)
                    {
                        foreach (string tag in VinhTan2Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.PhuMy1_ID) == 0)
                    {
                        foreach (string tag in PhuMy1Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.PhuMy21_ID) == 0)
                    {
                        foreach (string tag in PhuMy21Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.PhuMy22_ID) == 0)
                    {
                        foreach (string tag in PhuMy22Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.PhuMy3_ID) == 0)
                    {
                        foreach (string tag in PhuMy3Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.PhuMy4_ID) == 0)
                    {
                        foreach (string tag in PhuMy4Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.BaRia_ID) == 0)
                    {
                        foreach (string tag in BaRiaDefine.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.BuonKuop_ID) == 0)
                    {
                        foreach (string tag in BuonKuopDefine.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.BuonTuaShar_ID) == 0)
                    {
                        foreach (string tag in BuonTuaSharDefine.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }

                    else if (String.Compare(factory.ToUpper(), _Define.Srepok3_ID) == 0)
                    {
                        foreach (string tag in Srepok3Define.PowerTagNames)
                            TagNameList.Add(tag + strTagTime);
                    }
                }
            }

            return TagNameList.ToArray();
        }
    }
}