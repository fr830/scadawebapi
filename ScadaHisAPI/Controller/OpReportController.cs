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

            List<string> TagNameQuery = new List<string>();

            DateTime start = new DateTime(year, month, day);
            DateTime end = start.AddDays(1);

            foreach (OpReportGeneratorTagNames tag in TagNames)
            {
                OpReportGenerator gen = new OpReportGenerator(tag.Name);
                result.Add(gen);
                TagNameQuery.AddRange(tag.ToStringArray());
            }

            while (start < end)
            {
                DateTime end2 = start.AddMinutes(60);
                List<DataPoint> data = ScadaHisDao.AnalogSummaryHistory(start, end2, TagNameQuery.ToArray(), ScadaHisDao.SummaryType.Maximum).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result.ElementAt(i).ParsingValues(start.Hour, TagNames.ElementAt(i), data);
                }

                start = end2;
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

            List<string> TagNameQuery = new List<string>();

            DateTime start = new DateTime(year, month, day);
            DateTime end = start.AddDays(1);

            foreach (OpReportFeederTagNames tag in TagNames)
            {
                OpReportFeeder feeder = new OpReportFeeder(tag.Name);
                result.Add(feeder);
                TagNameQuery.AddRange(tag.ToStringArray());
            }

            while (start < end)
            {
                DateTime end2 = start.AddMinutes(60);
                List<DataPoint> data = ScadaHisDao.AnalogSummaryHistory(start, end2, TagNameQuery.ToArray(), ScadaHisDao.SummaryType.Maximum).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result.ElementAt(i).ParsingValues(start.Hour, TagNames.ElementAt(i), data);
                }

                start = end2;
            }

            return result;
        }
    }
}