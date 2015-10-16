using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussd.Api
{
    public interface IRequestHandler
    {
        UssdResponseMessage ProcessRequest(UssdRequestMessage request);
    }
}
