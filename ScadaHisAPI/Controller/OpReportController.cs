using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScadaHisAPI
{
    public class OpReportController : ApiController
    {
        // GET api/OpReport/Transformer/{FactoryName}/{day}/{month}/{year}
        [Route("OpReport/Transformer/{FactoryName}/{day}/{month}/{year}")]
        [HttpGet]
        public IEnumerable<OpReportTransformer> Transformer(string FactoryName, int day, int month, int year)
        {
            List<OpReportTransformer> result = new List<OpReportTransformer>();

            return result;
        }

        // GET api/OpReport/Generator/{FactoryName}/{day}/{month}/{year}
        [Route("OpReport/Generator/{FactoryName}/{day}/{month}/{year}")]
        [HttpGet]
        public IEnumerable<OpReportGenerator> Generator(string FactoryName, int day, int month, int year)
        {
            List<OpReportGenerator> result = new List<OpReportGenerator>();

            List<OpReportGeneratorTagNames> TagNames = OpReportUtils.GetGeneratorTagNames(FactoryName);

            DateTime start = new DateTime(year, month, day);
            DateTime end = start.AddDays(1);

            for (int i  = 0; i < TagNames.Count; i++)
            {
                OpReportGeneratorTagNames taglist = TagNames.ElementAt(i);

                List<DataPoint> data = ScadaHisDao.AnalogSummaryHistoryCyclic(start, end, 60, taglist.ToStringArray(), ScadaHisDao.SummaryType.Maximum);

                OpReportGenerator gen = new OpReportGenerator(taglist.Name);

                gen.ParsingValues(taglist, data);

                result.Add(gen);
            }

            return result;
        }

        // GET api/OpReport/Feeder/{FactoryName}/{day}/{month}/{year}
        [Route("OpReport/Feeder/{FactoryName}/{day}/{month}/{year}")]
        [HttpGet]
        public IEnumerable<OpReportFeeder> Feeder(string FactoryName, int day, int month, int year)
        {
            List<OpReportFeeder> result = new List<OpReportFeeder>();

            List<OpReportFeederTagNames> TagNames = OpReportUtils.GetFeederTagNames(FactoryName);

            DateTime start = new DateTime(year, month, day);
            DateTime end = start.AddDays(1);

            for (int i = 0; i < TagNames.Count; i++)
            {
                OpReportFeederTagNames taglist = TagNames.ElementAt(i);

                List<DataPoint> data = ScadaHisDao.AnalogSummaryHistoryCyclic(start, end, 60, taglist.ToStringArray(), ScadaHisDao.SummaryType.Maximum);

                OpReportFeeder feeder = new OpReportFeeder(taglist.Name);

                feeder.ParsingValues(taglist, data);

                result.Add(feeder);
            }

            return result;
        }
    }
}