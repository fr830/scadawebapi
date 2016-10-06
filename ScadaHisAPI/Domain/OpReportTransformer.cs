using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ScadaHisAPI
{
    [DataContractAttribute]
    public class OpReportTransformer
    {
        [DataMemberAttribute]
        string Name { get; set; }

        [DataMemberAttribute]
        public OpReportTransformerWind Primary { get; set; }

        [DataMemberAttribute]
        public OpReportTransformerWind Secondary1 { get; set; }

        [DataMemberAttribute]
        public OpReportTransformerWind Secondary2 { get; set; }

        public OpReportTransformer(string name)
        {
            Name = name;

            Primary = new OpReportTransformerWind(_Define.strNameTransPrimary);
            Secondary1 = new OpReportTransformerWind(_Define.strNameTransSecondary1);
            Secondary2 = new OpReportTransformerWind(_Define.strNameTransSecondary2);
        }

        public bool ParsingValues(OpReportTransformerTagNames TagNames, List<DataPoint> datalist)
        {
            if ((datalist.Count == 0) || (TagNames == null)) return false;

            this.Primary.ParsingValues(TagNames, datalist);
            this.Secondary1.ParsingValues(TagNames, datalist);
            this.Secondary2.ParsingValues(TagNames, datalist);

            return true;
        }
    }

    [DataContractAttribute]
    public class OpReportTransformerWind
    {
        [DataMemberAttribute]
        string Name { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ua { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ub { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Uc { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ia { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ib { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ic { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData OilTemp { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData WindTemp { get; set; }

        public OpReportTransformerWind(string name)
        {
            Name = name;

            Ua = new OpReportHourlyData(_Define.strNameUa);
            Ub = new OpReportHourlyData(_Define.strNameUb);
            Uc = new OpReportHourlyData(_Define.strNameUc);

            Ia = new OpReportHourlyData(_Define.strNameIa);
            Ib = new OpReportHourlyData(_Define.strNameIb);
            Ic = new OpReportHourlyData(_Define.strNameIc);

            OilTemp = new OpReportHourlyData(_Define.strNameOilTemp);
            WindTemp = new OpReportHourlyData(_Define.strNameWindTemp);
        }

        public bool ParsingValues(OpReportTransformerTagNames TagNames, List<DataPoint> datalist)
        {
            if ((datalist.Count == 0) || (TagNames == null)) return false;

            this.Ua.Values = OpReportUtils.GetHourlyValues(TagNames.Ua, datalist);
            this.Ub.Values = OpReportUtils.GetHourlyValues(TagNames.Ub, datalist);
            this.Uc.Values = OpReportUtils.GetHourlyValues(TagNames.Uc, datalist);

            this.Ia.Values = OpReportUtils.GetHourlyValues(TagNames.Ia, datalist);
            this.Ib.Values = OpReportUtils.GetHourlyValues(TagNames.Ib, datalist);
            this.Ic.Values = OpReportUtils.GetHourlyValues(TagNames.Ic, datalist);

            this.OilTemp.Values = OpReportUtils.GetHourlyValues(TagNames.OilTemp, datalist);
            this.WindTemp.Values = OpReportUtils.GetHourlyValues(TagNames.WindTemp, datalist);

            return true;
        }
    }

    public class OpReportTransformerTagNames
    {
        public string Name { get; set; }
        public string Ua { get; set; }
        public string Ub { get; set; }
        public string Uc { get; set; }
        public string Ia { get; set; }
        public string Ib { get; set; }
        public string Ic { get; set; }
        public string OilTemp { get; set; }
        public string WindTemp { get; set; }

        public string[] ToStringArray()
        {
            List<string> result = new List<string>();

            result.Add(Ua);
            result.Add(Ub);
            result.Add(Uc);
            result.Add(Ia);
            result.Add(Ib);
            result.Add(Ic);
            result.Add(OilTemp);
            result.Add(WindTemp);

            return result.ToArray();
        }
    }
}