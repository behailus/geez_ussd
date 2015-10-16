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
    public class UssdRequestCsvFormatter:BufferedMediaTypeFormatter
    {
        public UssdRequestCsvFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }
        public override bool CanReadType(Type type)
        {
            if (type == typeof(UssdRequestMessage))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<UssdRequestMessage>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override object ReadFromStream(Type type, System.IO.Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            using (var reader = new StreamReader(readStream))
            {
                var ussdRequest = new UssdRequestMessage();
                if (content != null)
                {
                    var values = content.ReadAsStringAsync().Result.Split(new char[] {','});
                    ussdRequest = new UssdRequestMessage() {TransactionId = values[0],TransactionTime = values[1],MSISDN = values[2],USSDServiceCode = values[3],USSDRequestString = values[4],response = Convert.ToBoolean(values[5]),ChargeCode = Convert.ToDecimal(values[6]),ChargingFlag = Convert.ToBoolean(values[7])};
                    return ussdRequest;
                }
                else
                {
                    throw new InvalidOperationException("Cannot DeSerialize Type");
                }
            }
            readStream.Close();
        }
    }
}