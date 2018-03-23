using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
using System.Configuration;
using System.Diagnostics;
using Microsoft.AspNet.SignalR;

namespace ScadaSignalR
{
    public class OPCSignalR : SignalRBase
    {
        private string OPCServer = ConfigurationManager.AppSettings["OPCServer"];
        private string OPCNode = ConfigurationManager.AppSettings["OPCNode"];

        private List<OPCConfig> _MFITagnames;
        private List<OPCConfig> _DITagnames;
        private OPCServer _opcServer;
        private OPCGroup _opcMFIGroup, _opcDIGroup;
        System.Timers.Timer _reconnectTimer;


        private readonly static Lazy<OPCSignalR> _instance = new Lazy<OPCSignalR>(
            () => new OPCSignalR(GlobalHost.ConnectionManager.GetHubContext<ScadaHub>().Clients));

        public OPCSignalR() : base()
        {
        }

        public OPCSignalR(IHubConnectionContext<dynamic> clients)
            : base(clients)
        {
            Init();
        }

        public static OPCSignalR Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private void Init()
        {
            _reconnectTimer = new System.Timers.Timer(Convert.ToDouble(ConfigurationManager.AppSettings["ReconnectPeriod"]));
            _reconnectTimer.Elapsed += _reconnectTimer_Elapsed;

            _opcServer = new OPCServer();
            _opcServer.ServerShutDown += _opcServer_ServerShutDown;

            ConnectOPC();
        }

        private bool ConnectOPC()
        {
            Array LocalServer = (Array)(object)_opcServer.GetOPCServers(OPCNode);

            foreach (object server in LocalServer)
            {
                if (server.ToString().Equals(OPCServer))
                {
                    _opcServer.Connect(OPCServer, OPCNode);

                    if (_opcServer.ServerState == 1)
                    {
                        _opcMFIGroup = _opcServer.OPCGroups.Add("AnalogLive");
                        _opcMFIGroup.DeadBand = 0;
                        _opcMFIGroup.UpdateRate = 1000;
                        _opcMFIGroup.IsSubscribed = true;
                        _opcMFIGroup.IsActive = true;
                        _MFITagnames = ImportTagfromCSV("MFI.csv", ref _opcMFIGroup);

                        _opcDIGroup = _opcServer.OPCGroups.Add("DiscreteLive");
                        _opcDIGroup.DeadBand = 0;
                        _opcDIGroup.UpdateRate = 1000;
                        _opcDIGroup.IsSubscribed = true;
                        _opcDIGroup.IsActive = true;
                        _DITagnames = ImportTagfromCSV("DI.csv", ref _opcDIGroup);

                        Log.WriteEntry("Connect to OPC Server successfully", EventLogEntryType.Information);
                    }

                    return true;
                }
            }

            Log.WriteEntry("Server is not available, Connect to OPC Server failed !", EventLogEntryType.Information);

            return false;
        }

        private void _opcServer_ServerShutDown(string Reason)
        {
            _reconnectTimer.Enabled = true;

            Log.WriteEntry("Server shutdown", EventLogEntryType.Information);
        }

        private void _reconnectTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ConnectOPC())
            {
                _reconnectTimer.Enabled = false;
            }
        }

        private List<OPCConfig> ImportTagfromCSV(string filename, ref OPCGroup opcGroup)
        {
            List<OPCConfig> cnfgTagnames = new List<OPCConfig>();

            try
            {
                string configDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "Config";
                System.IO.StreamReader readFile = new System.IO.StreamReader(configDirectory + "\\" + filename);
                string line;
                string[] row;

                List<string> itemIDs = new List<string>();

                itemIDs.Add("");

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');

                    OPCConfig tag = new OPCConfig
                    {
                        TagName = row[0],
                        ItemID = row[1],
                        ScaleValue = row.Length > 2 ? Convert.ToDouble(row[2]) : 1,
                    };

                    itemIDs.Add(tag.ItemID);

                    cnfgTagnames.Add(tag);

                }
                readFile.Close();

                int count = itemIDs.Count;

                Array OPCItemIDs = Array.CreateInstance(typeof(string), count);
                Array ItemServerHandles = Array.CreateInstance(typeof(Int32), count);
                Array ItemServerErrors = Array.CreateInstance(typeof(Int32), count);
                Array ClientHandles = Array.CreateInstance(typeof(Int32), count);
                Array RequestedDataTypes = Array.CreateInstance(typeof(Int16), count);
                Array AccessPaths = Array.CreateInstance(typeof(string), count);

                OPCItemIDs = itemIDs.ToArray();

                opcGroup.OPCItems.AddItems(count - 1, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);
                
            }
            catch (Exception ex)
            {
                Log.WriteEntry(ex.Message, EventLogEntryType.Error);
            }

            return cnfgTagnames;
        }

        private DataPoint ReadOPC(OPCGroup opcGroup, OPCConfig opc)
        {
            OPCItem opcItem = null;
            try
            {
                opcItem = opcGroup.OPCItems.Item(opc.ItemID);
            }
            catch { };
            
            if (opcItem != null)
            {
                if (opcItem.Value != null)
                {
                    DataPoint datapoint = new DataPoint
                    {
                        TagName = opc.TagName,
                        Value = (opc.ScaleValue == 1) ? opcItem.Value : (double)opcItem.Value * opc.ScaleValue,
                        OPCQuality = opcItem.Quality,
                        DateTime = opcItem.TimeStamp,
                    };

                    return datapoint;
                }
            }

            return null;
        }

        public override List<DataPoint> GetAnalogLive(List<string> ListTagname)
        {
            List<DataPoint> result = new List<DataPoint>();

            try
            {
                foreach (string tagname in ListTagname)
                {
                    if (tagname == null) break;

                    // check if this tagname is fomular tag which include '+' character
                    // using UnescapeDataString to decode http character
                    DataPoint sum = CheckTagNameOfSum(System.Uri.UnescapeDataString(tagname));

                    if (sum != null)
                    {
                        result.Add(sum);
                    }
                    // This is not a tagname of summary
                    else
                    {
                        OPCConfig opc = _MFITagnames.FirstOrDefault(x => x.TagName == tagname);

                        if (opc == null) break;

                        DataPoint datapoint = ReadOPC(_opcMFIGroup, opc);

                        if (datapoint != null)
                        {
                            result.Add(datapoint);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteEntry("Read OPC Analog:" + ex.Message, EventLogEntryType.Error);
            }

            return result;
        }

        public override List<DataPoint> GetDiscreteLive(List<string> ListTagname)
        {
            List<DataPoint> result = new List<DataPoint>();

            try
            {
                foreach (string tagname in ListTagname)
                {

                    OPCConfig opc = _DITagnames.FirstOrDefault(x => x.TagName == tagname);

                    if (opc == null) break;

                    DataPoint datapoint = ReadOPC(_opcDIGroup, opc);

                    if (datapoint != null)
                    {
                        result.Add(datapoint);
                    }

                }
            }
            catch (Exception ex)
            {
                Log.WriteEntry("Read OPC Discrete:" + ex.Message, EventLogEntryType.Error);
            }

            return result;
        }
    }
}
