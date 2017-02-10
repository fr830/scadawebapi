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

            DateTime now = DateTime.Now;

            foreach (string factory in FactoryList)
            {
                if (factory != null)
                {
                    string[] TagNameList = EnergyUtils.FactoryToPowerTag(new string[]{factory}, null);
                    var power = ScadaHisDao.AnalogLive(TagNameList);

                    var data = (from p in power where p.OPCQuality >= 192 /*&& Math.Abs((p.DateTime - now).TotalHours) < 1*/ select p);

                    DataPoint dp;

                    if (data.Count<DataPoint>() > 0)
                    {
                        dp = new DataPoint
                        {
                            DateTime = data.FirstOrDefault().DateTime,
                            TagName = factory,
                            Value = (from p in data where p.Value.HasValue select p.Value).Sum()
                        };
                    }
                    else
                    {
                        dp = new DataPoint
                        {
                            DateTime = now,
                            TagName = factory,
                            Value = null
                        };
                    }

                    result.Add(dp);
                }
            }

            return result;
        }
    }
}