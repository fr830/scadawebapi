using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web.Http.Results;

namespace ScadaHisAPI
{
    public class EnergyHistoryController : ApiController
    {
        private IEnumerable<DataPoint> EnergyHistory(DateTime start, DateTime end, string[] FactoryList)
        {
            return ScadaHisDao.AnalogSummaryFullRetrieval(start, end, Utils.GetAllPowerTagnames(FactoryList).Select(x => x + ".1H").ToArray()).ToList();
        }

        // POST api/EnergyHistory/hourly/{start=start}/{end=end}
        [Route("EnergyHistory/hourly/{start=start}/{end=end}")]
        [HttpPost]
        public IEnumerable<DataPoint> Hourly([FromUri]DateTime start, [FromUri]DateTime end, [FromBody]string[] FactoryList)
        {
            try
            {
                var energy = EnergyHistory(start, end, FactoryList);

                List<DataPoint> result = new List<DataPoint>();

                DateTime time = start.Date;

                do
                {
                    foreach (string factory in FactoryList)
                    {
                        if (factory != null)
                        {
                            List<string> TagNameList = XMLConfig.GetEnergySources(factory);

                            var data = (from p in energy where (TagNameList.Contains(p.TagName) && p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalHours < 1 && p.Value.HasValue) select (double)p.Value);

                            if (data.Count<double>() > 0)
                                result.Add(new DataPoint { DateTime = time, TagName = factory, Value = Math.Round(data.Sum(), 2) });
                        }
                    }

                    time = time.AddHours(1);

                } while (time.CompareTo(end) < 0);

                return result;
            }
            catch { return new List<DataPoint>(); }
        }

        // POST api/EnergyHistory/hourlySummary/{start=start}/{end=end}
        [Route("EnergyHistory/hourlySummary/{start=start}/{end=end}")]
        [HttpPost]
        public IEnumerable<DataPoint> HourlySummary([FromUri]DateTime start, [FromUri]DateTime end, [FromBody]string[] FactoryList)
        {
            try
            {
                var energy = EnergyHistory(start, end, FactoryList);

                List<DataPoint> result = new List<DataPoint>();

                DateTime time = start.Date;

                do
                {
                    var data = (from p in energy where (p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalHours < 1 && p.Value.HasValue) select (double)p.Value);

                    // Total Energy per Hour
                    if (data.Count<double>() > 0)
                        result.Add(new DataPoint { DateTime = time, TagName = "ETOTAL", Value = Math.Round(data.Sum(), 2) });

                    time = time.AddHours(1);

                } while (time.CompareTo(end) < 0);

                return result;
            }
            catch { return new List<DataPoint>(); }
        }

        // POST api/EnergyHistory/daily/{start=start}/{end=end}/
        [Route("EnergyHistory/daily/{start=start}/{end=end}")]
        [HttpPost]
        public IEnumerable<DataPoint> Daily([FromUri]DateTime start, [FromUri]DateTime end, [FromBody]string[] FactoryList)
        {
            try
            {
                var energy = EnergyHistory(start, end, FactoryList);

                List<DataPoint> result = new List<DataPoint>();

                DateTime time = start.Date;

                do
                {
                    foreach (string factory in FactoryList)
                    {
                        if (factory != null)
                        {
                            List<string> TagNameList = XMLConfig.GetEnergySources(factory);

                            var data = (from p in energy where (TagNameList.Contains(p.TagName) && p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalDays < 1 && p.Value.HasValue) select (double)p.Value);

                            if (data.Count<double>() > 0)
                                result.Add(new DataPoint { DateTime = time, TagName = factory, Value = Math.Round(data.Sum(), 2) });
                        }
                    }

                    time = time.AddDays(1);

                } while (time.CompareTo(end) < 0);

                return result;
            }
            catch { return new List<DataPoint>(); }
        }

        // POST api/EnergyHistory/DailySummary/{start=start}/{end=end}/
        [Route("EnergyHistory/dailySummary/{start=start}/{end=end}")]
        [HttpPost]
        public IEnumerable<DataPoint> DailySummary([FromUri]DateTime start, [FromUri]DateTime end, [FromBody]string[] FactoryList)
        {
            try
            {
                var energy = EnergyHistory(start, end, FactoryList);

                List<DataPoint> result = new List<DataPoint>();

                DateTime time = start.Date;

                do
                {
                    var data = (from p in energy where (p.DateTime.CompareTo(time) >= 0 && (p.DateTime - time).TotalDays < 1 && p.Value.HasValue) select (double)p.Value);

                    // Total Energy per Day
                    if (data.Count<double>() > 0)
                        result.Add(new DataPoint { DateTime = time, TagName = "ETOTAL", Value = Math.Round(data.Sum(), 2) });

                    time = time.AddDays(1);

                } while (time.CompareTo(end) < 0);

                return result;
            }
            catch { return new List<DataPoint>(); }
        }


        // POST api/EnergyHistory/ThisWeek/{now=now}
        [Route("EnergyHistory/ThisWeek/{now=now}")]
        [HttpPost]
        public IEnumerable<DataPoint> ThisWeek([FromUri]DateTime now, [FromBody]string[] FactoryList)
        {
            DateTime start = Utils.GetThisMonday(now);

            return DailySummary(start, now, FactoryList);
        }

        // POST api/EnergyHistory/LastWeek/{now=now}
        [Route("EnergyHistory/LastWeek/{now=now}")]
        [HttpPost]
        public IEnumerable<DataPoint> LastWeek([FromUri]DateTime now, [FromBody]string[] FactoryList)
        {
            DateTime end = Utils.GetThisMonday(now);

            // Find the Monday of last week
            DateTime start = Utils.GetThisMonday(end.AddDays(-1));

            return DailySummary(start, end, FactoryList);
        }

        // POST api/EnergyHistory/Week2Week/{now=now}
        [Route("EnergyHistory/Week2Week/{now=now}")]
        [HttpPost]
        public IEnumerable<DataPoint> Week2Week([FromUri]DateTime now, [FromBody]string[] FactoryList)
        {
            DateTime end = Utils.GetThisMonday(now);

            // Find the Monday of last week
            DateTime start = Utils.GetThisMonday(end.AddDays(-1));

            return DailySummary(start, now, FactoryList);
        }


    }
}