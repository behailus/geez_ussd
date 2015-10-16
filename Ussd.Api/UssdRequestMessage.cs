using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ussd.Api
{
    [Serializable]
    public class UssdRequestMessage
    {
        public string MethodName { get { return "handleUSSDRequest"; } }

        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string MSISDN { get; set; }
        public string USSDServiceCode { get; set; }
        public string USSDRequestString { get; set; }
        public bool response { get; set; }
        public bool ChargingFlag { get; set; }
        public decimal ChargeCode { get; set; }


    }
}
