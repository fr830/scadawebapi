using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class MongDuong1Define
    {
        public static readonly string[] PowerTagNames = new string[]
        {
            "MONGDUONG1_901_P", 
            "MONGDUONG1_902_P",
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy S1",
                Uab = "MONGDUONG1_901_U",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "MONGDUONG1_901_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_901_P",
                Q = "MONGDUONG1_901_Q",
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy S2",
                Uab = "MONGDUONG1_902_U",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "MONGDUONG1_902_I",
                Ib = null,
                Ic = null,
                P = "MONGDUONG1_902_P",
                Q = "MONGDUONG1_902_Q",
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
                Ia = "MONGDUONG1_172_I",
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
                Ua = "MONGDUONG1_T1_U",
                Ub = null,
                Uc = null,
                Ia = "MONGDUONG1_T1_I",
                Ib = null,
                Ic = null,
                TAP = "MONGDUONG1_T1_P",
                OilTemp = "MONGDUONG1_T1_Q",
                WindTemp = null,
            }
        };


        public static readonly string[] U_Generators = new string[]
        {
            "MONGDUONG1_901_U", 
            "MONGDUONG1_902_U", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "MONGDUONG1_901_I", 
            "MONGDUONG1_902_I", 
        };

        public static readonly string[] P_Generators = PowerTagNames;
    }
}