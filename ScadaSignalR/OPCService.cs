using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;

namespace ScadaSignalR
{
    public partial class OPCService : ServiceBase
    {
        private OPCServer _opcServer;
        private OPCGroup _opcMFIGroup, _opcDIGroup;
        private List<OPCTagScale> _cnfgTagnames;
        private List<OPCTagGroup> _reqTagGroups;
        private volatile MarketState _marketState;
        private readonly object _marketStateLock = new object();
        private System.Threading.Timer _timer;
        private volatile bool UpdatingHDMI;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(Convert.ToInt32(ConfigurationSettings.AppSettings["UpdatePeriod"]));
        

        private readonly static Lazy<OPCService> _instance = new Lazy<OPCService>(
            () => new OPCService(GlobalHost.ConnectionManager.GetHubContext<ScadaHub>().Clients));


        public OPCService()
        {
            InitializeComponent();
        }

        public OPCService(IHubConnectionContext<dynamic> clients)
        {
            InitializeComponent();

            Clients = clients;

            Init();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

     
        public static OPCService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public MarketState MarketState
        {
            get { return _marketState; }
            private set { _marketState = value; }
        }

        private void Init()
        {
            _cnfgTagnames = new List<OPCTagScale>(); 

            _opcServer = new OPCServer();

            _opcServer.Connect("ArchestrA.FSGateway.3", "localhost");

            if (_opcServer.ServerState == 1)
            {
                _opcMFIGroup = _opcServer.OPCGroups.Add("AnalogLive");
                _opcMFIGroup.DeadBand = 0;
                _opcMFIGroup.UpdateRate = 1000;
                _opcMFIGroup.IsSubscribed = true;
                _opcMFIGroup.IsActive = true;
                ImportTagfromCSV("MFI.csv", ref _opcMFIGroup);

                _opcDIGroup = _opcServer.OPCGroups.Add("DiscreteLive");
                _opcDIGroup.DeadBand = 0;
                _opcDIGroup.UpdateRate = 1000;
                _opcDIGroup.IsSubscribed = true;
                _opcDIGroup.IsActive = true;
                ImportTagfromCSV("DI.csv", ref _opcDIGroup);
            }
        }

        private void ImportTagfromCSV(string filename, ref OPCGroup opcGroup)
        {
            try
            {
                string configDirectory = HttpContext.Current.Server.MapPath("~") + "\\Config";
                System.IO.StreamReader readFile = new System.IO.StreamReader(configDirectory + "\\" + filename);
                string line;
                string[] row;

                List<string> tagNames = new List<string>();
                tagNames.Add("");

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');

                    tagNames.Add(row[0]);

                    OPCTagScale tag = new OPCTagScale
                    {
                        TagName = row[0],
                        ScaleValue = row.Length > 1 ? Convert.ToDouble(row[1]) : 1,
                    };

                    _cnfgTagnames.Add(tag);                    
                }
                readFile.Close();

                int count = tagNames.Count;

                Array OPCItemIDs = Array.CreateInstance(typeof(string), count);
                Array ItemServerHandles = Array.CreateInstance(typeof(Int32), count);
                Array ItemServerErrors = Array.CreateInstance(typeof(Int32), count);
                Array ClientHandles = Array.CreateInstance(typeof(Int32), count);
                Array RequestedDataTypes = Array.CreateInstance(typeof(Int16), count);
                Array AccessPaths = Array.CreateInstance(typeof(string), count);

                OPCItemIDs = tagNames.ToArray();

                opcGroup.OPCItems.AddItems(count - 1, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);
            }
            catch (Exception ex)
            {
                //_eventlog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }

        }

        public string GetDataByUserAjax(string ListTagName, string user)
        {
            //if (MarketState == MarketState.Open)
            {
                //_eventlog.WriteEntry(user, EventLogEntryType.Information);

                bool existed = _reqTagGroups.FirstOrDefault(x => x.GroupID == user) != null;

                if (!existed)
                {
                    string[] list = ListTagName.Split(',');
                    List<string> listAnalog = new List<string>();
                    List<string> listDiscrete = new List<string>();

                    for (int i = 0; i < list.Count(); i++)
                    {
                        string[] listitem = list[i].Split('*');
                        if (listitem[1].Contains("CB SW") || listitem[1].Contains("DS SW"))
                        {
                            listDiscrete.Add(listitem[0]);
                        }
                        else
                        {
                            listAnalog.Add(System.Uri.UnescapeDataString(listitem[0]));
                        }
                    }

                    OPCTagGroup g = new OPCTagGroup()
                    {
                        GroupID = user,
                        AnalogTagNames = listAnalog,
                        DiscreteTagNames = listDiscrete,
                    };

                    _reqTagGroups.Add(g);
                }
            }
            return "";
        }

        List<OPCData> ReadOPC(OPCGroup opcGroup, List<string> ListTagname, string Datatype)
        {
            List<OPCData> result = new List<OPCData>();

            try
            {
                foreach (string tagname in ListTagname)
                {
                    if (tagname == null) break;

                    // check if this tagname is fomular tag which include '+' character
                    if (tagname.Contains('+'))
                    {
                        List<string> memberTag = new List<string>(tagname.Split(new char[] { '+', ' ' }, StringSplitOptions.RemoveEmptyEntries));

                        List<OPCData> memberHMITag = ReadOPC(opcGroup, memberTag, Datatype);

                        OPCData sum =  new OPCData() {
                            TagName = tagname,
                            Value = (from p in memberHMITag where ((double ?)p.Value).HasValue select (double)p.Value).Sum(),
                            TimeStamp = memberHMITag.Max(p => p.TimeStamp),
                            Quality = (int)memberHMITag.Average(p => p.Quality),
                            RequestedDataType = Datatype,
                        };                       

                        result.Add(sum);
                    }

                    else
                    {
                        OPCTagScale tagScale = _cnfgTagnames.FirstOrDefault(x => x.TagName == tagname);

                        if (tagScale == null) break;

                        OPCItem opcItem = null;
                        try
                        {
                            opcItem = opcGroup.OPCItems.Item(tagname);
                        }
                        catch (Exception ex)
                        {
                            //_eventlog.WriteEntry("Tagname is not found: " + System.Uri.UnescapeDataString(tagname), EventLogEntryType.Error);
                        };

                        if (opcItem != null)
                        {
                            if (opcItem.Value != null)
                            {
                                OPCData tag = new OPCData
                                {
                                    TagName = tagname,
                                    Value = (tagScale.ScaleValue == 1) ? opcItem.Value : opcItem.Value * tagScale.ScaleValue,
                                    Quality = opcItem.Quality,
                                    TimeStamp = opcItem.TimeStamp,
                                    RequestedDataType = Datatype,
                                };

                                result.Add(tag);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_eventlog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }

            return result;
        }

        async void UpdateHDMI(object state)
        {
            // This function must be re-entrant as it's running as a timer interval handler
            //    lock (_updateHdmiLock)
            //  {
            if (!UpdatingHDMI)
            {
                UpdatingHDMI = true;

                try
                {
                    foreach (OPCTagGroup item in _reqTagGroups)
                    {
                        // Use Below code if read OPC data directly

                        if (item.DiscreteTagNames.Count > 0)
                        {
                            //Task<List<OPCData>> taskDiscreteLive = async ReadOPC(_opcDIGroup, item.DiscreteTagNames, "Boolean");
                            //List<OPCData> tblDiscreteLive = await taskDiscreteLive;
                            List<OPCData> tblDiscreteLive = ReadOPC(_opcDIGroup, item.DiscreteTagNames, "Boolean");
                            sendclientOPC(tblDiscreteLive);
                        }

                        if (item.AnalogTagNames.Count > 0)
                        {
                            //Task<List<OPCData>> taskAnalog = ReadOPC(_opcMFIGroup, item.AnalogTagNames, "float");
                            //List<OPCData> tblAnalog = await taskAnalog;
                            List<OPCData> tblAnalog = ReadOPC(_opcMFIGroup, item.AnalogTagNames, "float");
                            sendclientOPC(tblAnalog);
                        }
                    }

                }
                catch (Exception ex)
                { }

                UpdatingHDMI = false;
            }
            // }
        }

        public void sendclientOPC(List<OPCData> TagList)
        {
            if (TagList != null)
            {
                foreach (OPCData tag in TagList)
                {
                    Clients.All.updateHDMI(JsonConvert.SerializeObject(tag));
                }
            }
        }

        public bool OpenMarket()
        {
            bool returnCode = false;

            lock (_marketStateLock)
            {
                if (_marketState != MarketState.Open)
                {
                    _timer = new System.Threading.Timer(UpdateHDMI, null, _updateInterval, _updateInterval);

                    _marketState = MarketState.Open;

                    BroadcastMarketStateChange(MarketState.Open);
                }
            }
            returnCode = true;

            return returnCode;
        }

        public bool CloseMarket()
        {
            bool returnCode = false;

            lock (_marketStateLock)
            {
                if (_marketState == MarketState.Open)
                {
                    if (_timer != null)
                    {
                        _timer.Dispose();
                    }

                    _marketState = MarketState.Closed;

                    BroadcastMarketStateChange(MarketState.Closed);
                }
            }
            returnCode = true;

            return returnCode;
        }

        public bool Reset()
        {
            bool returnCode = false;

            lock (_marketStateLock)
            {
                if (_marketState != MarketState.Closed)
                {
                    throw new InvalidOperationException("Market must be closed before it can be reset.");
                }

                // LoadDefaultCurrencies();
                BroadcastMarketReset();
            }
            returnCode = true;

            return returnCode;
        }

        private void BroadcastMarketStateChange(MarketState marketState)
        {
            switch (marketState)
            {
                case MarketState.Open:
                    Clients.All.marketOpened();
                    break;
                case MarketState.Closed:
                    Clients.All.marketClosed();
                    break;
                default:
                    break;
            }
        }

        private void BroadcastMarketReset()
        {
            Clients.All.marketReset();
        }
    }

    public enum MarketState
    {
        Closed,
        Open
    }

    }
}
