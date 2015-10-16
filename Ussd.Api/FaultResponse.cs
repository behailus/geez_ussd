using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussd.Api
{
    public class FaultResponse
    {
        public string TransactionId { get; set; }
        public DateTime TransactionTime { get; set; }
        public string FaultCode { get; set; }
        public string FaultString { get; set; }
    }
}
