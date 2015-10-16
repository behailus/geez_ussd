using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using Ussd.Api;

namespace Geez.Services.Formatter
{
    public class UssdResponseCsvFormatter:BufferedMediaTypeFormatter
    {
        public UssdResponseCsvFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }
        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof (UssdResponseMessage))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof (IEnumerable<UssdResponseMessage>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public override void WriteToStream(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                var ussdResponse = value as UssdResponseMessage;
                if (ussdResponse != null)
                {
                    WriteItem(ussdResponse, writer);
                }
                else
                {
                    throw new InvalidOperationException("Cannot Serialize Type");
                }
            }
            writeStream.Close();
        }

        private void WriteItem(UssdResponseMessage ussdResponse, StreamWriter writer)
        {
            writer.WriteLine("{0},{1},{2},{3},{4}",Escape(ussdResponse.TransactionId),Escape(ussdResponse.TransactionTime),Escape(ussdResponse.USSDResponseString),Escape(ussdResponse.action),Escape(ussdResponse.ResponseCode));
        }
        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };

        private string Escape(object o)
        {
            if (o == null)
            {
                return "";
            }
            string field = o.ToString();
            if (field.IndexOfAny(_specialChars) != -1)
            {
                return String.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else return field;
        }
    }
}