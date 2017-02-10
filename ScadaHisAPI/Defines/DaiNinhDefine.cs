using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI
{
    public class DaiNinhDefine
    {

        public static readonly string[] PowerTagNames = new string[]
        {
            "DAININH_931_P", 
            "DAININH_932_P", 
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[2]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy H1",
                Uab = "DAININH_931_U",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "DAININH_931_I",
                Ib = null,
                Ic = null,
                P = "DAININH_931_P",
                Q = "DAININH_931_Q",
                F = "DAININH_931_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy H2",
                Uab = "DAININH_932_U",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "DAININH_932_I",
                Ib = null,
                Ic = null,
                P = "DAININH_932_P",
                Q = "DAININH_932_Q",
                F = "DAININH_932_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
        };

        public static readonly OpReportFeederTagNames[] Feeders = new OpReportFeederTagNames[3]
        {
            new OpReportFeederTagNames
            {
                Name = "271",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = "DAININH_271_I",
                Ib = null,
                Ic = null,
                P = "DAININH_271_P",
                Q = "DAININH_271_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "272",
                Uab = "DAININH_272_U",
                Ubc = null,
                Uca = null,
                Ia = "DAININH_272_I",
                Ib = null,
                Ic = null,
                P = "DAININH_272_P",
                Q = "DAININH_272_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "173",
                Uab = null,
                Ubc = null,
                Uca = null,
                Ia = "DAININH_173_I",
                Ib = null,
                Ic = null,
                P = "DAININH_173_P",
                Q = "DAININH_173_Q",
                F = null,
            }
        };

        public static readonly string[] U_Generators = new string[]
        {
            "DAININH_931_U", 
            "DAININH_932_U", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "DAININH_931_I", 
            "DAININH_932_I", 
        };

        public static readonly string[] P_Generators = PowerTagNames;

    }
}