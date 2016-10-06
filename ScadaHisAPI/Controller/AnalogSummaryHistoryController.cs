using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScadaHisAPI
{
    public class AnalogSummaryHistoryController : ApiController
    {

        // POST api/AnalogSummaryHistory/Average/{start}/{end}/{CycleMinutes}
        // QueryString api/AnalogSummaryHistory/Average?start={start}&end={end}&CycleMinutes={CycleMinutes}
        [Route("AnalogSummaryHistory/Average/{start=start}/{end=end}/{CycleMinutes=CycleMinutes}")]
        [HttpPost]
        public IEnumerable<DataPoint> Average([FromUri]DateTime start, [FromUri]DateTime end, [FromUri]int CycleMinutes, [FromBody]string[] TagNameList)
        {
            return ScadaHisDao.AnalogSummaryHistoryCyclic(start, end, CycleMinutes, TagNameList, ScadaHisDao.SummaryType.Average);
        }

        // POST api/AnalogSummaryHistory/Maximum/{start}/{end}/{CycleMinutes}
        // QueryString api/AnalogSummaryHistory/Maximum?start={start}&end={end}&CycleMinutes={CycleMinutes}
        [Route("AnalogSummaryHistory/Maximum/{start=start}/{end=end}/{CycleMinutes=CycleMinutes}")]
        [HttpPost]
        public IEnumerable<DataPoint> Maximum([FromUri]DateTime start, [FromUri]DateTime end, [FromUri]int CycleMinutes, [FromBody]string[] TagNameList)
        {
            return ScadaHisDao.AnalogSummaryHistoryCyclic(start, end, CycleMinutes, TagNameList, ScadaHisDao.SummaryType.Maximum);
        }

        // POST api/AnalogSummaryHistory/Minimum/{start}/{end}/{CycleMinutes}
        // QueryString api/AnalogSummaryHistory/Minimum?start={start}&end={end}&CycleMinutes={CycleMinutes}
        [Route("AnalogSummaryHistory/Minimum/{start=start}/{end=end}/{CycleMinutes=CycleMinutes}")]
        [HttpPost]
        public IEnumerable<DataPoint> Minimum([FromUri]DateTime start, [FromUri]DateTime end, [FromUri]int CycleMinutes, [FromBody]string[] TagNameList)
        {
            return ScadaHisDao.AnalogSummaryHistoryCyclic(start, end, CycleMinutes, TagNameList, ScadaHisDao.SummaryType.Minimum);
        }
    }
}