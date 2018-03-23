using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaSignalR
{
    public class TagNameGroup
    {
        public string GroupID { get; set; }
        public List<string> AnalogTagNames { get; set; }
        public List<string> DiscreteTagNames { get; set; }
    }
}