using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class BanVeDefine
    {
        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[2]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy G1",
                Uab = "BANVE_901_Uab",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "BANVE_901_Ia",
                Ib = null,
                Ic = null,
                P = "BANVE_901_P",
                Q = "BANVE_901_Q",
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy G2",
                Uab = "BANVE_902_Uab",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "BANVE_902_Ia",
                Ib = null,
                Ic = null,
                P = "BANVE_902_P",
                Q = "BANVE_902_Q",
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            }
        };

        public static readonly OpReportFeederTagNames[] Feeders = new OpReportFeederTagNames[2]
        {
            new OpReportFeederTagNames
            {
                Name = "271",
                Uab = "BANVE_271_Uab",
                Ubc = null,
                Uca = null,
                Ia = "BANVE_271_Ia",
                Ib = null,
                Ic = null,
                P = "BANVE_271_P",
                Q = "BANVE_271_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "272",
                Uab = "BANVE_272_Uab",
                Ubc = null,
                Uca = null,
                Ia = "BANVE_272_Ia",
                Ib = null,
                Ic = null,
                P = "BANVE_272_P",
                Q = "BANVE_272_Q",
                F = null,
            }
        };

        public static readonly string[] U_Generators = new string[]
        {
            "BANVE_901_Uab", 
            "BANVE_902_Uab", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "BANVE_901_Ia", 
            "BANVE_902_Ia", 
        };

        public static readonly string[] P_Generators = new string[]
        {
            "BANVE_901_P", 
            "BANVE_902_P", 
        };
    }
}