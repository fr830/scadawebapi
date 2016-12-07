using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class MongDuong1Define
    {
        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy S1",
                Uab = "MONGDUONG1_S1_U",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "MONGDUONG1_S1_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_S1_P",
                Q = "MONGDUONG1_S1_Q",
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy S2",
                Uab = "MONGDUONG1_S2_U",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "MONGDUONG1_S2_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_S2_P",
                Q = "MONGDUONG1_S2_Q",
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
                Uab = "MONGDUONG1_571_U",
                Ubc = null,
                Uca = null,
                Ia = "MONGDUONG1_571_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_571_P",
                Q = "MONGDUONG1_571_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "573",
                Uab = "MONGDUONG1_573_U",
                Ubc = null,
                Uca = null,
                Ia = "MONGDUONG1_573_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_573_P",
                Q = "MONGDUONG1_573_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "171",
                Uab = "MONGDUONG1_171_U",
                Ubc = null,
                Uca = null,
                Ia = "MONGDUONG1_171_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_171_P",
                Q = "MONGDUONG1_171_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "172",
                Uab = "MONGDUONG1_172_U",
                Ubc = null,
                Uca = null,
                Ia = "BMONGDUONG1_172_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_172_P",
                Q = "MONGDUONG1_172_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "131",
                Uab = "MONGDUONG1_131_U",
                Ubc = null,
                Uca = null,
                Ia = "MONGDUONG1_131_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_131_P",
                Q = "MONGDUONG1_131_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "132",
                Uab = "MONGDUONG1_132_U",
                Ubc = null,
                Uca = null,
                Ia = "MONGDUONG1_132_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_132_P",
                Q = "MONGDUONG1_132_Q",
                F = null,
            }
        };

        public static readonly OpReportTransformerTagNames[] Transformers = new OpReportTransformerTagNames[]
        {
            new OpReportTransformerTagNames
            {
                Name = "T1",
                Ua = "MONGDUONG1_132_U",
                Ub = null,
                Uc = null,
                Ia = "MONGDUONG1_132_I",
                Ib = null,
                Ic = null,
                TAP = "MONGDUONG1_132_P",
                OilTemp = "MONGDUONG1_132_Q",
                WindTemp = null,
            }
        };


        public static readonly string[] U_Generators = new string[]
        {
            "MONGDUONG1_S1_U", 
            "MONGDUONG1_S2_U", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "MONGDUONG1_S1_I", 
            "MONGDUONG1_S2_I", 
        };

        public static readonly string[] P_Generators = new string[]
        {
            "MONGDUONG1_S1_P", 
            "MONGDUONG1_S2_P", 
        };
    }
}