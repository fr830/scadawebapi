using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class DongNai4Define
    {

        public static readonly string[] PowerTagNames = new string[]
        {
            "DONGNAI4_COM_Ptotal",
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[2]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy H1",
                Uab = "DONGNAI4_901_Uab",
                Ubc = "DONGNAI4_901_Ubc",
                Uca = "DONGNAI4_901_Uca",
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "DONGNAI4_901_Ia",
                Ib = "DONGNAI4_901_Ib",
                Ic = "DONGNAI4_901_Ic",
                P = "DONGNAI4_901_P",
                Q = "DONGNAI4_901_Q",
                F = "DONGNAI4_901_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy H2",
                Uab = "DONGNAI4_902_Uab",
                Ubc = "DONGNAI4_902_Ubc",
                Uca = "DONGNAI4_902_Uca",
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "DONGNAI4_902_Ia",
                Ib = "DONGNAI4_902_Ib",
                Ic = "DONGNAI4_902_Ic",
                P = "DONGNAI4_902_P",
                Q = "DONGNAI4_902_Q",
                F = "DONGNAI4_902_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
        };

        public static readonly OpReportFeederTagNames[] Feeders = new OpReportFeederTagNames[2]
        {
            new OpReportFeederTagNames
            {
                Name = "273",
                Uab = "DONGNAI4_273_Uab",
                Ubc = "DONGNAI4_273_Ubc",
                Uca = "DONGNAI4_273_Uca",
                Ia = "DONGNAI4_273_Ia",
                Ib = "DONGNAI4_273_Ib",
                Ic = "DONGNAI4_273_Ic",
                P = "DONGNAI4_273_P",
                Q = "DONGNAI4_273_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "274",
                Uab = "DONGNAI4_274_Uab",
                Ubc = "DONGNAI4_274_Ubc",
                Uca = "DONGNAI4_274_Uca",
                Ia = "DONGNAI4_274_Ia",
                Ib = "DONGNAI4_274_Ib",
                Ic = "DONGNAI4_274_Ic",
                P = "DONGNAI4_274_P",
                Q = "DONGNAI4_274_Q",
                F = null,
            }
        };

        public static readonly string[] U_Generators = new string[]
        {
            "DONGNAI4_901_Uab", 
            "DONGNAI4_902_Uab", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "DONGNAI4_901_Ia", 
            "DONGNAI4_902_Ia", 
        };

        public static readonly string[] P_Generators = new string[]
        {
            "DONGNAI4_901_P", 
            "DONGNAI4_902_P",
        };

    }
}