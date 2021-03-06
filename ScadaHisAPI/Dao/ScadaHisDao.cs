﻿using System;
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

        static HisRuntimeEntities db = new HisRuntimeEntities();

        public static IEnumerable<DataPoint> AnalogLive(string[] TagNameList)
        {
            try
            {
                DateTime now = DateTime.Now;

                var q = (from p in db.AnalogLives
                         where TagNameList.Contains(p.TagName)
                         select new DataPoint
                         {
                             DateTime = p.DateTime,
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
            try
            {
                /* workaround: to prevent invalid Tagname request to server */
                string str_null = "";

                var q = (from p in db.AnalogSummaryHistories
                                     where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.StartDateTime >= start && p.EndDateTime <= end && p.OPCQuality >= 192
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
            string full_mode = "Full";

            /* workaround: to prevent invalid Tagname request to server */
            string str_null = "";

            try
            {
                var q = (from p in db.AnalogSummaryHistories
                         where (p.TagName == str_null || TagNameList.Contains(p.TagName)) && p.StartDateTime >= start && p.EndDateTime <= end && p.wwRetrievalMode == full_mode && p.OPCQuality >= 192
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