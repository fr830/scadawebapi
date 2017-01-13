using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class PhuMy22Define
    {
        public static readonly string[] PowerTagNames = new string[]
        {
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[]
        {

        };

        public static readonly OpReportFeederTagNames[] Feeders = new OpReportFeederTagNames[]
        {   
            new OpReportFeederTagNames
            {
                Name = "Line #1",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM22_LINE1_MW",
                Q = null,
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "Line #2",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = null,
                Ib = null,
                Ic = null,
                P = "PMG_PM22_LINE2_MW",
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