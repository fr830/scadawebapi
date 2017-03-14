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
            return ScadaHisDao.AnalogLive(TagNameList);
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

                    result.Add(DataPointSum(power, PowerList, factory));
                }
            }

            return result;
        }

        private DataPoint DataPointSum(IEnumerable<DataPoint> datalist, string[] TagNameList, string NewTagName)
        {
            DataPoint sum = new DataPoint() { DateTime = DateTime.Now, TagName = NewTagName, Value = null };

            var data = (from p in datalist where TagNameList.Contains(p.TagName) select p);

            if (data.Count<DataPoint>() > 0)
            {
                sum.DateTime = data.FirstOrDefault().DateTime;
                sum.Value = (from p in data where p.Value.HasValue select p.Value).Sum();
            }

            return sum;
        }
    }
}