using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScadaHisAPI
{
    public class AnalogLiveController : ApiController
    {
        // GET api/AnalogLive/{TagName}
        [Route("AnalogLive/{TagName}")]
        [HttpGet]
        public DataPoint Get(string TagName)
        {
            return ScadaHisDao.AnalogLive(new string[] { TagName }).FirstOrDefault();
        }

        // POST api/AnalogLive
        [Route("AnalogLive")]
        [HttpPost]
        public IEnumerable<DataPoint> Post([FromBody]string[] TagNameList)
        {
            var datalist = ScadaHisDao.AnalogLive(TagNameList);

            List<DataPoint> result = datalist.ToList();

            if (TagNameList.Contains("PTOTAL_GENCO3"))
            {
                datalist = datalist.Union(ScadaHisDao.AnalogLive(EnergyUtils.FactoryToPowerTag(new string[] { _Define.PhuMy22_ID }, null)));

                result.AddRange(Genco3HMI(datalist, GroupHMIType.Genco3HMI));
            }
            else if (TagNameList.Contains("PTOTAL_BK110KV_MW"))
            {
                result.AddRange(Genco3HMI(datalist, GroupHMIType.BuonKuopHMI));
            }
            else if (TagNameList.Contains("PTOTAL_PM1_MBA"))
            {
                result.AddRange(Genco3HMI(datalist, GroupHMIType.PhuMyHMI));
            }

            return result;
        }

        // POST api/PowerLive/Power
        [Route("AnalogLive/Power")]
        [HttpPost]
        public IEnumerable<DataPoint> Power([FromBody]string[] FactoryList)
        {
            List<DataPoint> result = new List<DataPoint>();

            string[] TagNameList = EnergyUtils.FactoryToPowerTag(FactoryList, null);
            var power = ScadaHisDao.AnalogLive(TagNameList);

            foreach (string factory in FactoryList)
            {
                if (factory != null)
                {
                    string[] PowerList = EnergyUtils.FactoryToPowerTag(new string[] { factory }, null);

                    result.Add(DataPointSum(power, PowerList, factory));
                }
            }

            return result;
        }

        private DataPoint DataPointSum(IEnumerable<DataPoint> datalist, string[] TagNameList, string NewTagName)
        {
            DataPoint sum = new DataPoint() { DateTime = DateTime.Now, TagName = NewTagName, Value = null, OPCQuality = 0 };

            var data = (from p in datalist where TagNameList.Contains(p.TagName) && p.OPCQuality >= 192 select p);

            if (data.Count<DataPoint>() > 0)
            {
                sum.DateTime = data.FirstOrDefault().DateTime;
                sum.Value = (from p in data where p.Value.HasValue select p.Value).Sum();
                sum.OPCQuality = data.FirstOrDefault().OPCQuality;
            }

            return sum;
        }

        private enum GroupHMIType
        {
            PhuMyHMI = 1,
            BuonKuopHMI = 2,
            Genco3HMI = 3,
        };

        private List<DataPoint> Genco3HMI(IEnumerable<DataPoint> datalist, GroupHMIType code)
        {
            List<DataPoint> result = new List<DataPoint>();

            /* Apply for Phu My & Genco3 HMI */
            if (((int)code & (int)GroupHMIType.PhuMyHMI) != 0)
            {
                /* Phu My */
                result.Add(DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.PhuMy1_ID }, null), "PTOTAL_PM1"));

                result.Add(DataPointSum(datalist, new string[] { "PMG_GT21_MW", "PMG_GT22_MW", "PMG_ST23_MW" }, "PTOTAL_PM21"));

                result.Add(DataPointSum(datalist, new string[] { "PMG_GT24_MW", "PMG_GT25_MW", "PMG_ST26_MW" }, "PTOTAL_PM21_MR"));

                result.Add(DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.PhuMy4_ID }, null), "PTOTAL_PM4"));

                result.Add(DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.PhuMy22_ID }, null), "PTOTAL_PM22"));

                result.Add(DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.PhuMy3_ID }, null), "PTOTAL_PM3"));

                /* Clone PMG_BR_ST9_MW */
                result.Add(DataPointSum(datalist, new string[] { "PMG_BR_ST9_MW"}, "PMG_BR_ST10_MW"));
            }

            /* Apply for BuonKuop & Genco3 */
            if (((int)code & (int)GroupHMIType.BuonKuopHMI) != 0)
            {
                /* Buon Kuop */
                result.Add(DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.BuonKuop_ID }, null), "PTOTAL_BUONKUOP"));

                result.Add(DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.BuonTuaSrah_ID }, null), "PTOTAL_BUONTUASRAH"));

                result.Add(DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.Srepok3_ID }, null), "PTOTAL_SREPOK3"));
            }

            /* Apply for Genco3 Only */
            if (code == GroupHMIType.Genco3HMI)
            {
                result.Add(DataPointSum(result, new string[] { "PTOTAL_PM1", "PTOTAL_PM21", "PTOTAL_PM21_MR", "PTOTAL_PM4" }, "PTOTAL_PHUMY"));

                result.Add(DataPointSum(result, new string[] { "PTOTAL_PHUMY", "PTOTAL_PM3", "PTOTAL_PM22" }, "PTOTAL_PM_COMPLEX"));

                /* Phu My - Ba Ria Complex */
                result.Add(DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.BaRia_ID }, null), "PTOTAL_BR"));

                result.Add(DataPointSum(result, new string[] { "PTOTAL_PM_COMPLEX", "PTOTAL_BR" }, "PTOTAL_PM_BR_COMPLEX"));

                result.Add(DataPointSum(result, new string[] { "PTOTAL_BUONKUOP", "PTOTAL_BUONTUASRAH", "PTOTAL_SREPOK3" }, "PTOTAL_BUONKUOP_GROUP"));

                /* Vinh Tan - Mong Duong */
                DataPoint dp = DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.VinhTan2_ID }, null), "PTOTAL_VINHTAN2");
                result.Add(dp);
                /* Clone Vinh Tan 2 Ptotal */
                result.Add(new DataPoint
                {
                    DateTime = dp.DateTime,
                    TagName = "PTOTAL_VINHTAN2_2",
                    Value = dp.Value,
                    OPCQuality = dp.OPCQuality,
                });


                dp = DataPointSum(datalist, EnergyUtils.FactoryToPowerTag(new string[] { _Define.MongDuong1_ID }, null), "PTOTAL_MONGDUONG1");
                result.Add(dp);
                /* Cloning */
                result.Add(new DataPoint
                {
                    DateTime = dp.DateTime,
                    TagName = "PTOTAL_MONGDUONG1_2",
                    Value = dp.Value,
                    OPCQuality = dp.OPCQuality,
                });

                /* Genco3 Total */
                result.Add(DataPointSum(result, new string[] { "PTOTAL_PHUMY", "PTOTAL_VINHTAN2", "PTOTAL_MONGDUONG1", "PTOTAL_BUONKUOP_GROUP" }, "PTOTAL_GENCO3"));
            }

            /* Apply for BuonKuop Group Only */
            if (code == GroupHMIType.BuonKuopHMI)
            {
                result.Add(DataPointSum(datalist, new string[] { "BK172_172_172_KW", "BK171_171_171_KW" }, "PTOTAL_BK110KV_MW"));
            }

            /* Apply for PhuMy Group Only */
            if (code == GroupHMIType.PhuMyHMI)
            {
                result.Add(DataPointSum(datalist, new string[] { "PMG_BR_GT1_MW", "PMG_BR_GT2_MW", "PMG_BR_GT3_MW", "PMG_BR_GT4_MW", "PMG_BR_GT8_MW" }, "PTOTAL_BR1"));

                result.Add(DataPointSum(datalist, new string[] { "PMG_BR_GT5_MW", "PMG_BR_GT6_MW", "PMG_BR_GT7_MW", "PMG_BR_ST9_MW" }, "PTOTAL_BR2"));

                result.Add(DataPointSum(datalist, new string[] { "PMG_GT11_MBA_MW", "PMG_GT12_MBA_MW", "PMG_GT13_MBA_MW", "PMG_ST14_MBA_MW" }, "PTOTAL_PM1_MBA"));

                result.Add(DataPointSum(datalist, new string[] { "PMG_GSUT41_MW", "PMG_GSUT42_MW", "PMG_GSUT43_MW" }, "PTOTAL_PM4_MBA"));

                result.Add(DataPointSum(datalist, new string[] { "PMG_PM3_GT1_MBA_MW", "PMG_PM3_GT2_MBA_MW", "PMG_PM3_ST3_MBA_MW" }, "PTOTAL_PM3_MBA"));

                result.Add(DataPointSum(datalist, new string[] { "PMG_GT21_MBA_MW", "PMG_GT22_MBA_MW", "PMG_ST23_MBA_MW", "PMG_GT24_MBA_MW", "PMG_GT25_MBA_MW", "PMG_ST26_MBA_MW" }, "PTOTAL_PM21_MBA"));
            }

            return result;
        }
    }
}