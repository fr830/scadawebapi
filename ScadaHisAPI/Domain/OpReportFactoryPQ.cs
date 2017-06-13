using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ScadaHisAPI
{
    [DataContractAttribute]
    public class OpReportFactoryPQ
    {
        [DataMemberAttribute]
        public string FactoryID { get; set; }

        [DataMemberAttribute]
        public int GeneratorCount { get; set; }

        [DataMemberAttribute]
        public List<OpReportGeneratorPQ> Generator { get; set; }

        public OpReportFactoryPQ(string factoryID)
        {
            this.FactoryID = factoryID;

            GeneratorCount = 0;
            Generator = new List<OpReportGeneratorPQ>();
        }

        public bool AddGenerator(OpReportGeneratorPQ gen)
        {
            Generator.Add(gen);
            GeneratorCount++;
            return true;
        }
    }


    [DataContractAttribute]
    public class OpReportGeneratorPQ
    {
        [DataMemberAttribute]
        public string Name;

        [DataMemberAttribute]
        public OpReportHourlyData P { get; set; }

        [DataMemberAttribute]
        public OpReportHourlyData Q { get; set; }

        OpReportGeneratorTagNames TagNames;

        public OpReportGeneratorPQ(string name, OpReportGeneratorTagNames tagnames)
        {
            this.Name = name;

            OpReportTitle title = Hubs.GetReportTitle();

            P = new OpReportHourlyData(title.strNameP);
            Q = new OpReportHourlyData(title.strNameQ);

            TagNames = tagnames;
        }

        public bool ParsingValues(int index, IEnumerable<DataPoint> datalist)
        {
            //if ((datalist.Count() == 0) || (TagNames == null)) return false;

            this.P.SetValues(index, Utils.GetHourlyValues(TagNames.P, datalist));
            this.Q.SetValues(index, Utils.GetHourlyValues(TagNames.Q, datalist));

            return true;
        }
    }
}