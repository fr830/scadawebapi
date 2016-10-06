using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScadaHisAPI
{
    public class AnalogHistoryController : ApiController
    {
        // POST api/AnalogHistory/{start}/{end}/{CycleMinutes}
        // QueryString api/AnalogHistory?start={start}&end={end}&CycleMinutes={CycleMinutes}
        [Route("api/AnalogHistory/{start=start}/{end=end}/{CycleMinutes=CycleMinutes}")]
        [HttpPost]
        public IEnumerable<DataPoint> Post([FromUri]DateTime start, [FromUri]DateTime end, [FromUri]int CycleMinutes, [FromBody]string[] TagNameList)
        {
            return ScadaHisDao.AnalogHistory(start, end, CycleMinutes, TagNameList);
        }
    }
}