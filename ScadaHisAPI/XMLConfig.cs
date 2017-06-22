using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ScadaHisAPI
{
    public class XMLConfig
    {
        private static string GetXMLConfigFile(string fileName)
        {
            string configDirectory = HttpContext.Current.Server.MapPath("~") + "\\Config";

            return configDirectory + "\\" + fileName + ".xml";
        }

        public static List<OpReportGeneratorTagNames> GetGeneratorTagNames(string FactoryName)
        {
            string xmlpath = GetXMLConfigFile(FactoryName);

            try
            {
                XElement content = XElement.Load(xmlpath);

                List<OpReportGeneratorTagNames> result =
                    (from x in content.Element("Generators").Elements("Generator")
                     select new OpReportGeneratorTagNames()
                     {
                         Name = x.Element("Name").Value,
                         Uab = x.Element("Uab").Value,
                         Ubc = x.Element("Ubc").Value,
                         Uca = x.Element("Uca").Value,
                         Ua = x.Element("Ua").Value,
                         Ub = x.Element("Ub").Value,
                         Uc = x.Element("Uc").Value,
                         Ia = x.Element("Ia").Value,
                         Ib = x.Element("Ib").Value,
                         Ic = x.Element("Ic").Value,
                         P = x.Element("P").Value,
                         Q = x.Element("Q").Value,
                         F = x.Element("F").Value,
                         IRotor = x.Element("IRotor").Value,
                         URotor = x.Element("URotor").Value,
                         VRotor = x.Element("VRotor").Value,
                     }).ToList();

                return result;
            }
            catch (Exception)
            {
                return new List<OpReportGeneratorTagNames>();
            }
        }

        public static List<OpReportFeederTagNames> GetFeederTagNames(string FactoryName)
        {
            string xmlpath = GetXMLConfigFile(FactoryName);

            try
            {
                XElement content = XElement.Load(xmlpath);

                List<OpReportFeederTagNames> result =
                    (from x in content.Element("Feeders").Elements("Feeder")
                     select new OpReportFeederTagNames()
                     {
                         Name = x.Element("Name").Value,
                         Uab = x.Element("Uab").Value,
                         Ubc = x.Element("Ubc").Value,
                         Uca = x.Element("Uca").Value,
                         Ua = x.Element("Ua").Value,
                         Ub = x.Element("Ub").Value,
                         Uc = x.Element("Uc").Value,
                         Ia = x.Element("Ia").Value,
                         Ib = x.Element("Ib").Value,
                         Ic = x.Element("Ic").Value,
                         P = x.Element("P").Value,
                         Q = x.Element("Q").Value,
                         F = x.Element("F").Value,
                     }).ToList();

                return result;
            }
            catch (Exception)
            {
                return new List<OpReportFeederTagNames>();
            }
        }

        public static List<string> GetEnergySources(string FactoryName)
        {
            string xmlpath = GetXMLConfigFile(FactoryName);

            try
            {
                XElement content = XElement.Load(xmlpath);

                List<string> result = content.Element("EnergySources").Elements().Select(s => (string)s).ToList();

                return result;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public static List<string> GetUList(string FactoryName)
        {
            string xmlpath = GetXMLConfigFile(FactoryName);

            try
            {
                XElement content = XElement.Load(xmlpath);

                List<string> result = content.Element("U_Generators").Elements().Select(s => (string)s).ToList();

                return result;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public static List<string> GetIList(string FactoryName)
        {
            string xmlpath = GetXMLConfigFile(FactoryName);

            try
            {
                XElement content = XElement.Load(xmlpath);

                List<string> result = content.Element("I_Generators").Elements().Select(s => (string)s).ToList();

                return result;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public static List<string> GetPList(string FactoryName)
        {
            string xmlpath = GetXMLConfigFile(FactoryName);

            try
            {
                XElement content = XElement.Load(xmlpath);

                List<string> result = content.Element("P_Generators").Elements().Select(s => (string)s).ToList();

                return result;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public static OpReportTitle GetReportTitle()
        {
            string xmlpath = GetXMLConfigFile("_General");

            try
            {
                XElement content = XElement.Load(xmlpath);

                XElement x = content.Element("ReportTitle");

                OpReportTitle result = new OpReportTitle
                {
                    strNameGenParams = x.Element("Parameters").Value,
                    strNameGenRotor = x.Element("Rotor").Value,

                    strNameUab = x.Element("Uab").Value,
                    strNameUbc = x.Element("Ubc").Value,
                    strNameUca = x.Element("Uca").Value,

                    strNameUa = x.Element("Ua").Value,
                    strNameUb = x.Element("Ub").Value,
                    strNameUc = x.Element("Uc").Value,

                    strNameIa = x.Element("Ia").Value,
                    strNameIb = x.Element("Ib").Value,
                    strNameIc = x.Element("Ic").Value,

                    strNameP = x.Element("P").Value,
                    strNameQ = x.Element("Q").Value,
                    strNameF = x.Element("F").Value,

                    strNameTAP = x.Element("TAP").Value,
                    strNameOilTemp = x.Element("OilTemp").Value,
                    strNameWindTemp = x.Element("WindTemp").Value,

                    strNameEnergy= x.Element("Energy").Value,

                    strNameVoltage = x.Element("Voltage").Value,
                    strNameCurrent = x.Element("Current").Value,
                    strNameVelocity = x.Element("Velocity").Value,

                    strNameTransPrimary = x.Element("TransPrimary").Value,
                    strNameTransSecondary1 = x.Element("TransSecondary1").Value,
                    strNameTransSecondary2 = x.Element("TransSecondary2").Value,
                };

                return result;
            }
            catch (Exception)
            {
                return new OpReportTitle();
            }
        }

        public static int DiscreteLiveQuality()
        {
            try
            {
                string xmlpath = GetXMLConfigFile("_General");

                XElement content = XElement.Load(xmlpath);

                XElement x = content.Element("QualityCodeAcceptable");
                return Convert.ToInt32(x.Element("DiscreteLive").Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int AnalogLiveQuality()
        {
            try
            {
                string xmlpath = GetXMLConfigFile("_General");

                XElement content = XElement.Load(xmlpath);

                XElement x = content.Element("QualityCodeAcceptable");
                return Convert.ToInt32(x.Element("AnalogLive").Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int AnalogHistoryQuality()
        {
            try
            {
                string xmlpath = GetXMLConfigFile("_General");

                XElement content = XElement.Load(xmlpath);

                XElement x = content.Element("QualityCodeAcceptable");
                return Convert.ToInt32(x.Element("AnalogHistory").Value);
            }
            catch (Exception)
            {
                return 192;
            }
        }

        public static int AnalogHistorySummaryQuality()
        {
            try
            {
                string xmlpath = GetXMLConfigFile("_General");

                XElement content = XElement.Load(xmlpath);

                XElement x = content.Element("QualityCodeAcceptable");
                return Convert.ToInt32(x.Element("AnalogHistorySummary").Value);
            }
            catch (Exception)
            {
                return 192;
            }
        }
    }
}