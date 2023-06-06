using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Helpers
{
    public static class HashPassword
    {
        public static string HashPas(string input)
        {
            using (var sha = SHA256.Create())
            {
                var hasByt = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                string hash = BitConverter.ToString(hasByt).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
