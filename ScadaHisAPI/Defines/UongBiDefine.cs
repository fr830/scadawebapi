using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class UongBiDefine
    {
        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[2]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy S7",
                Uab = "UONGBIMR_907_Uab",
                Ubc = "UONGBIMR_907_Ubc",
                Uca = "UONGBIMR_907_Uca",
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "UONGBIMR_907_Ia",
                Ib = "UONGBIMR_907_Ib",
                Ic = "UONGBIMR_907_Ic",
                P = "UONGBIMR_907_P",
                Q = "UONGBIMR_907_Q",
                F = "UONGBIMR_907_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy S8",
                Uab = null,//"UONGBIMR_908_Ua",
                Ubc = null,//"UONGBIMR_908_Ub",
                Uca = null,//"UONGBIMR_908_Uc",
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "UONGBIMR_908_Ia",
                Ib = "UONGBIMR_908_Ib",
                Ic = "UONGBIMR_908_Ic",
                P = "UONGBIMR_908_P",
                Q = "UONGBIMR_908_Q",
                F = "UONGBIMR_908_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            }
        };


        public static readonly OpReportFeederTagNames[] Feeders = new OpReportFeederTagNames[3]
        {
            new OpReportFeederTagNames
            {
                Name = "272",
                Uab = "UONGBIMR_272_U",
                Ubc = null,
                Uca = null,
                Ia = "UONGBIMR_272_Ia",
                Ib = "UONGBIMR_272_Ib",
                Ic = "UONGBIMR_272_Ic",
                P = "UONGBIMR_272_P",
                Q = "UONGBIMR_272_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "273",
                Uab = "UONGBIMR_273_U",
                Ubc = null,
                Uca = null,
                Ia = "UONGBIMR_273_Ia",
                Ib = "UONGBIMR_273_Ib",
                Ic = "UONGBIMR_273_Ic",
                P = "UONGBIMR_273_P",
                Q = "UONGBIMR_273_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "139",
                Uab = "UONGBIMR_139_Uab",
                Ubc = "UONGBIMR_139_Ubc",
                Uca = "UONGBIMR_139_Uca",
                Ia = "UONGBIMR_139_Ia",
                Ib = "UONGBIMR_139_Ib",
                Ic = "UONGBIMR_139_Ic",
                P = "UONGBIMR_139_P",
                Q = "UONGBIMR_139_Q",
                F = null,
            }
        };

        public static readonly string[] U_Generators = new string[]
        {
            "UONGBIMR_907_Uab", 
            "UONGBIMR_908_Ua", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "UONGBIMR_907_Ia", 
            "UONGBIMR_908_Ia", 
        };

        public static readonly string[] P_Generators = new string[]
        {
            "UONGBIMR_907_P", 
            "UONGBIMR_908_P", 
        };
    }
}