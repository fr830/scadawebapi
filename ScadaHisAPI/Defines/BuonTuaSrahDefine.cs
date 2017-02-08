using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class BuonTuaSrahDefine
    {
        public static readonly string[] PowerTagNames = new string[]
        {
            "H1_Device1_KW", 
            "H2_Device1_KW",
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
                P = "H1_Device1_KW",
                Q = "H1_Device1_KVAr",
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
                P = "H2_Device1_KW",
                Q = "H2_Device1_KVAr",
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
                P = "BTS_DN_DZ272_DN_KW",
                Q = "BTS_DN_DZ272_DN_KVAr",
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
                P = "BTS_BK_DZ274_BK_KW",
                Q = "BTS_BK_DZ274_BK_KVAr",
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