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
        public string Name;

        [DataMemberAttribute]
        public OpReportHourlyData Uab { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ubc { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Uca { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ia { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ib { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ic { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData P { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Q { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData F { get; set; }

        public OpReportFeeder(string name)
        {
            this.Name = name;

            Uab = new OpReportHourlyData(_Define.strNameUab);
            Ubc = new OpReportHourlyData(_Define.strNameUbc);
            Uca = new OpReportHourlyData(_Define.strNameUca);

            Ia = new OpReportHourlyData(_Define.strNameIa);
            Ib = new OpReportHourlyData(_Define.strNameIb);
            Ic = new OpReportHourlyData(_Define.strNameIc);

            P = new OpReportHourlyData(_Define.strNameP);
            Q = new OpReportHourlyData(_Define.strNameQ);
            F = new OpReportHourlyData(_Define.strNameF);
        }

        public bool ParsingValues(int index, OpReportFeederTagNames TagNames, IEnumerable<DataPoint> datalist)
        {
            //if ((datalist.Count() == 0) || (TagNames == null)) return false;           

            this.Uab.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Uab, datalist));
            this.Ubc.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ubc, datalist));
            this.Uca.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Uca, datalist));

            this.Ia.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ia, datalist));
            this.Ib.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ib, datalist));
            this.Ic.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Ic, datalist));

            this.P.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.P, datalist));
            this.Q.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.Q, datalist));
            this.F.SetValues(index, OpReportUtils.GetHourlyValues(TagNames.F, datalist));

            return true;
        }
    }

    public class OpReportFeederTagNames
    {
        public string Name { get; set; }
        public string Uab { get; set; }
        public string Ubc { get; set; }
        public string Uca { get; set; }
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