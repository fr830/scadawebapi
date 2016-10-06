using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class _Define
    {
        public const string UongBi_ID = "UONGBIMR";
        public const string BanVe_ID = "BANVE";
        public const string DaiNinh_ID = "DAININH";
        public const string DongNai3_ID = "DONGNAI3";
        public const string DongNai4_ID = "DONGNAI4";
        public const string SongTranh2_ID = "SONGTRANH2";

        public const string strNameGenParams = "Dòng điện, điện áp máy phát";
        public const string strNameGenRotor = "Rotor";
        public const string strNameUab = "Uab (kV)";
        public const string strNameUbc = "Ubc (kV)";
        public const string strNameUca = "Uca (kV)";

        public const string strNameUa = "Ua (kV)";
        public const string strNameUb = "Ub (kV)";
        public const string strNameUc = "Uc (kV)";

        public const string strNameIa = "Ia (A)";
        public const string strNameIb = "Ib (A)";
        public const string strNameIc = "Ic (A)";

        public const string strNameP = "P (MW)";
        public const string strNameQ = "Q (MVar)";
        public const string strNameF = "Tần số (Hz)";

        public const string strNameOilTemp = "Nhiệt độ dầu (C)";
        public const string strNameWindTemp = "Nhiệt độ cuộn dây (C)";

        public const string strNameEnergy = "Sản lượng (MWh)";

        public const string strNameVoltage = "Điện áp (kV)";
        public const string strNameCurrent = "Dòng điện (A)";
        public const string strNameVelocity = "Tốc độ (rpm)";

        public const string strNameTransPrimary = "Sơ cấp";
        public const string strNameTransSecondary1 = "Thứ cấp 1";
        public const string strNameTransSecondary2 = "Thứ cấp 2";

        public static readonly string[] PowerTagNames = new string[]
        {
            "UONGBIMR_907_P", 
            "UONGBIMR_908_P", 
            "BANVE_COM_Ptotal", 
            "DAININH_COM_Ptotal", 
            "DONGNAI3_COM_Ptotal", 
            "DONGNAI4_COM_Ptotal", 
            "SONGTRANH2_COM_Ptotal",
        };
    }
}