using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaSignalR
{
    public class DataPoint
    {
        [JsonProperty("DateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("TagName")]
        public string TagName { get; set; }

        [JsonProperty("Value")]
        public dynamic Value { get; set; }

        [JsonProperty("OPCQuality")]
        public int? OPCQuality { get; set; }
    }

}
