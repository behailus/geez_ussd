using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Script.Serialization;
using Ussd.Api;
using Ussd.Gateway;
public class UssdReceiver:UssdHttpHandler
 {
     protected override void onMessage(string message)
     {
         MessageAnalysis(message);
     }
    
     public void MessageAnalysis(string message)
     {
         #region Commented Old Version
         //string responseXml = "";
         // pass it to some service
         //var client=new HttpClient();
         //string url = ConfigurationManager.AppSettings["ServiceUrl"].ToString(CultureInfo.InvariantCulture);
         //client.BaseAddress = new Uri(url);
         //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



         //var irequest = new UssdRequestMessage() { TransactionId = "53635424", MSISDN = "911734365", TransactionTime = DateTime.UtcNow.ToString("o"), response = false, USSDServiceCode = "809", USSDRequestString = "*809#" };
         ////HttpResponseMessage response = client.GetAsync("api/Communication/GetResponse?request=" + message).Result;
         //HttpResponseMessage iresponse = client.PostAsJsonAsync("api/communication/GetResponse", irequest).Result;
         //var cont = iresponse.Content.ReadAsAsync<UssdResponseMessage>();

         ////HttpResponseMessage response = client.GetAsync("api/Communication/GetResponse?request=" + message).Result;
         ////HttpResponseMessage response = client.PostAsJsonAsync("api/communication/GetResponse", message).Result;
         //if (iresponse.IsSuccessStatusCode)
         //{
         //    var content = iresponse.Content.ReadAsAsync<UssdResponseMessage>();
         //    responseXml = new USSDXMLWriter(content.Result).GenerateXml();
         //}
         //else
         //{
         //    var request = new USSDXMLReader(message).GenerateMessageObject();
         //    var fault = new FaultResponse() {TransactionId = request.TransactionId,FaultCode = "303",FaultString = "Error occured processing the request",TransactionTime = DateTime.UtcNow};
         //    responseXml = new USSDXMLWriter(fault).GenerateXml();
         //}
         //var request = new USSDXMLReader(message).GenerateMessageObject();
         //var response = new Gate().GetResponse(request);
         //responseXml = new USSDXMLWriter(response).GenerateXml();
#endregion
         var client = new HttpClient();
         string url = ConfigurationManager.AppSettings["ServiceUrl"].ToString(CultureInfo.InvariantCulture);
         client.BaseAddress = new Uri(url);
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

         string transactionId = "";
         Random random = new Random();
         var trId = random.Next(0, 99999999);
         transactionId = trId.ToString("0000000#");
         //var request = new UssdRequestMessage() { TransactionId = transactionId, MSISDN = "251911734365", TransactionTime = DateTime.UtcNow.ToString("o"), response = false, USSDServiceCode = "809", USSDRequestString = "*809#" };
         var request = new USSDXMLReader(message).GenerateMessageObject();
         var serializer = new JavaScriptSerializer();
         var requestCsv = serializer.Serialize(request);//new CsvHelper().WriteObjectToCsv(request);
         HttpResponseMessage response = client.PostAsJsonAsync("service/api/communication/GetResponse", requestCsv).Result;
         var cont = response.Content.ReadAsStringAsync();

         new UssdSender().XmlRpcCall(cont.Result);//.SendUssdResponse(cont.Result);
     }


 }
