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
        // POST api/EnergyHistory/hourly/{start=start}/{end=end}
        [Route("EnergyHistory/hourly/{start=start}/{end=end}")]
        [HttpPost]
        public /*IEnumerable<DataPoint>*/ IHttpActionResult Hourly([FromUri]DateTime start, [FromUri]DateTime end, [FromBody]string[] FactoryList)
        {
            try
            {
                var energy = EnergyUtils.EnergyHistory(start, end, FactoryList);

                List<DataPoint> result = new List<DataPoint>();

                DateTime time = new DateTime(start.Year, start.Month, start.Day);

                do
                {
                    foreach (string factory in FactoryList)
                    {
                        if (factory != null)
                        {
                            result.Add(new DataPoint { DateTime = time, TagName = factory, Value = EnergyUtils.GetEnergyByHour(energy, time, factory) });
                        }
                    }

                    time = time.AddHours(1);

                } while (time.CompareTo(end) < 0);

                return Ok(result);
            }
            catch { return Ok(new List<DataPoint>()); }
        }

        // POST api/EnergyHistory/hourlySummary/{start=start}/{end=end}
        [Route("EnergyHistory/hourlySummary/{start=start}/{end=end}")]
        [HttpPost]
        public IEnumerable<DataPoint> HourlySummary([FromUri]DateTime start, [FromUri]DateTime end, [FromBody]string[] FactoryList)
        {
            try
            {
                var energy = EnergyUtils.EnergyHistory(start, end, FactoryList);

                List<DataPoint> result = new List<DataPoint>();

                DateTime time = new DateTime(start.Year, start.Month, start.Day);

                do
                {
                    // Total Energy per Hour
                    result.Add(new DataPoint { DateTime = time, TagName = "ETOTAL", Value = EnergyUtils.GetTotalEnergyByHour(energy, time) });

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
                var energy = EnergyUtils.EnergyHistory(start, end, FactoryList);

                List<DataPoint> result = new List<DataPoint>();

                DateTime time = new DateTime(start.Year, start.Month, start.Day);

                do
                {
                    foreach (string factory in FactoryList)
                    {
                        if (factory != null)
                        {
                            result.Add(new DataPoint { DateTime = time, TagName = factory, Value = EnergyUtils.GetEnergyByDay(energy, time, factory) });
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
                var energy = EnergyUtils.EnergyHistory(start, end, FactoryList);

                List<DataPoint> result = new List<DataPoint>();

                DateTime time = new DateTime(start.Year, start.Month, start.Day);

                do
                {
                    // Total Energy per Day
                    result.Add(new DataPoint { DateTime = time, TagName = "ETOTAL", Value = EnergyUtils.GetTotalEnergyByDay(energy, time) });

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