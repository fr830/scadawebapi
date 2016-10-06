using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Cors;

namespace ScadaHisAPI
{
    public class WebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable Cross-Origin Request for all clients
            var corsAttr = new EnableCorsAttribute(origins:"*", headers:"*", methods:"*");
            config.EnableCors(corsAttr);

            // Enable Route Attribution
            config.MapHttpAttributeRoutes();
        }
    }
}