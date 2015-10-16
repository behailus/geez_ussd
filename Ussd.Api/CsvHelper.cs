using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussd.Api
{
    public class CsvHelper
    {
        public UssdResponseMessage GetObjectFromCsv(string csvString)
        {
            var values = csvString.Split(new char[] {','});
            var response = new UssdResponseMessage(){TransactionId = values[0],TransactionTime = values[1],USSDResponseString = values[2],action = values[3],ResponseCode = Convert.ToInt32(values[4])};
            return response;
        }

        public string WriteObjectToCsv(UssdRequestMessage request)
        {
            return request.TransactionId+","+request.TransactionTime.ToString()+","+request.MSISDN+","+request.USSDServiceCode+","+request.USSDRequestString+","+request.response+","+request.ChargeCode+","+request.ChargingFlag;
        }
    }
}
