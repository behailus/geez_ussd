using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using Ussd.Api;

namespace Ussd.Gateway
{
    public class UssdSender
    {
      
        public void SendUssdResponse(string httpresponse)
        {
            //Remember to format it in xml first
            httpresponse = httpresponse.Replace("©", "\n");
            var responseObj = GetObjectFromJson(httpresponse);
            var xmlResponse = new USSDXMLWriter(responseObj).GenerateXml();
            string url = ConfigurationManager.AppSettings["EtUrl"].ToString(CultureInfo.InvariantCulture);
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml";
            byte[] byteArray = Encoding.UTF8.GetBytes(xmlResponse);
            webRequest.ContentLength = byteArray.Length;
            Stream stream = webRequest.GetRequestStream();
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Close();
            WebResponse response = webRequest.GetResponse();
            response.Close();
            
        }

        public UssdResponseMessage GetObjectFromJson(string jsonString)
        {
            jsonString = jsonString.Replace("\\", "").Trim(new char[] { '"', '\\' });
            var serializer = new JavaScriptSerializer();
            //request = serializer.Deserialize<UssdRequestMessage>(requestJson);
            var response = serializer.Deserialize<UssdResponseMessage>(jsonString);//new UssdResponseMessage() { TransactionId = values[0], TransactionTime = Convert.ToDateTime(values[1]), USSDResponseString = values[2], action = values[3], ResponseCode = Convert.ToInt32(values[4]) };
            return response;
        }

        public void XmlRpcCall(string httpResponse)
        {
            httpResponse = httpResponse.Replace("©", "<\br>");
            var responseObj = GetObjectFromJson(httpResponse);
            var xmlResponse = new USSDXMLWriter(responseObj).GenerateXml();
            string url = ConfigurationManager.AppSettings["EtUrl"].ToString(CultureInfo.InvariantCulture);
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            var command = xmlResponse;
            var bytes = Encoding.ASCII.GetBytes(command);
            req.ContentLength = bytes.Length;
            using (var stream = req.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            using (var stream = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                stream.ReadToEnd();
            }
        }

    }
}