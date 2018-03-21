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
        private readonly DAService _DAService;

        public ScadaHub()
            : this(DAService.Instance)
        {
        }

        public ScadaHub(DAService input)
        {
            _DAService = input;
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