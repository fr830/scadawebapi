using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaHisAPI.Defines
{
    public class VinhTan2Define
    {
        public static readonly string[] PowerTagNames = new string[]
        {
            "VINHTAN2_901_P", 
            "VINHTAN2_902_P",
        };

        public static readonly OpReportGeneratorTagNames[] Generators = new OpReportGeneratorTagNames[]
        {
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy G1",
                Uab = "VINHTAN2_901_U",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "VINHTAN2_901_I",
                Ib = null,
                Ic = null,
                P = "VINHTAN2_901_P",
                Q = "VINHTAN2_901_Q",
                F = "VINHTAN2_901_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            },
            new OpReportGeneratorTagNames
            {
                Name = "Tổ máy G2",
                Uab = "VINHTAN2_902_U",
                Ubc = null,
                Uca = null,
                //Ua = null,
                //Ub = null,
                //Uc = null,
                Ia = "VINHTAN2_902_I",
                Ib = null,
                Ic = null,
                P = "VINHTAN2_902_P",
                Q = "VINHTAN2_902_Q",
                F = "VINHTAN2_902_F",
                URotor = null,
                IRotor = null,
                VRotor = null,
            }
        };

        public static readonly OpReportFeederTagNames[] Feeders = new OpReportFeederTagNames[]
        {
            new OpReportFeederTagNames
            {
                Name = "641",
                Uab = "VINHTAN2_641_U",
                Ubc = null,
                Uca = null,
                Ia = "VINHTAN2_641_I",
                Ib = null,
                Ic = null,
                P = "VINHTAN2_641_P",
                Q = "VINHTAN2_641_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "642",
                Uab = "VINHTAN2_642_U",
                Ubc = null,
                Uca = null,
                Ia = "VINHTAN2_642_I",
                Ib = null,
                Ic = null,
                P = "VINHTAN2_642_P",
                Q = "VINHTAN2_642_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "643",
                Uab = "VINHTAN2_643_U",
                Ubc = null,
                Uca = null,
                Ia = "VINHTAN2_643_I",
                Ib = null,
                Ic = null,
                P = "VINHTAN2_643_P",
                Q = "VINHTAN2_643_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "644",
                Uab = "VINHTAN2_644_U",
                Ubc = null,
                Uca = null,
                Ia = "VINHTAN2_644_I",
                Ib = null,
                Ic = null,
                P = "VINHTAN2_644_P",
                Q = "VINHTAN2_644_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "645",
                Uab = "VINHTAN2_645_U",
                Ubc = null,
                Uca = null,
                Ia = "VINHTAN2_645_I",
                Ib = null,
                Ic = null,
                P = "VINHTAN2_645_P",
                Q = "VINHTAN2_645_Q",
                F = null,
            },
            new OpReportFeederTagNames
            {
                Name = "646",
                Uab = "VINHTAN2_646_U",
                Ubc = null,
                Uca = null,
                Ia = "VINHTAN2_646_I",
                Ib = null,
                Ic = null,
                P = "VINHTAN2_646_P",
                Q = "VINHTAN2_646_Q",
                F = null,
            }
        };

        public static readonly string[] U_Generators = new string[]
        {
            "VINHTAN2_901_U", 
            "VINHTAN2_902_U", 
        };

        public static readonly string[] I_Generators = new string[]
        {
            "VINHTAN2_901_I", 
            "VINHTAN2_902_I", 
        };

        public static readonly string[] P_Generators = PowerTagNames;
    }
}