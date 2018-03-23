using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScadaSignalR
{
    public class WebAPISignalR : SignalRBase
    {
        private readonly static Lazy<WebAPISignalR> _instance = new Lazy<WebAPISignalR>(
            () => new WebAPISignalR(GlobalHost.ConnectionManager.GetHubContext<ScadaHub>().Clients));

        public WebAPISignalR()
            : base()
        {
        }

        public WebAPISignalR(IHubConnectionContext<dynamic> clients)
            : base(clients)
        {
        }

        public static WebAPISignalR Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        List<DataPoint> GetRequest(string url, List<string> param)
        {
            List<DataPoint> result = new List<DataPoint>();
            try
            {
                string requestedURL = url;
                //  StringBuilder sb = new StringBuilder();
                var request = (HttpWebRequest)WebRequest.Create(requestedURL);
                string data = "";
                for (int i = 0; i < param.Count; i++)
                {
                    if (i == 0)
                    {
                        data = "%5B" + i + "%5D=" + param[i];
                    }
                    else
                    {
                        data += "&%5B" + i + "%5D=" + param[i];
                    }
                }
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
                    stream.Write(byteData, 0, data.Length);
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        result = JsonConvert.DeserializeObject<List<DataPoint>>(responseString);
                        response.Dispose();
                        response.Close();
                        byteData = null;
                        responseString = null;
                    }
                    stream.Dispose();
                    stream.Close();
                }

                //  var response = (HttpWebResponse)request.GetResponse();
                //  var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                // dt = (DataTable)JsonConvert.DeserializeObject(responseString, (typeof(DataTable)));
                // response = null;
                // responseString = null;
                request.Abort();
                data = null;
                request = null;
                return result;
            }
            catch (Exception ex)
            {
                Log.WriteEntry("GetRequest error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                return result;
            }

        }

        public override List<DataPoint> GetAnalogLive(List<string> tagnames)
        {
            string urlAnalogLive = ConfigurationManager.AppSettings["WebserviceAPI"] + "AnalogLive";
            //DataTable dt = new DataTable();

            //List<DataPoint> result = new List<DataPoint>();

            try
            {
                return GetRequest(urlAnalogLive, tagnames);
            }
            catch (Exception ex)
            {
                Log.WriteEntry("GetAnalogLive error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            
            return null;

        }

        public override List<DataPoint> GetDiscreteLive(List<string> tagnames)
        {
           // List<DataPoint> result = new List<DataPoint>();

            try
            {
                string urlDiscreteLive = ConfigurationManager.AppSettings["WebserviceAPI"] + "DiscreteLive";

                return GetRequest(urlDiscreteLive, tagnames);

            }
            catch (Exception ex)
            {
                Log.WriteEntry("GetDiscreteLive error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }

            return null;
        }
    }
}
