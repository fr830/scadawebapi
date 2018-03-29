using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Threading;

namespace ScadaSignalR
{
    public class SignalRBase
    {
        private List<TagNameGroup> _reqTagGroups;
        private volatile MarketState _marketState;
        private readonly object _marketStateLock = new object();
        private System.Threading.Timer _timer;
        private volatile bool UpdatingHDMI;
        private EventLog _eventlog;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(Convert.ToInt32(ConfigurationManager.AppSettings["UpdatePeriod"]));

        public const string DataTypeFloat = "float";
        public const string DataTypeBoolean = "Boolean";

        public SignalRBase()
        {
            Init();
        }

        public SignalRBase(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            Init();
        }

        protected IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public MarketState MarketState
        {
            get { return _marketState; }
            private set { _marketState = value; }
        }

        public EventLog Log
        {
            get { return _eventlog; }
        }

        private void Init()
        {
            string EventLogSource = "WebHMI";
            string EventLogName = "WebHMILog";

            if (!System.Diagnostics.EventLog.SourceExists(EventLogSource))
            {
                System.Diagnostics.EventLog.CreateEventSource(EventLogSource, EventLogName);
            }

            _eventlog = new System.Diagnostics.EventLog();
            _eventlog.Source = EventLogSource;
            _eventlog.Log = EventLogName;

            _reqTagGroups = new List<TagNameGroup>();
        }

        public string GetDataByUserAjax(string ListTagName, string user)
        {
            try
            {
                if (MarketState == MarketState.Open)
                {
                    string str = "GetDataByUserAjax: user id = " + user + "\n";

                    bool existed = _reqTagGroups.FirstOrDefault(x => x.GroupID == user) != null;

                    if (!existed)
                    {
                        string[] list = ListTagName.Split(',');
                        string strDI = "Discrete Tagnames:-------\n";
                        string strAI = "Analog Tagnames:---------\n";

                        TagNameGroup g = new TagNameGroup()
                        {
                            GroupID = user,
                            AnalogTagNames = new List<string>(),
                            DiscreteTagNames = new List<string>(),
                        };

                        for (int i = 0; i < list.Count(); i++)
                        {
                            string[] listitem = list[i].Split('*');
                            if (listitem[0] != null)
                            {
                                if (listitem[1].Contains("CB SW") || listitem[1].Contains("DS SW"))
                                {
                                    g.DiscreteTagNames.Add(listitem[0]);
                                    strDI += listitem[0] + "\n";
                                }
                                else
                                {
                                    g.AnalogTagNames.Add(listitem[0]);
                                    strAI += listitem[0] + "\n";
                                }
                            }
                        }

                        _reqTagGroups.Add(g);

                        str = str + strDI + strAI;
                    }

                    _eventlog.WriteEntry(str, EventLogEntryType.Information);
                }
            }

            catch (Exception ex)
            {
                _eventlog.WriteEntry("GetDataByUserAjax error: " + ex.Message, EventLogEntryType.Error);
            }
            return "";
        }

        private DataPoint DataPointSum(IEnumerable<DataPoint> datalist, string[] TagNameList, string NewTagName)
        {
            DataPoint sum = new DataPoint() { DateTime = DateTime.Now, TagName = NewTagName, Value = null, OPCQuality = 0 };

            var data = (from p in datalist where TagNameList.Contains(p.TagName) select p);

            if (data.Count<DataPoint>() > 0)
            {
                sum.DateTime = data.Max(p => p.DateTime);
                sum.Value = (from p in data where ((double?)p.Value).HasValue select (double)p.Value).Sum();
                sum.OPCQuality = (int)data.Average(p => p.OPCQuality);
            }

            return sum;
        }

        protected DataPoint CheckTagNameOfSum(string tagname)
        {
            if (tagname.Contains('+'))
            {
                List<string> memberTag = new List<string>(tagname.Split(new char[] { '+', ' ' }, StringSplitOptions.RemoveEmptyEntries));

                List<DataPoint> memberHMITag = GetAnalogLive(memberTag);

                DataPoint sum = DataPointSum(memberHMITag, memberTag.ToArray(), tagname);

                return sum;
            }

            return null;
        }

        public virtual List<DataPoint> GetAnalogLive(List<string> tagnames)
        {
            return null;
        }

        public virtual List<DataPoint> GetDiscreteLive(List<string> tagnames)
        {
            return null;
        }

        public async Task<List<DataPoint>> GetAnalogLiveAsync(List<string> tagnames)
        {
            return (await Task.Run(() => GetAnalogLive(tagnames)));
        }

        public async Task<List<DataPoint>> GetDiscreteLiveAsync(List<string> tagnames)
        {
            return (await Task.Run(() => GetDiscreteLive(tagnames)));
        }

        public async void UpdateHDMI(object state)
        {
            if (!UpdatingHDMI)
            {
                UpdatingHDMI = true;

                try
                {
                    foreach (TagNameGroup item in _reqTagGroups)
                    {
                        if (item.DiscreteTagNames.Count > 0)
                        {
                            Task<List<DataPoint>> taskDiscreteLive = GetDiscreteLiveAsync(item.DiscreteTagNames);
                            List<DataPoint> tblDiscreteLive = await taskDiscreteLive;
                            sendclient(tblDiscreteLive, DataTypeBoolean);
                        }

                        if (item.AnalogTagNames.Count > 0)
                        {
                            Task<List<DataPoint>> taskAnalog = GetAnalogLiveAsync(item.AnalogTagNames);
                            List<DataPoint> tblAnalog = await taskAnalog;
                            sendclient(tblAnalog, DataTypeFloat);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _eventlog.WriteEntry("Update HMI error " + ex.Message, EventLogEntryType.Error);
                }

                UpdatingHDMI = false;
            }
        }

        private void sendclient(List<DataPoint> list, string Datatype)
        {
            if (list != null)
            {
                foreach (DataPoint datapoint in list)
                {
                    HMITag tag = new HMITag
                    {
                        TagName = datapoint.TagName,
                        value = datapoint.Value,
                        quality = (int)datapoint.OPCQuality,
                        timeStamp = datapoint.DateTime,
                        RequestedDataType = Datatype
                    };
                    Clients.All.updateHDMI(JsonConvert.SerializeObject(tag));
                }
            }
        }

        public bool OpenMarket()
        {
            lock (_marketStateLock)
            {
                if (_marketState != MarketState.Open)
                {
                    _timer = new Timer(UpdateHDMI, null, _updateInterval, _updateInterval);

                    _marketState = MarketState.Open;

                    _eventlog.WriteEntry("Open Market", EventLogEntryType.Information);

                    BroadcastMarketStateChange(MarketState.Open);
                }
            }          

            return true;
        }

        public bool CloseMarket()
        {
            lock (_marketStateLock)
            {
                if (_marketState == MarketState.Open)
                {
                    if (_timer != null)
                    {
                        _timer.Dispose();
                    }

                    _marketState = MarketState.Closed;

                    _eventlog.WriteEntry("Close Market", EventLogEntryType.Information);

                    BroadcastMarketStateChange(MarketState.Closed);
                }
            }
           

            return true;
        }

        public bool Reset()
        {
            lock (_marketStateLock)
            {
                if (_marketState != MarketState.Closed)
                {
                    throw new InvalidOperationException("Market must be closed before it can be reset.");
                }

                // LoadDefaultCurrencies();
                BroadcastMarketReset();
            }

            return true;
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
