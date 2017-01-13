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

        public bool ParsingValues(int index, OpReportTransformerTagNames TagNames, IEnumerable<DataPoint> datalist)
        {
            if ((datalist.Count() == 0) || (TagNames == null)) return false;

            this.Primary.ParsingValues(index, TagNames, datalist);
            this.Secondary1.ParsingValues(index, TagNames, datalist);
            this.Secondary2.ParsingValues(index, TagNames, datalist);

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
        public OpReportHourlyData TAP { get; set; }

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

            TAP = new OpReportHourlyData(_Define.strNameTAP);
            OilTemp = new OpReportHourlyData(_Define.strNameOilTemp);
            WindTemp = new OpReportHourlyData(_Define.strNameWindTemp);
        }

        public bool ParsingValues(int index, OpReportTransformerTagNames TagNames, IEnumerable<DataPoint> datalist)
        {
            if ((datalist.Count() == 0) || (TagNames == null)) return false;

            this.Ua.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ua, datalist));
            this.Ub.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ub, datalist));
            this.Uc.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Uc, datalist));

            this.Ia.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ia, datalist));
            this.Ib.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ib, datalist));
            this.Ic.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ic, datalist));

            this.TAP.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.TAP, datalist));
            this.OilTemp.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.OilTemp, datalist));
            this.WindTemp.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.WindTemp, datalist));

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
        public string TAP { get; set; }
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
            result.Add(TAP);
            result.Add(OilTemp);
            result.Add(WindTemp);

            return result.ToArray();
        }
    }
}