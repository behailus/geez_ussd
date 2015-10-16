using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Geez.Data;

namespace Geez.Business
{
    public class WebHelper
    {
        private GeezEntities _context = new GeezEntities();
        public bool SaveUser(User user)
        {
            try
            {
                var lastUser = _context.User.OrderByDescending(u => u.Id).FirstOrDefault();
                var id = lastUser == null ? 1 : lastUser.Id + 1;
                user.Id = id;
                _context.User.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool SaveMessage(Message message)
        {
            try
            {
                message.SentOn = DateTime.Now;
                _context.Message.Add(message);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string CalculateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
