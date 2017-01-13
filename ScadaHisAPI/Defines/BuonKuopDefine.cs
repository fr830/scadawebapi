using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class BuonKuopDefine
    {
        public static readonly string[] PowerTagNames = new string[]
        {
            "BK901_901_H1_KW", 
            "BK901_902_H1_KW",
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
                P = "BK901_901_H1_KW",
                Q = "BK901_901_H1_KVAr",
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
                P = "BK901_902_H2_KW",
                Q = "BK901_902_H2_KVAr",
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
                Name = "271",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "BK271_271_271_KW",
                Q = "BK271_271_271_KVAr",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "272",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "BK272_272_272_KW",
                Q = "BK272_272_272_KVAr",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "273",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "BK273_273_273_KW",
                Q = "BK273_273_273_KVAr",
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
                P = "BK273_274_274_KW",
                Q = "BK273_274_274_KVAr",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "171",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "BK171_171_171_KW",
                Q = "BK171_171_171_KVAr",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "172",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "BK172_172_172_KW",
                Q = "BK172_172_172_KVAr",
                F = null,
            }
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