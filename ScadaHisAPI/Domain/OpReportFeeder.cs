using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ScadaHisAPI
{
    [DataContractAttribute]
    public class OpReportFeeder
    {
        [DataMemberAttribute]
        public string Name { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData P { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Q { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Uab { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ubc { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Uca { get; set; }

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
        public OpReportHourlyData F { get; set; }

        public OpReportFeeder(string name)
        {
            this.Name = name;

            OpReportTitle title = Hubs.GetReportTitle();

            Uab = new OpReportHourlyData(title.strNameUab);
            Ubc = new OpReportHourlyData(title.strNameUbc);
            Uca = new OpReportHourlyData(title.strNameUca);

            Ua = new OpReportHourlyData(title.strNameUa);
            Ub = new OpReportHourlyData(title.strNameUb);
            Uc = new OpReportHourlyData(title.strNameUc);

            Ia = new OpReportHourlyData(title.strNameIa);
            Ib = new OpReportHourlyData(title.strNameIb);
            Ic = new OpReportHourlyData(title.strNameIc);

            P = new OpReportHourlyData(title.strNameP);
            Q = new OpReportHourlyData(title.strNameQ);
            F = new OpReportHourlyData(title.strNameF);
        }

        public bool ParsingValues(int index, OpReportFeederTagNames TagNames, IEnumerable<DataPoint> datalist)
        {
            //if ((datalist.Count() == 0) || (TagNames == null)) return false;           

            this.Uab.SetValues(index, Utils.GetHourlyValues(TagNames.Uab, datalist));
            this.Ubc.SetValues(index, Utils.GetHourlyValues(TagNames.Ubc, datalist));
            this.Uca.SetValues(index, Utils.GetHourlyValues(TagNames.Uca, datalist));

            this.Ua.SetValues(index, Utils.GetHourlyValues(TagNames.Ua, datalist));
            this.Ub.SetValues(index, Utils.GetHourlyValues(TagNames.Ub, datalist));
            this.Uc.SetValues(index, Utils.GetHourlyValues(TagNames.Uc, datalist));

            this.Ia.SetValues(index, Utils.GetHourlyValues(TagNames.Ia, datalist));
            this.Ib.SetValues(index, Utils.GetHourlyValues(TagNames.Ib, datalist));
            this.Ic.SetValues(index, Utils.GetHourlyValues(TagNames.Ic, datalist));

            this.P.SetValues(index, Utils.GetHourlyValues(TagNames.P, datalist));
            this.Q.SetValues(index, Utils.GetHourlyValues(TagNames.Q, datalist));
            this.F.SetValues(index, Utils.GetHourlyValues(TagNames.F, datalist));

            return true;
        }
    }

    public class OpReportFeederTagNames
    {
        public string Name { get; set; }
        public string Uab { get; set; }
        public string Ubc { get; set; }
        public string Uca { get; set; }
        public string Ua { get; set; }
        public string Ub { get; set; }
        public string Uc { get; set; }
        public string Ia { get; set; }
        public string Ib { get; set; }
        public string Ic { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string F { get; set; }

        public string[] ToStringArray()
        {
            List<string> result = new List<string>();

            result.Add(Uab);
            result.Add(Ubc);
            result.Add(Uca);
            result.Add(Ua);
            result.Add(Ub);
            result.Add(Uc);
            result.Add(Ia);
            result.Add(Ib);
            result.Add(Ic);
            result.Add(P);
            result.Add(Q);
            result.Add(F);

            return result.ToArray();
        }
    }
}