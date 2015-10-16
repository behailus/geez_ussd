using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Ussd.Gateway.Formatter;

namespace Ussd.Gateway
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Add(new UssdResponseCsvFormatter());
            config.Formatters.Add(new UssdRequestCsvFormatter());
        }
    }
}