using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class OpReportUtils
    {
        public static List<OpReportGeneratorTagNames> GetGeneratorTagNames(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.UongBi_ID) == 0)
                return new List<OpReportGeneratorTagNames>(UongBiDefine.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BanVe_ID) == 0)
                return new List<OpReportGeneratorTagNames>(BanVeDefine.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DaiNinh_ID) == 0)
                return new List<OpReportGeneratorTagNames>(DaiNinhDefine.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai3_ID) == 0)
                return new List<OpReportGeneratorTagNames>(DongNai3Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai4_ID) == 0)
                return new List<OpReportGeneratorTagNames>(DongNai4Define.Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.SongTranh2_ID) == 0)
                return new List<OpReportGeneratorTagNames>(SongTranh2Define.Generators);

            return new List<OpReportGeneratorTagNames>();
        }

        public static List<OpReportFeederTagNames> GetFeederTagNames(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.UongBi_ID) == 0)
                return new List<OpReportFeederTagNames>(UongBiDefine.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BanVe_ID) == 0)
                return new List<OpReportFeederTagNames>(BanVeDefine.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DaiNinh_ID) == 0)
                return new List<OpReportFeederTagNames>(DaiNinhDefine.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai3_ID) == 0)
                return new List<OpReportFeederTagNames>(DongNai3Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai4_ID) == 0)
                return new List<OpReportFeederTagNames>(DongNai4Define.Feeders);

            else if (String.Compare(FactoryName.ToUpper(), _Define.SongTranh2_ID) == 0)
                return new List<OpReportFeederTagNames>(SongTranh2Define.Feeders);

            return new List<OpReportFeederTagNames>();
        }

        public static List<string> GetUList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.UongBi_ID) == 0)
                return new List<string>(UongBiDefine.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BanVe_ID) == 0)
                return new List<string>(BanVeDefine.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DaiNinh_ID) == 0)
                return new List<string>(DaiNinhDefine.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai3_ID) == 0)
                return new List<string>(DongNai3Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai4_ID) == 0)
                return new List<string>(DongNai4Define.U_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.SongTranh2_ID) == 0)
                return new List<string>(SongTranh2Define.U_Generators);

            return new List<string>();
        }

        public static List<string> GetIList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.UongBi_ID) == 0)
                return new List<string>(UongBiDefine.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BanVe_ID) == 0)
                return new List<string>(BanVeDefine.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DaiNinh_ID) == 0)
                return new List<string>(DaiNinhDefine.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai3_ID) == 0)
                return new List<string>(DongNai3Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai4_ID) == 0)
                return new List<string>(DongNai4Define.I_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.SongTranh2_ID) == 0)
                return new List<string>(SongTranh2Define.I_Generators);

            return new List<string>();
        }

        public static List<string> GetPList(string FactoryName)
        {
            if (String.Compare(FactoryName.ToUpper(), _Define.UongBi_ID) == 0)
                return new List<string>(UongBiDefine.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.BanVe_ID) == 0)
                return new List<string>(BanVeDefine.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DaiNinh_ID) == 0)
                return new List<string>(DaiNinhDefine.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai3_ID) == 0)
                return new List<string>(DongNai3Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.DongNai4_ID) == 0)
                return new List<string>(DongNai4Define.P_Generators);

            else if (String.Compare(FactoryName.ToUpper(), _Define.SongTranh2_ID) == 0)
                return new List<string>(SongTranh2Define.P_Generators);

            return new List<string>();
        }

        public static double[] GetHourlyValues(string TagName, List<DataPoint> datalist)
        {
            if (TagName == null || datalist.Count == 0) return null;

            return (from p in datalist where p.TagName == TagName orderby p.DateTime select (p.Value != null ? Math.Round((double)p.Value, 3) : 0)).ToArray();
        }
   }
}