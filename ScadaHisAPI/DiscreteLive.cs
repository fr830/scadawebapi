//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScadaHisAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class DiscreteLive
    {
        public System.DateTime DateTime { get; set; }
        public string TagName { get; set; }
        public Nullable<double> Value { get; set; }
        public byte Quality { get; set; }
        public Nullable<int> QualityDetail { get; set; }
        public Nullable<int> OPCQuality { get; set; }
        public int wwTagKey { get; set; }
        public string wwRetrievalMode { get; set; }
        public Nullable<int> wwTimeDeadband { get; set; }
        public string wwTimeZone { get; set; }
        public string wwParameters { get; set; }
    }
}
