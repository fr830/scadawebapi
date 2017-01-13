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

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy1_ID) == 0)
                return new List<OpReportGeneratorTagNames>(PhuMy1Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy21_ID) == 0)
                return new List<OpReportGeneratorTagNames>(PhuMy21Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<OpReportGeneratorTagNames>(PhuMy22Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<OpReportGeneratorTagNames>(PhuMy22Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy3_ID) == 0)
                return new List<OpReportGeneratorTagNames>(PhuMy3Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy4_ID) == 0)
                return new List<OpReportGeneratorTagNames>(PhuMy4Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BaRia_ID) == 0)
                return new List<OpReportGeneratorTagNames>(BaRiaDefine.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonKuop_ID) == 0)
                return new List<OpReportGeneratorTagNames>(BuonKuopDefine.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonTuaShar_ID) == 0)
                return new List<OpReportGeneratorTagNames>(BuonTuaSharDefine.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.Srepok3_ID) == 0)
                return new List<OpReportGeneratorTagNames>(Srepok3Define.Generators);

            return new List<OpReportGeneratorTagNames>();
        }

        public static List<OpReportFeederTagNames> GetFeederTagNames(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<OpReportFeederTagNames>(MongDuong1Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<OpReportFeederTagNames>(VinhTan2Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy1_ID) == 0)
                return new List<OpReportFeederTagNames>(PhuMy1Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy21_ID) == 0)
                return new List<OpReportFeederTagNames>(PhuMy21Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<OpReportFeederTagNames>(PhuMy22Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<OpReportFeederTagNames>(PhuMy22Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy3_ID) == 0)
                return new List<OpReportFeederTagNames>(PhuMy3Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy4_ID) == 0)
                return new List<OpReportFeederTagNames>(PhuMy4Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BaRia_ID) == 0)
                return new List<OpReportFeederTagNames>(BaRiaDefine.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonKuop_ID) == 0)
                return new List<OpReportFeederTagNames>(BuonKuopDefine.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonTuaShar_ID) == 0)
                return new List<OpReportFeederTagNames>(BuonTuaSharDefine.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.Srepok3_ID) == 0)
                return new List<OpReportFeederTagNames>(Srepok3Define.Feeders);

            return new List<OpReportFeederTagNames>();
        }

        public static List<string> GetUList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<string>(MongDuong1Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<string>(VinhTan2Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy1_ID) == 0)
                return new List<string>(PhuMy1Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy21_ID) == 0)
                return new List<string>(PhuMy21Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<string>(PhuMy22Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<string>(PhuMy22Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy3_ID) == 0)
                return new List<string>(PhuMy3Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy4_ID) == 0)
                return new List<string>(PhuMy4Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BaRia_ID) == 0)
                return new List<string>(BaRiaDefine.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonKuop_ID) == 0)
                return new List<string>(BuonKuopDefine.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonTuaShar_ID) == 0)
                return new List<string>(BuonTuaSharDefine.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.Srepok3_ID) == 0)
                return new List<string>(Srepok3Define.U_Generators);


            return new List<string>();
        }

        public static List<string> GetIList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<string>(MongDuong1Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<string>(VinhTan2Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy1_ID) == 0)
                return new List<string>(PhuMy1Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy21_ID) == 0)
                return new List<string>(PhuMy21Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<string>(PhuMy22Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<string>(PhuMy22Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy3_ID) == 0)
                return new List<string>(PhuMy3Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy4_ID) == 0)
                return new List<string>(PhuMy4Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BaRia_ID) == 0)
                return new List<string>(BaRiaDefine.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonKuop_ID) == 0)
                return new List<string>(BuonKuopDefine.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonTuaShar_ID) == 0)
                return new List<string>(BuonTuaSharDefine.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.Srepok3_ID) == 0)
                return new List<string>(Srepok3Define.I_Generators);

            return new List<string>();
        }

        public static List<string> GetPList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.MongDuong1_ID) == 0)
                return new List<string>(MongDuong1Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.VinhTan2_ID) == 0)
                return new List<string>(VinhTan2Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy1_ID) == 0)
                return new List<string>(PhuMy1Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy21_ID) == 0)
                return new List<string>(PhuMy21Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<string>(PhuMy22Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy22_ID) == 0)
                return new List<string>(PhuMy22Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy3_ID) == 0)
                return new List<string>(PhuMy3Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.PhuMy4_ID) == 0)
                return new List<string>(PhuMy4Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BaRia_ID) == 0)
                return new List<string>(BaRiaDefine.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonKuop_ID) == 0)
                return new List<string>(BuonKuopDefine.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BuonTuaShar_ID) == 0)
                return new List<string>(BuonTuaSharDefine.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.Srepok3_ID) == 0)
                return new List<string>(Srepok3Define.P_Generators);

            return new List<string>();
        }

        public static double? GetHourlyValues(string TagName, IEnumerable<DataPoint> datalist)
        {
            if (TagName == null || datalist.Count() == 0) return null;

            return (from p in datalist where p.TagName == TagName select (p.Value != null ? (double?)Math.Round((double)p.Value, 2) : 0)).FirstOrDefault();
        }
   }
}