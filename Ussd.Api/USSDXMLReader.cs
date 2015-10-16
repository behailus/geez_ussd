using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using CookComputing.XmlRpc;

namespace Ussd.Api
{
    public class USSDXMLReader
    {
        private string _xmlDocument = "";
        public USSDXMLReader(string xmlDocument)
        {
            _xmlDocument = xmlDocument;
        }
        public USSDXMLReader()
        {
            
        }
        public UssdRequestMessage GenerateMessageObject()
        {
            UssdRequestMessage message = new UssdRequestMessage(); ;
            XmlDocument document = new XmlDocument();
            var xmlFormed = _xmlDocument.Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&gt;", ">");
            //throw new Exception(xmlFormed);
            document.LoadXml(xmlFormed);
            XmlNodeList nodes;
            try
            {
                nodes = document.SelectNodes("//methodCall/params/param/value/struct/member");
                if (nodes.Count > 0)
                {
                    message = new UssdRequestMessage();
                    foreach (XmlNode node in nodes)
                    {
                        XmlDocument nodedoc = new XmlDocument();
                        string modif = "<modified>" + node.InnerXml + "</modified>";
                        nodedoc.LoadXml(modif);
                        var resultName = nodedoc.SelectSingleNode("modified/name").InnerText;
                        var resultValue = nodedoc.SelectSingleNode("modified/value").InnerText;
                        PropertyInfo prop = message.GetType().GetProperty(resultName);
                        if (prop != null)
                        {
                            var val = Convert.ChangeType(resultValue, prop.PropertyType);
                            prop.SetValue(message, val, null);
                        }

                    }
                }
            }
            catch (Exception ex) { }

            return message;
        }

        public UssdRequestMessage GetUssdRequest(object[] parameters)
        {
            var rpcStruct = (XmlRpcStruct)parameters[0];

            return new UssdRequestMessage() { TransactionId = rpcStruct["TransactionId"].ToString(), TransactionTime = rpcStruct["TransactionTime"].ToString(), MSISDN = rpcStruct["MSISDN"].ToString(), USSDServiceCode = rpcStruct["USSDServiceCode"].ToString(), USSDRequestString = rpcStruct["USSDRequestString"].ToString(), response = Convert.ToBoolean(rpcStruct["response"].ToString()) };
        }

    }
}
