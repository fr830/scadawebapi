using System;
using System.Collections.Generic;
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

        public enum CycleTime
        {
            OneHour = 0,
            HalfHour = 1,
        }

        static HisRuntimeEntities db = new HisRuntimeEntities();

        public static IEnumerable<DataPoint> AnalogLive(string[] TagNameList)
        {
            try
            {
                //DateTime now = DateTime.Now;

                int OPCQualityAcceptable = XMLConfig.AnalogLiveQuality();

                var q = (from p in db.AnalogLives
                         where TagNameList.Contains(p.TagName) && p.OPCQuality >= OPCQualityAcceptable
                         select new DataPoint
                         {
                             DateTime = p.DateTime,
                             TagName = p.TagName,
                             Value = p.Value,
                             OPCQuality = p.OPCQuality,
                         }).ToList();

                return q;// (from p in q where (Math.Abs((p.DateTime - now).TotalHours) < 1) || (p.DateTime > now) select p);
            }
            catch { return null; }
        }

        public static IEnumerable<DataPoint> DiscreteLive(string[] TagNameList)
        {
            try
            {
                int OPCQualityAcceptable = XMLConfig.DiscreteLiveQuality();

                var q = (from p in db.DiscreteLives
                         where TagNameList.Contains(p.TagName) && p.OPCQuality >= OPCQualityAcceptable
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
            try
            {
                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";
                int OPCQualityAcceptable = XMLConfig.AnalogHistoryQuality();

                var q = (from p in db.AnalogHistories
                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.DateTime >= start && p.DateTime <= end && p.OPCQuality >= OPCQualityAcceptable
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
            try
            {
                //List<DataPoint> result = new List<DataPoint>();
                int OPCQualityAcceptable = XMLConfig.AnalogHistoryQuality();

                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";

                var q = (from p in db.AnalogHistories
                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.DateTime >= start && p.DateTime <= end && p.wwTimeDeadband == CycleMinutes * 60000 && p.OPCQuality >= OPCQualityAcceptable
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

        //private static string DateTime2String(DateTime dt)
        //{
        //    return dt.Month.ToString() + "-" + dt.Day.ToString() + "-" + dt.Year.ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString();
        //}

        //public static IEnumerable<DataPoint> AnalogSummaryHistoryHourly(DateTime start, DateTime end, string[] TagNameList, SummaryType summaryType)
        //{
        //    try
        //    {
        //        List<DataPoint> result = new List<DataPoint>();

        //        string strSummary = summaryType == SummaryType.Average ? "AVG(Value)" : (summaryType == SummaryType.Maximum ? "MAX(Value)" : "MIN(Value)");

        //        string querry = "Select DATEADD(hh, DATEPART(hh, DateTime), CAST(CAST(DateTime AS date) AS datetime)) AS DateTime, TagName, " + strSummary + " AS Value, AVG(OPCQuality) AS OPCQuality from AnalogHistory where TagName IN ('";

        //        foreach (string tag in TagNameList)
        //        {
        //            querry += "','" + tag;
        //        }
        //        querry += "')";

        //        querry += " and DateTime >= '" + DateTime2String(start) + "'";

        //        querry += " and DateTime <= '" + DateTime2String(end) + "'";

        //        querry += " and OPCQuality >= 192";

        //        querry += " GROUP BY DATEADD(hh, DATEPART(hh, DateTime), CAST(CAST(DateTime AS date) AS datetime)), TagName";

        //        querry += " ORDER BY DateTime ASC";

        //        List<DataPoint> q = db.Database.SqlQuery<DataPoint>(querry).ToList();

        //        result.AddRange(q);

        //        return result;
        //    }
        //    catch { return null; }
        //}

        public static IEnumerable<DataPoint> AnalogHistoryCyclic2(DateTime start, DateTime end, string[] TagNameList, CycleTime cycle, SummaryType summaryType)
        {
            try
            {
                //List<DataPoint> result = new List<DataPoint>();
                int OPCQualityAcceptable = XMLConfig.AnalogHistoryQuality();

                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";

                var first = (from p in db.AnalogHistories
                             where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.DateTime >= start && p.DateTime <= end && p.OPCQuality >= OPCQualityAcceptable
                             orderby p.DateTime
                             select new DataPoint
                             {
                                 DateTime = p.DateTime,
                                 TagName = p.TagName,
                                 Value = p.Value,
                                 OPCQuality = p.OPCQuality,
                             });

                // Group by Hour in DateTime and TagName
                var groups = first.ToList().Select(x => new DataPoint
                {
                    DateTime = (cycle == CycleTime.OneHour) ? x.DateTime.Date.AddHours(x.DateTime.Hour) : (x.DateTime.Minute < 30 ? x.DateTime.Date.AddHours(x.DateTime.Hour) : x.DateTime.Date.AddHours(x.DateTime.Hour).AddMinutes(30)),
                    TagName = x.TagName,
                    Value = x.Value,
                    OPCQuality = x.OPCQuality
                }).GroupBy(x => new {x.DateTime, x.TagName });

                var q = (from g in groups
                         select new DataPoint
                         {
                             DateTime = g.Key.DateTime,
                             TagName = g.Key.TagName,
                             Value = summaryType == SummaryType.Average ? g.Average(x => x.Value) : (summaryType == SummaryType.Maximum ? g.Max(x => x.Value) : g.Min(x => x.Value)),
                             OPCQuality = (int)g.Min(y => y.OPCQuality)
                         });

                return q;
            }
            catch { return null; }
        }

         public static IEnumerable<DataPoint> AnalogSummaryHistory(DateTime start, DateTime end, string[] TagNameList, SummaryType summaryType)
        {
            try
            {
                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";
                int OPCQualityAcceptable = XMLConfig.AnalogHistorySummaryQuality();

                var q = (from p in db.AnalogSummaryHistories
                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.StartDateTime >= start && p.EndDateTime <= end && p.OPCQuality >= OPCQualityAcceptable && p.Minimum != null && p.Maximum != null
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
            try
            {
                List<DataPoint> result = new List<DataPoint>();
                int OPCQualityAcceptable = XMLConfig.AnalogHistorySummaryQuality();

                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";

                while (start < end)
                {
                    DateTime end2 = start.AddMinutes(CycleMinutes);

                    List<DataPoint> q = (from p in db.AnalogSummaryHistories
                                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.StartDateTime >= start && p.EndDateTime <= end2 && p.OPCQuality >= OPCQualityAcceptable
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
            string full_mode = "Full";

            /* workaround: to prevent invalid Tagname request to server */
            string str_null = "";
            int OPCQualityAcceptable = XMLConfig.AnalogHistorySummaryQuality();

            try
            {
                var q = (from p in db.AnalogSummaryHistories
                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.StartDateTime >= start && p.EndDateTime <= end && p.wwRetrievalMode == full_mode && p.OPCQuality >= OPCQualityAcceptable
                         select new DataPoint
                         {
                             DateTime = p.StartDateTime,
                             TagName = p.SourceTag,
                             Value = p.Average,
                             OPCQuality = p.OPCQuality,
                         });

                return q;
            }
            catch { return null; }
        }
    }
}