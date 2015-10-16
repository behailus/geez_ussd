using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Ussd.Api;

namespace Geez.Services.Controllers
{
    public class TestController : Controller
    {
        //  
        // GET: /Test/

        public ActionResult Index()
        {
            var client = new HttpClient();
            string url = ConfigurationManager.AppSettings["ServiceUrl"].ToString(CultureInfo.InvariantCulture);
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var request = new UssdRequestMessage() { TransactionId = "53635424", MSISDN = "911734365", TransactionTime = DateTime.UtcNow.ToString("o"), USSDServiceCode = "809", USSDRequestString = "*809#" };
            var request = new UssdRequestMessage() { TransactionId = "53635424", MSISDN = "911734365", TransactionTime = DateTime.UtcNow.ToString("o"),response = false,USSDServiceCode = "809", USSDRequestString = "*809#" };
            //HttpResponseMessage response = client.GetAsync("api/Communication/GetResponse?request=" + message).Result;
            HttpResponseMessage response = client.PostAsJsonAsync("api/communication/GetResponse", request).Result;
            var cont = response.Content.ReadAsAsync<UssdResponseMessage>();
            
            return View(cont.Result);
        }

        public ActionResult ViewXml()
        {
            var response = new UssdResponseMessage { TransactionId = "TransactionId", TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = "Your account is blah blah blah!" };
            var xmlForm = new USSDXMLWriter(response).GenerateXml();
            ViewBag.Message = xmlForm;
            return View();
        }

    }
}
