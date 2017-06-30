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

            var datalist = ScadaHisDao.AnalogLive(tagList.ToArray()).ToList();

            List<DataPoint> result = datalist;

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
            List<string> TagNameList = new List<string>();
            List<SumTagName> SumTagList = new List<SumTagName>();

            foreach (string factory in FactoryList)
            {
                if (factory != null)
                {
                    List<string> str = XMLConfig.GetEnergySources(factory);
                    TagNameList.AddRange(str);
                    SumTagList.Add(new SumTagName { TagName = factory, SumList = str });
                }
            }

            var power = ScadaHisDao.AnalogLive(TagNameList.ToArray());

            SumTagList.ForEach(x => result.Add(Utils.DataPointSum(power, x.SumList.ToArray(), x.TagName)));

            return result;
        }
    }
}