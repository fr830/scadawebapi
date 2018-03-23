using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaSignalR
{
    public class HMITag
    {
        public string TagName { get; set; }
        public dynamic value { get; set; }
        public int quality { get; set; }
        public DateTime timeStamp { get; set; }
        public string RequestedDataType { get; set; }
    }
}