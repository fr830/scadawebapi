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
    [HubName("scadaHub")]
    public class ScadaHub : Hub
    {
        private readonly OPCService _OPCService;

        public ScadaHub()
            : this(OPCService.Instance)
        {
        }

        public ScadaHub(OPCService input)
        {
            _OPCService = input;
        }


        public override Task OnConnected()
        {
            Clients.All.addMessage("Client connected: " + Context.ConnectionId);
            //UserHandler.ConnectedIds.Add(Context.ConnectionId);
            return base.OnConnected();
        }

        public string send(string message)
        {
            Clients.All.addMessage(message);

            return message;
        }

        public string GetDataByUserAjax(string LisTagName, string user)
        {
            return _OPCService.GetDataByUserAjax(LisTagName, user);
        }

        public string GetMarketState()
        {
            return _OPCService.MarketState.ToString();
        }

        public bool OpenMarket()
        {
            _OPCService.OpenMarket();
            return true;
        }

        public bool CloseMarket()
        {
            _OPCService.CloseMarket();
            return true;
        }

        public bool Reset()
        {
            _OPCService.Reset();
            return true;
        }
    }
}