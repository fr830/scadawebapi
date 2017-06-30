﻿using System;
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

            List<OpReportGeneratorTagNames> TagNames = XMLConfig.GetGeneratorTagNames(FactoryName);

            List<string> TagNameQuery = new List<string>();

            DateTime start = new DateTime(year, month, day);
            DateTime end = start.AddDays(1);

            foreach (OpReportGeneratorTagNames tag in TagNames)
            {
                OpReportGenerator gen = new OpReportGenerator(tag.Name);
                result.Add(gen);
                TagNameQuery.AddRange(tag.ToStringArray());
            }

            List<DataPoint> data = ScadaHisDao.AnalogHistoryCyclic2(start, end, TagNameQuery.ToArray(), ScadaHisDao.CycleTime.OneHour, ScadaHisDao.SummaryType.Average).ToList();

            while (start < end)
            {
                DateTime end2 = start.AddMinutes(60);

                List<DataPoint> subdata = (from p in data where start.Hour == p.DateTime.Hour select p).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result.ElementAt(i).ParsingValues(start.Hour, TagNames.ElementAt(i), subdata);
                }

                start = end2;
            }

            return result;
        }

        // POST apiOpReport/Generator/PQ/{day=day}/{month=month}/{year=year}
        [Route("OpReport/Generator/PQ/{day=day}/{month=month}/{year=year}")]
        [HttpPost]
        public IEnumerable<OpReportFactoryPQ> FactoryPQ([FromUri]int day, [FromUri]int month, [FromUri]int year, [FromBody]string[] FactoryList)
        {
            List<OpReportFactoryPQ> result = new List<OpReportFactoryPQ>();
            List<string> TagNameQuery = new List<string>();
            
            List<DataPoint> data = new List<DataPoint>();

            foreach (string FactoryName in FactoryList)
            {
                List<OpReportGeneratorTagNames> TagNames = XMLConfig.GetGeneratorTagNames(FactoryName);
                OpReportFactoryPQ factory = new OpReportFactoryPQ(FactoryName);

                result.Add(factory);

                foreach (OpReportGeneratorTagNames tag in TagNames)
                {
                    factory.AddGenerator(new OpReportGeneratorPQ(tag.Name, tag));
                    TagNameQuery.AddRange(tag.toPQString());
                }
            }

            DateTime start = new DateTime(year, month, day);
            DateTime end = start.AddDays(1);

            data = ScadaHisDao.AnalogHistoryCyclic2(start, end, TagNameQuery.ToArray(), ScadaHisDao.CycleTime.OneHour, ScadaHisDao.SummaryType.Average).ToList();

            while (start < end)
            {
                DateTime end2 = start.AddMinutes(60);

                List<DataPoint> subdata = (from p in data where start.Hour == p.DateTime.Hour select p).ToList();

                foreach (OpReportFactoryPQ factory in result)
                {
                    foreach(OpReportGeneratorPQ gen in factory.Generator)
                    {
                        gen.ParsingValues(start.Hour, subdata);
                    }
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

            List<OpReportFeederTagNames> TagNames = XMLConfig.GetFeederTagNames(FactoryName);

            List<string> TagNameQuery = new List<string>();

            DateTime start = new DateTime(year, month, day);
            DateTime end = start.AddDays(1);

            foreach (OpReportFeederTagNames tag in TagNames)
            {
                OpReportFeeder feeder = new OpReportFeeder(tag.Name);
                result.Add(feeder);
                TagNameQuery.AddRange(tag.ToStringArray());
            }


            List<DataPoint> data = ScadaHisDao.AnalogHistoryCyclic2(start, end, TagNameQuery.ToArray(), ScadaHisDao.CycleTime.OneHour, ScadaHisDao.SummaryType.Average).ToList();

            while (start < end)
            {
                DateTime end2 = start.AddMinutes(60);

                List<DataPoint> subdata = (from p in data where start.Hour == p.DateTime.Hour select p).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result.ElementAt(i).ParsingValues(start.Hour, TagNames.ElementAt(i), subdata);
                }

                start = end2;
            }

            return result;
        }
    }
}