using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ScadaHisAPI
{
    [DataContractAttribute]
    public class DataPoint
    {
        [DataMemberAttribute]
        public DateTime DateTime { get; set; }

        [DataMemberAttribute]
        public string TagName { get; set; }

        [DataMemberAttribute]
        public double? Value { get; set; }

        public int? OPCQuality { get; set; }
    }
}