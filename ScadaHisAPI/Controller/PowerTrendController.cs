using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace ScadaHisAPI
{
    public class PowerTrendController : ApiController
    {
        // POST api/PowerTrend/{start=start}/{end=end}
        [Route("PowerTrend/{start=start}/{end=end}")]
        [HttpPost]
        public IEnumerable<DataPoint> PowerTrend([FromUri]DateTime start, [FromUri]DateTime end, [FromBody]string[] FactoryList)
        {
            try
            {
                string[] powerTagnames = Utils.GetAllPowerTagnames(FactoryList).Select(x => x + ".30M").ToArray();

                var q = ScadaHisDao.AnalogSummaryFullRetrieval(start, end, powerTagnames);

                /* group by DateTime and sum = Ptotal */
                List<DataPoint> data = (from p in q where (p.Value.HasValue) select p)
                    .OrderBy(x => x.DateTime)
                    .GroupBy(g => g.DateTime, v => v.Value)
                    .Select(g => new DataPoint
                    {
                        DateTime = g.Key,
                        TagName = "PTOTAL",
                        Value = g.Sum()
                    }).ToList();

                /* remove the last Item to prevent rapidly drop of the trend */
                if (data.Count > 0)
                    data.Remove(data.LastOrDefault());

                return data;
            }
            catch { return new List<DataPoint>(); }
        }

        // POST api/PowerTrend/ThisWeek/{now=now}
        [Route("PowerTrend/ThisWeek/{now=now}")]
        [HttpPost]
        public IEnumerable<DataPoint> ThisWeek([FromUri]DateTime now, [FromBody]string[] FactoryList)
        {
            DateTime start = Utils.GetThisMonday(now);

            return PowerTrend(start, now, FactoryList);
        }

        // GET "api/Trend/U/{FactoryName}/{start=start}/{end=end}"
        [Route("Trend/U/{FactoryName}/{start=start}/{end=end}")]
        [HttpGet]
        public IEnumerable<DataPoint> TrendU(string FactoryName, DateTime start, DateTime end)
        {
            try
            {
                List<string> taglist = XMLConfig.GetUList(FactoryName);

                List<DataPoint> result = new List<DataPoint>();

                if (taglist.Count > 0)
                {
                    result = ScadaHisDao.AnalogHistoryCyclic2(start, end, taglist.ToArray(), ScadaHisDao.CycleTime.HalfHour, ScadaHisDao.SummaryType.Average).ToList();
                }

                return result;
            }
            catch { return new List<DataPoint>(); }
        }

        // GET "api/Trend/I/{FactoryName}/{start=start}/{end=end}"
        [Route("Trend/I/{FactoryName}/{start=start}/{end=end}")]
        [HttpGet]
        public IEnumerable<DataPoint> TrendI(string FactoryName, DateTime start, DateTime end)
        {
            try
            {
                List<string> taglist = XMLConfig.GetIList(FactoryName);

                List<DataPoint> result = new List<DataPoint>();

                if (taglist.Count > 0)
                {
                    result = ScadaHisDao.AnalogHistoryCyclic2(start, end, taglist.ToArray(), ScadaHisDao.CycleTime.HalfHour, ScadaHisDao.SummaryType.Average).ToList();
                }

                return result;
            }
            catch { return new List<DataPoint>(); }
        }

        // GET "api/Trend/P/{FactoryName}/{start=start}/{end=end}"
        [Route("Trend/P/{FactoryName}/{start=start}/{end=end}")]
        [HttpGet]
        public IEnumerable<DataPoint> TrendP(string FactoryName, DateTime start, DateTime end)
        {
            try
            {
                List<string> taglist = XMLConfig.GetPList(FactoryName);

                List<DataPoint> result = new List<DataPoint>();

                if (taglist.Count > 0)
                {
                    result = ScadaHisDao.AnalogHistoryCyclic2(start, end, taglist.ToArray(), ScadaHisDao.CycleTime.HalfHour, ScadaHisDao.SummaryType.Average).ToList();
                }

                return result;
            }
            catch { return new List<DataPoint>(); }
        }

        // GET "api/GeneratorCount/{FactoryName}"
        [Route("GeneratorCount/{FactoryName}")]
        [HttpGet]
        public IEnumerable<string> GeneratorCount(string FactoryName)
        {
            try
            {
                List<OpReportGeneratorTagNames> genList = XMLConfig.GetGeneratorTagNames(FactoryName);
                List<string> result = new List<string>();

                if (genList.Count > 0)
                {
                    foreach (OpReportGeneratorTagNames item in genList)
                    {
                        result.Add(item.Name);
                    }
                }

                return result;
            }
            catch { return null; }
        }
    }
}