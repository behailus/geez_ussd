using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Geez.Services.Formatter;

namespace Geez.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Formatters.Add(new UssdResponseCsvFormatter());
            //config.Formatters.Add(new UssdRequestCsvFormatter());
        }
    }
}
