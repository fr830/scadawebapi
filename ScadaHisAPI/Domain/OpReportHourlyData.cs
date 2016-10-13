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
        public List<double?> Values { get; set; }

        public OpReportHourlyData(string name)
        {
            this.Name = name;
            this.Values = null;
        }

        public void SetValues(int index, double? value)
        {
            if (value == null) return;
            if (this.Values == null) this.Values = new List<double?>(new double?[index]);
            Values.Add(value);
        } 
    }
}