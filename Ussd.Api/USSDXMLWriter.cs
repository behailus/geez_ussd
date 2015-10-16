using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussd.Api
{
    public class USSDXMLWriter
    {
        public  UssdResponseMessage _response;
        public  FaultResponse _fault;
        private bool isFault = false;

        private Dictionary<Type, string> aliasDictionary = new Dictionary<Type, string>()
            {
                { typeof(byte), "byte" },
                { typeof(sbyte), "sbyte" },
                { typeof(short), "short" },
                { typeof(ushort), "ushort" },
                { typeof(int), "int" },
                { typeof(uint), "uint" },
                { typeof(long), "long" },
                { typeof(ulong), "ulong" },
                { typeof(float), "float" },
                { typeof(double), "double" },
                { typeof(decimal), "decimal" },
                { typeof(object), "object" },
                { typeof(bool), "bool" },
                { typeof(char), "char" },
                { typeof(string), "string" },
                {typeof(DateTime),"dateTime.iso8601"}
            };
        private string resStarter = "<?xml version=\"1.0\"?> "+
                                     "<methodResponse> "+
                                            "<params>"+ 
                                                "<param> "+
                                                    "<value> "+
                                                        "<struct> ";
        private string resClosing = "</struct> " +
                                 "</value> " +
                               "</param> " +
                           "</params> " +
                     "</methodResponse>";
        private string faulStarter = "<?xml version=\"1.0\"?> " +
                                    "<methodFault >  " +
                                           "<params>" +
                                               "<param> " +
                                                   "<value> " +
                                                       "<struct> ";
        private string faulClosing = "</struct> " +
                                 "</value> " +
                               "</param> " +
                           "</params> " +
                     "</methodFault>";
        private StringBuilder builder;
        public USSDXMLWriter(UssdResponseMessage response)
        {
            _response = response;
            builder = new StringBuilder(resStarter);
            isFault = false;
        }
        public USSDXMLWriter(FaultResponse fault)
        {
             _fault= fault;
            builder = new StringBuilder(faulStarter);
            isFault = true;
        }
        string responseXML = "";
        public string GenerateXml()
        {
            if (!isFault)
            {
                var properties = _response.GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    builder.Append("<member>");
                    builder.Append("<name>");
                    builder.Append(propertyInfo.Name);
                    builder.Append("</name>");
                    builder.Append("<value>");
                    string type = aliasDictionary[propertyInfo.PropertyType];
                    
                    if (propertyInfo.Name=="TransactionTime")
                    {
                        builder.Append("<dateTime.iso8601>");
                        DateTime dateTime = Convert.ToDateTime(propertyInfo.GetValue(_response));
                        builder.Append(dateTime.ToString("yyyymmddThh:mm:ss +0000"));
                        builder.Append("</dateTime.iso8601>");
                    }
                    else
                    {
                        builder.Append("<" + type + ">");
                        builder.Append(propertyInfo.GetValue(_response));
                        builder.Append("</" + type + ">");
                    }
                    builder.Append("</value>");
                    builder.Append("</member>");
                }
                builder.Append(resClosing);
            }
            else
            {
                var properties = _fault.GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    builder.Append("<member>");
                    builder.Append("<name>");
                    builder.Append(propertyInfo.Name);
                    builder.Append("</name>");
                    builder.Append("<value>");
                    string type = aliasDictionary[propertyInfo.PropertyType];
                    builder.Append("<" + type + ">");
                    builder.Append(propertyInfo.GetValue(_fault).ToString());
                    builder.Append("</" + type + ">");
                    builder.Append("</value>");
                    builder.Append("</member>");
                }
                builder.Append(faulClosing);
            }

            responseXML = builder.ToString();
            return responseXML;
        }

    }
}
