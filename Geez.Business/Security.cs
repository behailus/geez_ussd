using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Geez.Data;

namespace Geez.Business
{
    public class Security
    {
        private GeezEntities _context;
        public Security()
        {
            _context = new GeezEntities();
        }
        public string CalculateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public int Authenticate(User user)
        {
            var hashedPassword = CalculateMd5Hash(user.Password);
            var tempUser = _context.User.Where(u => u.UserName == user.UserName).FirstOrDefault(u => u.Password == hashedPassword);
            if (tempUser == null)
            {
                return 0;
            }
            else
            {
                if (tempUser.Role)
                    return 7;
                return 3;
            }
        }
    }
}
