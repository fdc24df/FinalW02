using LoanAPI.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace FLoanAPI.Data
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var hashedProvidedPassword = HashPassword(providedPassword);
            return hashedPassword.Equals(hashedProvidedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
