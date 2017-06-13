using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ScadaHisAPI
{
    public class OpReportUtils
    {
        private static string GetFactoryConfigFile(string FactoryName)
        {
            string configDirectory = HttpContext.Current.Server.MapPath("~") + "\\Config";
            string generalConfig = configDirectory + "\\_General.xml";

            XElement content = XElement.Load(generalConfig);



            return configDirectory + "\\" + FactoryName + ".xml";
        }

        //public static List<string> GetFactoryList()
        //{

        //}

        public static List<OpReportGeneratorTagNames> GetGeneratorTagNames(string FactoryName)
        {
            string xmlpath = GetFactoryConfigFile(FactoryName);

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

        public static List<OpReportFeederTagNames> GetFeederTagNames(string FactoryName)
        {
            string xmlpath = GetFactoryConfigFile(FactoryName);

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

        public static List<string> GetEnergySources(string FactoryName)
        {
            string xmlpath = GetFactoryConfigFile(FactoryName);

            XElement content = XElement.Load(xmlpath);

            List<string> result = content.Element("EnergySources").Elements().Select(s => (string)s).ToList();

            return result;
        }

        public static List<string> GetUList(string FactoryName)
        {
            string xmlpath = GetFactoryConfigFile(FactoryName);

            XElement content = XElement.Load(xmlpath);

            List<string> result = content.Element("U_Generators").Elements().Select(s => (string)s).ToList();

            return result;
        }

        public static List<string> GetIList(string FactoryName)
        {
            string xmlpath = GetFactoryConfigFile(FactoryName);

            XElement content = XElement.Load(xmlpath);

            List<string> result = content.Element("I_Generators").Elements().Select(s => (string)s).ToList();

            return result;
        }

        public static List<string> GetPList(string FactoryName)
        {
            string xmlpath = GetFactoryConfigFile(FactoryName);

            XElement content = XElement.Load(xmlpath);

            List<string> result = content.Element("P_Generators").Elements().Select(s => (string)s).ToList();

            return result;
        }

        public static double? GetHourlyValues(string TagName, IEnumerable<DataPoint> datalist)
        {
            if (TagName == null || datalist.Count() == 0) return null;

            return (from p in datalist where p.TagName == TagName select (p.Value != null ? (double?)Math.Round((double)p.Value, 2) : 0)).FirstOrDefault();
        }
   }
}