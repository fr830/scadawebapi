using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class Srepok3Define
    {
        public static readonly string[] PowerTagNames = new string[]
        {
            "SP3_H1_H1_H1_KW", 
            "SP3_H2_H2_H2_KW",
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy H1",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "SP3_H1_H1_H1_KW",
                Q = "SP3_H1_H1_H1_KVAr",
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy H2",
                Uab = null,
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "SP3_H2_H2_H2_KW",
                Q = "SP3_H2_H2_H2_KVAr",
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
                Name = "272",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "SWY_SP4_SP4_KW",
                Q = "SWY_SP4_SP4_KVAr",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "274",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "SWY_BK_BK_KW",
                Q = "SWY_BK_BK_KVAr",
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