using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ScadaHisAPI
{
    [DataContractAttribute]
    public class OpReportHourlyData
    {
        [DataMemberAttribute]
        public string Name { get; set; }

        [DataMemberAttribute]
        public double[] Values { get; set; }

        public OpReportHourlyData(string name)
        {
            this.Name = name;
            Values = new double[24];
        }
    }
}