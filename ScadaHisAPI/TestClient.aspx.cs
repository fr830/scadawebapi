using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;  // for class Encoding
using System.IO;
using System.Collections.Specialized;

namespace ScadaHisAPI
{
    public partial class TestClient : System.Web.UI.Page
    {
        static HttpClient client = new HttpClient();

        static async Task<List<DataPoint>> GetProductAsync(DateTime start, DateTime end, string[] FactoryList)
        {
            string uri = "/api/EnergyHistory/hourly?start=" + start.ToString() + "&end=" + end.ToString();
            List<DataPoint> result = null;
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, FactoryList);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<List<DataPoint>>(jsonString.Result);
            }

            return result;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime end = DateTime.Now;
            DateTime start = new DateTime(end.Year, end.Month, end.Day);
            string[] FactoryList = new string[] { "UONGBIMR", "BANVE", "DAININH", "DONGNAI3", "DONGNAI4", "SONGTRANH2" };

            var request = (HttpWebRequest)WebRequest.Create("http://localhost:7322/api/EnergyHistory/hourly?start=" + start.ToString() + "&end=" + end.ToString());

            var data = FactoryList;

            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, data.Length);

            //dataStream.Flush();

            // Close the Stream object.
            //dataStream.Close();

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            List<DataPoint> result = JsonConvert.DeserializeObject<List<DataPoint>>(responseString);

            foreach (DataPoint dp in result)
            {
                txtResult.InnerHtml += dp.DateTime.ToString() + " ";
                txtResult.InnerHtml += dp.TagName.ToString() + " ";
                txtResult.InnerHtml += dp.Value.ToString() + "\n";
            }
        }

        static async Task RunAsync()
        {
            DateTime end = DateTime.Now;
            DateTime start = new DateTime(end.Year, end.Month, end.Day);
            string[] FactoryList = new string[] { "UONGBIMR", "BANVE", "DAININH", "DONGNAI3", "DONGNAI4", "SONGTRANH2" };

            client.BaseAddress = new Uri("http://localhost:7322/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            List<DataPoint> result = await GetProductAsync(start, end, FactoryList);

            ShowProduct(result);

            //return result;
        }

        static void ShowProduct(List<DataPoint> data)
        {
        }
    }
}