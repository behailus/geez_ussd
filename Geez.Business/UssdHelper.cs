using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geez.Data;

namespace Geez.Business
{
    public class UssdHelper
    {
        private GeezEntities _context;
        public UssdHelper()
        {
            _context = new GeezEntities();
        }

        public List<Request> GetLatestRequests()
        {
            return _context.Request.OrderByDescending(r => r.Id).Take(100).ToList();
        }

        public List<Response> GetLatestResponses()
        {
            return _context.Response.OrderByDescending(r => r.Id).Take(100).ToList();
        }

        public List<Fault> GetLatestFaults()
        {
            return _context.Fault.OrderByDescending(r => r.Id).Take(100).ToList();
        }

        public List<ConnectionStatModel> GetStatistics(int year,int month)
        {
            return new List<ConnectionStatModel>();
        }
    }
}
