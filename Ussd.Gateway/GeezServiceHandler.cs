using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Script.Serialization;
using CookComputing.XmlRpc;
using Ussd.Api;

public struct UssdResponse
{
    public string TransactionId;
    public DateTime TransactionTime;
    public string USSDResponseString;
    public string action;
    public int ResponseCode;
}
public interface IHandleUssdRequest
{
    [XmlRpcMethod("handleUssdRequest")]
    UssdResponse handleUSSDRequest(params object[] parameters);
}
public class GeezServiceHandler:XmlRpcService,IHandleUssdRequest
{
    [XmlRpcMethod("handleUSSDRequest")]
    public UssdResponse handleUSSDRequest(params object[] parameters)
    {
        return GetResponse(parameters);
        //return new UssdResponse() { TransactionId = "123456", TransactionTime = DateTime.Now, USSDResponseString = "Hello Geez", action = "end", ResponseCode = 0 };
    }

    private UssdResponse GetResponse(object[] parameters)
    {
        
        var client = new HttpClient();
        string url = ConfigurationManager.AppSettings["ServiceUrl"].ToString(CultureInfo.InvariantCulture);
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var request = new USSDXMLReader().GetUssdRequest(parameters);
        var serializer = new JavaScriptSerializer();
        var requestCsv = serializer.Serialize(request);//new CsvHelper().WriteObjectToCsv(request);
        HttpResponseMessage response = client.PostAsJsonAsync("service/api/communication/GetResponse", requestCsv).Result;
        var cont = response.Content.ReadAsAsync<object[]>();
        var resul = (object[])cont.Result;
        return new UssdResponse() { TransactionId = resul[0].ToString(), TransactionTime = Convert.ToDateTime(resul[1].ToString()), USSDResponseString = resul[2].ToString().Replace("©","\n"), action = resul[3].ToString(), ResponseCode = Convert.ToInt32(resul[4]) };
         
        //return new UssdResponse() { TransactionId = "123456", TransactionTime = DateTime.Now, USSDResponseString = "Hello Geez " + rpcStruct["MSISDN"].ToString(), action = "end", ResponseCode = 0 };
    }
}