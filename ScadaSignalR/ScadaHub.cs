using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using OPCAutomation;
using Microsoft.AspNet.SignalR.Hubs;

namespace ScadaSignalR
{
    [HubName("hubSSCADA")]
    public class ScadaHub : Hub
    {
        private readonly OPCSignalR _DAService;

        public ScadaHub()
            : this(OPCSignalR.Instance)
        {
        }

        public ScadaHub(OPCSignalR input)
        {
            _DAService = input;
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public string GetDataByUserAjax(string LisTagName, string user)
        {
            return _DAService.GetDataByUserAjax(LisTagName, user);
        }

        public string GetMarketState()
        {
            return _DAService.MarketState.ToString();
        }

        public bool OpenMarket()
        {
            _DAService.OpenMarket();
            return true;
        }

        public bool CloseMarket()
        {
            _DAService.CloseMarket();
            return true;
        }

        public bool Reset()
        {
            _DAService.Reset();
            return true;
        }
    }
}