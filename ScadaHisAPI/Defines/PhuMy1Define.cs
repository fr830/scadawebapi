using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class PhuMy1Define
    {
        public static readonly string[] PowerTagNames = new string[]
        {
            "PMG_GT11_MW", 
            "PMG_GT12_MW",
            "PMG_GT13_MW",
            "PMG_ST14_MW",
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy GT11",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_GT11_MW",
                Q = null,
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy GT12",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_GT12_MW",
                Q = null,
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy GT13",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_GT13_MW",
                Q = null,
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy ST14",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_ST14_MW",
                Q = null,
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            }
        };

        public static readonly OpReportFeederTagNames[] Feeders = new OpReportFeederTagNames[]
        {
            new OpReportFeederTagNames
            {
                Name = "110kV BARIA 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_110KV_BR1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "110kV BARIA 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_110KV_BR2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "110kV LONG THANH 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_110KV_LT1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "110kV LONG THANH 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_110KV_LT2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "110kV PHUMY 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_110KV_PM_LINE1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "110kV PHUMY 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_110KV_PM_LINE2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "110kV THI VAI 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_110KV_TV1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "110kV THI VAI 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_110KV_TV2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV BARIA 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_BR1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV BARIA 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_BR2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV CAI LAY 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_CL1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV CAI LAY 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_CL2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV CAT LAI 3",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_CLAI1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV CAT LAI 4",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_CLAI2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV LONG THANH 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_LT1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV LONG THANH 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_LT2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV NHA BE 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_NB1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV NHA BE 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_NB2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV PHUMY 2.1 1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_PM21_LINE1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "220kV PHUMY 2.1 2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM1_220KV_PM21_LINE2_MW",
                Q = null,
                F = null,
            },
        };

        public static readonly OpReportTransformerTagNames[] Transformers = new OpReportTransformerTagNames[]
        {
        };


        public static readonly string[] U_Generators = new string[]
        {
        };

        public static readonly string[] I_Generators = new string[]
        {
        };

        public static readonly string[] P_Generators = PowerTagNames;
    }
}