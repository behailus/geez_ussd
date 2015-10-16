using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussd.Api
{
    [Serializable]
    public class UssdResponseMessage
    {
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string USSDResponseString { get; set; }
        public string action { get; set; }
        public int ResponseCode { get; set; }

    }
}
