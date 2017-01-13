using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class PhuMy4Define
    {
        public static readonly string[] PowerTagNames = new string[]
        {
            "PMG_GT41_MW", 
            "PMG_GT42_MW",
            "PMG_ST43_MW",
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy GT41",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_GT41_MW",
                Q = null,
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy GT42",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_GT42_MW",
                Q = null,
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy ST43",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_ST43_MW",
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
                Name = "571",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM4_LINE2_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "573",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM4_LINE1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "GSUT41",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_GSUT41_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "GSUT42",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_GSUT42_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "GSUT43",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_GSUT43_MW",
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