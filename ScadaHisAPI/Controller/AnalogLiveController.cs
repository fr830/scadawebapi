using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScadaHisAPI
{
    public class AnalogLiveController : ApiController
    {
        // GET api/AnalogLive/{TagName}
        [Route("AnalogLive/{TagName}")]
        [HttpGet]
        public DataPoint Get(string TagName)
        {
            return ScadaHisDao.AnalogLive(new string[] { TagName }).FirstOrDefault();
        }

        // POST api/AnalogLive
        [Route("AnalogLive")]
        [HttpPost]
        public IEnumerable<DataPoint> Post([FromBody]string[] TagNameList)
        {
            List<string> tagList = new List<string>(TagNameList);
            List<SumTagName> SumTagnameList = Utils.SearchSumTagname(TagNameList);

            if (SumTagnameList.Count > 0)
            {
                foreach (SumTagName sumTag in SumTagnameList)
                {
                    tagList.AddRange(sumTag.SumList);
                }
            }

            var datalist = ScadaHisDao.AnalogLive(tagList.ToArray());

            List<DataPoint> result = datalist.ToList();

            if (SumTagnameList.Count > 0)
            {
                foreach (SumTagName sumTag in SumTagnameList)
                {
                    result.Add(Utils.DataPointSum(datalist, sumTag.SumList.ToArray(), sumTag.TagName));
                }
            }

            return result;
        }

        // POST api/PowerLive/Power
        [Route("AnalogLive/Power")]
        [HttpPost]
        public IEnumerable<DataPoint> Power([FromBody]string[] FactoryList)
        {
            List<DataPoint> result = new List<DataPoint>();

            string[] TagNameList = EnergyUtils.FactoryToPowerTag(FactoryList, null);
            var power = ScadaHisDao.AnalogLive(TagNameList);

            foreach (string factory in FactoryList)
            {
                if (factory != null)
                {
                    string[] PowerList = EnergyUtils.FactoryToPowerTag(new string[] { factory }, null);

                    result.Add(Utils.DataPointSum(power, PowerList, factory));
                }
            }

            return result;
        }
    }
}