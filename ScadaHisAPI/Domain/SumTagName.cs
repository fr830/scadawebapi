using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ScadaHisAPI
{
    public class SumTagName
    {
        public string TagName { get; set; }

        public List<string> SumList { get; set; }

        public SumTagName(string tagname)
        {
            this.TagName = tagname;
            SumList = null;
        }

    }
}