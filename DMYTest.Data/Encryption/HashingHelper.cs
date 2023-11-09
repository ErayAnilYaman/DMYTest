
using System.Security.Cryptography;
using System.Text;

namespace DMYTest.Data.Encryption
{
    public class HashingHelper
    {
        public static void HashPassword(string password , out byte[] passwordHash ,out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VerifyPassword(string password , byte[] passwordHash , byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                byte[] passwordCheck;
                passwordCheck = hmac.ComputeHash (Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < passwordCheck.Length; i++)
                {
                    if (passwordCheck[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
