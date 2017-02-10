using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class DongNai3Define
    {

        public static readonly string[] PowerTagNames = new string[]
        {
            "DONGNAI3_901_P", 
            "DONGNAI3_902_P",
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[2]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy H1",
                Uab = "DONGNAI3_901_Uab",
                Ubc = "DONGNAI3_901_Ubc",
                Uca = "DONGNAI3_901_Uca",
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "DONGNAI3_901_Ia",
                Ib = "DONGNAI3_901_Ib",
                Ic = "DONGNAI3_901_Ic",
                P = "DONGNAI3_901_P",
                Q = "DONGNAI3_901_Q",
                F = "DONGNAI3_901_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy H2",
                Uab = "DONGNAI3_902_Uab",
                Ubc = "DONGNAI3_902_Ubc",
                Uca = "DONGNAI3_902_Uca",
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "DONGNAI3_902_Ia",
                Ib = "DONGNAI3_902_Ib",
                Ic = "DONGNAI3_902_Ic",
                P = "DONGNAI3_902_P",
                Q = "DONGNAI3_902_Q",
                F = "DONGNAI3_902_F",
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
                Uab = "DONGNAI3_273_Uab",
                Ubc = "DONGNAI3_273_Ubc",
                Uca = "DONGNAI3_273_Uca",
                Ia = "DONGNAI3_273_Ia",
                Ib = "DONGNAI3_273_Ib",
                Ic = "DONGNAI3_273_Ic",
                P = "DONGNAI3_273_P",
                Q = "DONGNAI3_273_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "274",
                Uab = "DONGNAI3_274_Uab",
                Ubc = "DONGNAI3_274_Ubc",
                Uca = "DONGNAI3_274_Uca",
                Ia = "DONGNAI3_274_Ia",
                Ib = "DONGNAI3_274_Ib",
                Ic = "DONGNAI3_274_Ic",
                P = "DONGNAI3_274_P",
                Q = "DONGNAI3_274_Q",
                F = null,
            }
        };

        public static readonly string[] U_Generators = new string[]
        {
            "DONGNAI3_901_Uab", 
            "DONGNAI3_902_Uab", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "DONGNAI3_901_Ia", 
            "DONGNAI3_902_Ia", 
        };

        public static readonly string[] P_Generators = PowerTagNames;
    }
}