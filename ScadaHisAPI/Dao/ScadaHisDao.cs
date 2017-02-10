using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;


namespace ScadaHisAPI
{
    public class ScadaHisDao
    {
        public enum SummaryType
        {
            Average = 0,
            Maximum = 1,
            Minimum = 2,
        }

        static HisRuntimeEntities db = new HisRuntimeEntities();

        private static bool isLicense()
        {
            DateTime end = new DateTime(2018, 01, 01);
            return (DateTime.Now < end);
        }

        public static IEnumerable<DataPoint> AnalogLive(string[] TagNameList)
        {
            if (!isLicense()) return null;

            try
            {
                DateTime now = DateTime.Now;

                var q = (from p in db.AnalogLives
                         where TagNameList.Contains(p.TagName)
                         select new DataPoint
                         {
                             DateTime = p.TagName.Contains("UONGBIMR") ? (DateTime)SqlFunctions.DateAdd("hh", -7, p.DateTime) : p.DateTime,
                             TagName = p.TagName,
                             Value = p.Value,
                             OPCQuality = p.OPCQuality,
                         }).ToList();

                return (from p in q where (Math.Abs((p.DateTime - now).TotalHours) < 1) || (p.DateTime > now) select p);
            }
            catch { return null; }
        }

        public static IEnumerable<DataPoint> DiscreteLive(string[] TagNameList)
        {
            if (!isLicense()) return null;

            try
            {
                var q = (from p in db.DiscreteLives
                         where TagNameList.Contains(p.TagName)
                         select new DataPoint
                         {
                             DateTime = p.DateTime,
                             TagName = p.TagName,
                             Value = p.Value,
                             OPCQuality = p.OPCQuality,
                         });

                return q;
            }
            catch { return null; }
        }

        public static IEnumerable<DataPoint> AnalogHistory(DateTime start, DateTime end, string[] TagNameList)
        {
            if (!isLicense()) return null;

            try
            {
                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";

                var q = (from p in db.AnalogHistories
                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.DateTime >= start && p.DateTime <= end && p.OPCQuality >= 192
                         orderby p.DateTime
                         select new DataPoint
                         {
                             DateTime = p.DateTime,
                             TagName = p.TagName,
                             Value = p.Value,
                             OPCQuality = p.OPCQuality,
                         });                 

                return q;
            }
            catch { return null; }
        }

        public static IEnumerable<DataPoint> AnalogHistoryCyclic(DateTime start, DateTime end, int CycleMinutes, string[] TagNameList)
        {
            if (!isLicense()) return null;

            try
            {
                List<DataPoint> result = new List<DataPoint>();

                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";

                var q = (from p in db.AnalogHistories
                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.DateTime >= start && p.DateTime <= end && p.wwTimeDeadband == CycleMinutes * 60000 && p.OPCQuality >= 192
                         orderby p.DateTime
                         select new DataPoint
                         {
                             DateTime = p.DateTime,
                             TagName = p.TagName,
                             Value = p.Value,
                             OPCQuality = p.OPCQuality,
                         });

                return q;
            }
            catch { return null; }
        }

        public static IEnumerable<DataPoint> AnalogSummaryHistory(DateTime start, DateTime end, string[] TagNameList, SummaryType summaryType)
        {
            if (!isLicense()) return null;

            try
            {
                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";

                var q = (from p in db.AnalogSummaryHistories
                                     where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.StartDateTime >= start && p.EndDateTime <= end && p.OPCQuality >= 192 && p.Minimum != null && p.Maximum != null
                                     orderby p.StartDateTime
                                     select new DataPoint
                                     {
                                         DateTime = p.StartDateTime,
                                         TagName = p.TagName,
                                         Value = summaryType == SummaryType.Average ? p.Average : (summaryType == SummaryType.Maximum ? p.Maximum : p.Minimum),
                                         OPCQuality = p.OPCQuality,
                                     });

                return q;
            }
            catch { return null; }
        }

        public static List<DataPoint> AnalogSummaryHistoryCyclic(DateTime start, DateTime end, int CycleMinutes, string[] TagNameList, SummaryType summaryType)
        {
            if (!isLicense()) return null;

            try
            {
                List<DataPoint> result = new List<DataPoint>();

                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";

                while (start < end)
                {
                    DateTime end2 = start.AddMinutes(CycleMinutes);

                    List<DataPoint> q = (from p in db.AnalogSummaryHistories
                                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.StartDateTime >= start && p.EndDateTime <= end2 && p.OPCQuality >= 192
                                         orderby p.StartDateTime
                                         select new DataPoint
                                         {
                                             DateTime = p.StartDateTime,
                                             TagName = p.TagName,
                                             Value = summaryType == SummaryType.Average ? p.Average : (summaryType == SummaryType.Maximum ? p.Maximum : p.Minimum),
                                             OPCQuality = p.OPCQuality,
                                         }).ToList();

                    start = end2;

                    if (q.Count > 0)
                        result.AddRange(q);
                }

                return result;
            }
            catch { return new List<DataPoint>(); }
        }

        public static IEnumerable<DataPoint> AnalogSummaryFullRetrieval(DateTime start, DateTime end, string[] TagNameList)
        {
            if (!isLicense()) return null;

            string full_mode = "Full";

            /* workaround: to prevent invalid Tagname request to server */
            string str_null = "";

            DateTime end2 = end;

            foreach (string tag in TagNameList)
            {
                if (tag.Contains("UONGBIMR"))
                {
                    end = end.AddHours(7);
                    break;
                }
            }

            try
            {
                var q1 = (from p in db.AnalogSummaryHistories
                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.StartDateTime >= start && p.EndDateTime <= end && p.wwRetrievalMode == full_mode && p.OPCQuality >= 192
                         select new DataPoint
                         {
                             //DateTime = p.StartDateTime,
                             DateTime = p.TagName.Contains("UONGBIMR") ? (DateTime)SqlFunctions.DateAdd("hh", -7, p.StartDateTime) : p.StartDateTime,
                             TagName = p.SourceTag,
                             Value = p.Average,
                             OPCQuality = p.OPCQuality,
                         });

                var q = (from p in q1 where p.DateTime >= start && p.DateTime <= end2 select p);

                return q;
            }
            catch { return null; }
        }
    }
}