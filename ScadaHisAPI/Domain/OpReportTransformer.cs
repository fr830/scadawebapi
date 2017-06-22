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

            OpReportTitle title = XMLConfig.GetReportTitle();

            Primary = new OpReportTransformerWind(title.strNameTransPrimary);
            Secondary1 = new OpReportTransformerWind(title.strNameTransSecondary1);
            Secondary2 = new OpReportTransformerWind(title.strNameTransSecondary2);
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

            OpReportTitle title = XMLConfig.GetReportTitle();

            Ua = new OpReportHourlyData(title.strNameUa);
            Ub = new OpReportHourlyData(title.strNameUb);
            Uc = new OpReportHourlyData(title.strNameUc);

            Ia = new OpReportHourlyData(title.strNameIa);
            Ib = new OpReportHourlyData(title.strNameIb);
            Ic = new OpReportHourlyData(title.strNameIc);

            TAP = new OpReportHourlyData(title.strNameTAP);
            OilTemp = new OpReportHourlyData(title.strNameOilTemp);
            WindTemp = new OpReportHourlyData(title.strNameWindTemp);
        }

        public bool ParsingValues(int index, OpReportTransformerTagNames TagNames, IEnumerable<DataPoint> datalist)
        {
            if ((datalist.Count() == 0) || (TagNames == null)) return false;

            this.Ua.SetValues(index, Utils.GetHourlyValues(TagNames.Ua, datalist));
            this.Ub.SetValues(index, Utils.GetHourlyValues(TagNames.Ub, datalist));
            this.Uc.SetValues(index, Utils.GetHourlyValues(TagNames.Uc, datalist));

            this.Ia.SetValues(index, Utils.GetHourlyValues(TagNames.Ia, datalist));
            this.Ib.SetValues(index, Utils.GetHourlyValues(TagNames.Ib, datalist));
            this.Ic.SetValues(index, Utils.GetHourlyValues(TagNames.Ic, datalist));

            this.TAP.SetValues(index, Utils.GetHourlyValues(TagNames.TAP, datalist));
            this.OilTemp.SetValues(index, Utils.GetHourlyValues(TagNames.OilTemp, datalist));
            this.WindTemp.SetValues(index, Utils.GetHourlyValues(TagNames.WindTemp, datalist));

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