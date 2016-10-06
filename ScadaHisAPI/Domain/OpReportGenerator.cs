using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ScadaHisAPI
{
    [DataContractAttribute]
    public class OpReportGenerator
    {
        [DataMemberAttribute]
        public string Name {get; set;}

        [DataMemberAttribute]
        public OpReportGeneratorParam Params { get; set; }

        [DataMemberAttribute]
        public OpReportGeneratorRotor Rotor { get; set; }

        public OpReportGenerator(string name)
        {
            this.Name = name;

            Params = new OpReportGeneratorParam(_Define.strNameGenParams);
            Rotor = new OpReportGeneratorRotor(_Define.strNameGenRotor);
        }

        public bool ParsingValues(OpReportGeneratorTagNames TagNames, List<DataPoint> datalist)
        {
            if ((datalist.Count == 0) || (TagNames == null)) return false;

            this.Params.Uab.Values = OpReportUtils.GetHourlyValues(TagNames.Uab, datalist);
            this.Params.Ubc.Values = OpReportUtils.GetHourlyValues(TagNames.Ubc, datalist);
            this.Params.Uca.Values = OpReportUtils.GetHourlyValues(TagNames.Uca, datalist);

            //this.Params.Ua.Values = OpReportUtils.GetHourlyValues(TagNames.Ua, datalist);
            //this.Params.Ub.Values = OpReportUtils.GetHourlyValues(TagNames.Ub, datalist);
            //this.Params.Uc.Values = OpReportUtils.GetHourlyValues(TagNames.Uc, datalist);

            this.Params.Ia.Values = OpReportUtils.GetHourlyValues(TagNames.Ia, datalist);
            this.Params.Ib.Values = OpReportUtils.GetHourlyValues(TagNames.Ib, datalist);
            this.Params.Ic.Values = OpReportUtils.GetHourlyValues(TagNames.Ic, datalist);

            this.Params.P.Values = OpReportUtils.GetHourlyValues(TagNames.P, datalist);
            this.Params.Q.Values = OpReportUtils.GetHourlyValues(TagNames.Q, datalist);
            this.Params.F.Values = OpReportUtils.GetHourlyValues(TagNames.F, datalist);

            this.Rotor.U.Values = OpReportUtils.GetHourlyValues(TagNames.URotor, datalist);
            this.Rotor.I.Values = OpReportUtils.GetHourlyValues(TagNames.IRotor, datalist);
            this.Rotor.Velocity.Values = OpReportUtils.GetHourlyValues(TagNames.VRotor, datalist);

            return true;
        }
    }

    [DataContractAttribute]
    public class OpReportGeneratorParam
    {
        [DataMemberAttribute]
        public string Name;

        [DataMemberAttribute]
        public OpReportHourlyData Uab { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Ubc { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Uca { get; set; }

        //[DataMemberAttribute]
        //public OpReportHourlyData Ua { get; set; }

        //[DataMemberAttribute]
        //public OpReportHourlyData Ub { get; set; }

        //[DataMemberAttribute]
        //public OpReportHourlyData Uc { get; set; }

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

        public OpReportGeneratorParam(string name)
        {
            this.Name = name;

            Uab = new OpReportHourlyData(_Define.strNameUab);
            Ubc = new OpReportHourlyData(_Define.strNameUbc);
            Uca = new OpReportHourlyData(_Define.strNameUca);

            //Ua = new OpReportHourlyData(_Define.strNameUa);
            //Ub = new OpReportHourlyData(_Define.strNameUb);
            //Uc = new OpReportHourlyData(_Define.strNameUc);

            Ia = new OpReportHourlyData(_Define.strNameIa);
            Ib = new OpReportHourlyData(_Define.strNameIb);
            Ic = new OpReportHourlyData(_Define.strNameIc);

            P = new OpReportHourlyData(_Define.strNameP);
            Q = new OpReportHourlyData(_Define.strNameQ);
            F = new OpReportHourlyData(_Define.strNameF);
        }
    }

    [DataContractAttribute]
    public class OpReportGeneratorRotor
    {
        [DataMemberAttribute]
        public string Name { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData U { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData I { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Velocity { get; set; }

        public OpReportGeneratorRotor(string name)
        {
            this.Name = name;

            U = new OpReportHourlyData(_Define.strNameVoltage);
            I = new OpReportHourlyData(_Define.strNameCurrent);
            Velocity = new OpReportHourlyData(_Define.strNameVelocity);
        }
    }

    public class OpReportGeneratorTagNames
    {
        public string Name { get; set; }
        public string Uab { get; set; }
        public string Ubc { get; set; }
        public string Uca { get; set; }
        //public string Ua { get; set; }
        //public string Ub { get; set; }
        //public string Uc { get; set; }
        public string Ia { get; set; }
        public string Ib { get; set; }
        public string Ic { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string F { get; set; }
        public string URotor { get; set; }
        public string IRotor { get; set; }
        public string VRotor { get; set; }

        public string[] ToStringArray()
        {
            List<string> result = new List<string>();

            result.Add(Uab);
            result.Add(Ubc);
            result.Add(Uca);
            //result.Add(Ua);
            //result.Add(Ub);
            //result.Add(Uc);
            result.Add(Ia);
            result.Add(Ib);
            result.Add(Ic);
            result.Add(P);
            result.Add(Q);
            result.Add(F);
            result.Add(URotor);
            result.Add(IRotor);
            result.Add(VRotor);

            return result.ToArray();
        }
    }
}