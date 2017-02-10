using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class SongTranh2Define
    {

        public static readonly string[] PowerTagNames = new string[]
        {
            "SONGTRANH2_901_P", 
            "SONGTRANH2_902_P", 
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[2]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy G1",
                Uab = "SONGTRANH2_901_Uab",
                Ubc = "SONGTRANH2_901_Ubc",
                Uca = "SONGTRANH2_901_Uca",
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "SONGTRANH2_901_Ia",
                Ib = "SONGTRANH2_901_Ib",
                Ic = "SONGTRANH2_901_Ic",
                P = "SONGTRANH2_901_P",
                Q = "SONGTRANH2_901_Q",
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy G2",
                Uab = "SONGTRANH2_902_Uab",
                Ubc = "SONGTRANH2_902_Ubc",
                Uca = "SONGTRANH2_902_Uca",
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "SONGTRANH2_902_Ia",
                Ib = "SONGTRANH2_902_Ib",
                Ic = "SONGTRANH2_902_Ic",
                P = "SONGTRANH2_902_P",
                Q = "SONGTRANH2_902_Q",
                F = null,
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
        };

        public static readonly OpReportFeederTagNames[] Feeders = new OpReportFeederTagNames[2]
        {
            new OpReportFeederTagNames
            {
                Name = "271",
                Uab = "SONGTRANH2_271_Uab",
                Ubc = "SONGTRANH2_271_Ubc",
                Uca = "SONGTRANH2_271_Uca",
                Ia = "SONGTRANH2_271_Ia",
                Ib = "SONGTRANH2_271_Ib",
                Ic = "SONGTRANH2_271_Ic",
                P = "SONGTRANH2_271_P",
                Q = "SONGTRANH2_271_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "272",
                Uab = "SONGTRANH2_272_Uab",
                Ubc = "SONGTRANH2_272_Ubc",
                Uca = "SONGTRANH2_272_Uca",
                Ia = "SONGTRANH2_272_Ia",
                Ib = "SONGTRANH2_272_Ib",
                Ic = "SONGTRANH2_272_Ic",
                P = "SONGTRANH2_272_P",
                Q = "SONGTRANH2_272_Q",
                F = null,
            }
        };

        public static readonly string[] U_Generators = new string[]
        {
            "SONGTRANH2_901_Uab", 
            "SONGTRANH2_902_Uab", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "SONGTRANH2_901_Ia", 
            "SONGTRANH2_902_Ia", 
        };

        public static readonly string[] P_Generators = PowerTagNames;

    }
}