using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geez.Data;

namespace Geez.Business
{
    public class Logger
    {
        private GeezEntities _context = new GeezEntities();
        public bool Log(TransactionLog transaction)
        {
            try
            {
              _context.TransactionLog.Add(transaction);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public TransactionLog LastState(string transactionId)
        {
            return
                _context.TransactionLog.Where(t => t.TransactionId == transactionId)
                        .OrderByDescending(t => t.Id)
                        .FirstOrDefault();
        }

        public IQueryable<TransactionLog> AllState(string transactionId)
        {
            return _context.TransactionLog.Where(t => t.TransactionId == transactionId).OrderByDescending(t => t.Id);
        }
    }
}
