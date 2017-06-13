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

            OpReportTitle title = Hubs.GetReportTitle();

            Params = new OpReportGeneratorParam(title.strNameGenParams);
            Rotor = new OpReportGeneratorRotor(title.strNameGenRotor);
        }

        public bool ParsingValues(int index, OpReportGeneratorTagNames TagNames, IEnumerable<DataPoint> datalist)
        {
            //if ((datalist.Count() == 0) || (TagNames == null)) return false;

            this.Params.Uab.SetValues(index, Utils.GetHourlyValues(TagNames.Uab, datalist));
            this.Params.Ubc.SetValues(index, Utils.GetHourlyValues(TagNames.Ubc, datalist));
            this.Params.Uca.SetValues(index, Utils.GetHourlyValues(TagNames.Uca, datalist));

            this.Params.Ua.SetValues(index, Utils.GetHourlyValues(TagNames.Ua, datalist));
            this.Params.Ub.SetValues(index, Utils.GetHourlyValues(TagNames.Ub, datalist));
            this.Params.Uc.SetValues(index, Utils.GetHourlyValues(TagNames.Uc, datalist));

            this.Params.Ia.SetValues(index, Utils.GetHourlyValues(TagNames.Ia, datalist));
            this.Params.Ib.SetValues(index, Utils.GetHourlyValues(TagNames.Ib, datalist));
            this.Params.Ic.SetValues(index, Utils.GetHourlyValues(TagNames.Ic, datalist));

            this.Params.P.SetValues(index, Utils.GetHourlyValues(TagNames.P, datalist));
            this.Params.Q.SetValues(index, Utils.GetHourlyValues(TagNames.Q, datalist));
            this.Params.F.SetValues(index, Utils.GetHourlyValues(TagNames.F, datalist));

            this.Rotor.U.SetValues(index, Utils.GetHourlyValues(TagNames.URotor, datalist));
            this.Rotor.I.SetValues(index, Utils.GetHourlyValues(TagNames.IRotor, datalist));
            this.Rotor.Velocity.SetValues(index, Utils.GetHourlyValues(TagNames.VRotor, datalist));

            return true;
        }
    }

    [DataContractAttribute]
    public class OpReportGeneratorParam
    {
        [DataMemberAttribute]
        public string Name {get; set;}

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

        public OpReportGeneratorParam(string name)
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

            OpReportTitle title = Hubs.GetReportTitle();

            U = new OpReportHourlyData(title.strNameVoltage);
            I = new OpReportHourlyData(title.strNameCurrent);
            Velocity = new OpReportHourlyData(title.strNameVelocity);
        }
    }

    public class OpReportGeneratorTagNames
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
        public string URotor { get; set; }
        public string IRotor { get; set; }
        public string VRotor { get; set; }

        public string[] toPQString()
        {
            List<string> result = new List<string>();

            result.Add(P);
            result.Add(Q);
            
            return result.ToArray();
        }

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
            result.Add(URotor);
            result.Add(IRotor);
            result.Add(VRotor);

            return result.ToArray();
        }
    }
}