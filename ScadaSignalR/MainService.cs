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
using Microsoft.AspNet.SignalR.Hubs;
using System.Configuration;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Microsoft.Owin.Hosting;
using System.Threading;

namespace ScadaSignalR
{
    public partial class MainService : ServiceBase
    {
        protected IDisposable _signalRApplication = null;

        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();

            _signalRApplication = WebApp.Start(ConfigurationManager.AppSettings["HostIP"]); // Must be @"http://+:8084" if you want clients on other computers to connect
        }

        protected override void OnStop()
        {
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }
    }
}
