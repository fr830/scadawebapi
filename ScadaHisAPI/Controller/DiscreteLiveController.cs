using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScadaHisAPI
{
    public class DiscreteLiveController : ApiController
    {
        // GET api/DiscreteLive/{TagName}
        [Route("DiscreteLive/{TagName}")]
        [HttpGet]
        public DataPoint Get(string TagName)
        {
            return ScadaHisDao.DiscreteLive(new string[] { TagName }).FirstOrDefault();
        }

        // POST api/DiscreteLive
        [Route("DiscreteLive")]
        [HttpPost]
        public IEnumerable<DataPoint> Post([FromBody]string[] TagNameList)
        {
            return ScadaHisDao.DiscreteLive(TagNameList);
        }
    }
}