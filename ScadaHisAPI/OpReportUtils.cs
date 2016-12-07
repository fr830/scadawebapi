using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScadaHisAPI.Defines;

namespace ScadaHisAPI
{
    public class OpReportUtils
    {
        public static List<OpReportGeneratorTagNames> GetGeneratorTagNames(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<OpReportGeneratorTagNames>(MongDuong1Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<OpReportGeneratorTagNames>(VinhTan2Define.Generators);

            return new List<OpReportGeneratorTagNames>();
        }

        public static List<OpReportFeederTagNames> GetFeederTagNames(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<OpReportFeederTagNames>(MongDuong1Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<OpReportFeederTagNames>(VinhTan2Define.Feeders);

            return new List<OpReportFeederTagNames>();
        }

        public static List<string> GetUList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<string>(MongDuong1Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<string>(VinhTan2Define.U_Generators);

            return new List<string>();
        }

        public static List<string> GetIList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<string>(MongDuong1Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<string>(VinhTan2Define.I_Generators);

            return new List<string>();
        }

        public static List<string> GetPList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<string>(MongDuong1Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<string>(VinhTan2Define.P_Generators);

            return new List<string>();
        }

        public static double? GetHourlyValues(string TagName, IEnumerable<DataPoint> datalist)
        {
            if (TagName == null || datalist.Count() == 0) return null;

            return (from p in datalist where p.TagName == TagName select (p.Value != null ? (double?)Math.Round((double)p.Value, 2) : 0)).FirstOrDefault();
        }
   }
}