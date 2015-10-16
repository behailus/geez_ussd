using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geez.Data;

namespace Geez.Business
{
    public class Administration
    {
        private GeezEntities _context;
        public Administration()
        {
            _context = new GeezEntities();
        }
        public int TotalMembers()
        {
            return _context.User.Count();
        }
        public int TotalServiceProviders()
        {
           return _context.ServiceProvider.Count();
        }
        public int TotalMessages()
        {
            return _context.Message.Count();
        }
        public int TotalApplications()
        {
            return _context.Application.Count();
        }
        public int TotalUniqueRequests()
        {
            return _context.Request.Count(r => r.Response == false);
        }
        public int UnReadMessages()
        {
            return _context.Message.Count(m => m.Status == 0);
        }
        public int UnSentRecommendations()
        {
            return _context.Recommendation.Count(r => r.Status == 0);
        }
        public int TotalRecommendation()
        {
            return _context.Recommendation.Count();
        }
        public int ThisMonthUsers()
        {
            return _context.User.Where(u =>u.DateRegistered.Year==DateTime.Now.Year).Count(u => u.DateRegistered.Month==DateTime.Now.Month);
        }
        public List<Recommendation> ListOfUnsentRecommendations()
        {
            return _context.Recommendation.Where(r => r.Status == 0).Take(5).ToList();
        }
        public Dictionary<string, int> TransactionStatus()
        {
            var logs = _context.TransactionLog.GroupBy(t => t.State);
            return logs.ToDictionary(log => log.Key, log => log.Count());
        }
    }
}
