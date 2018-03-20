using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaSignalR
{
    public class OPCData
    {
        public string TagName { get; set; }
        public dynamic Value { get; set; }
        public int Quality { get; set; }
        public DateTime TimeStamp { get; set; }
        public string RequestedDataType { get; set; }
    }
}