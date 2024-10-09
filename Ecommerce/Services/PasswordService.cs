using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Services
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var data = Convert.ToBase64String(bytes);
            return data;
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var hashOfInput = HashPassword(password);
            return hashedPassword.Equals(hashOfInput);
        }
    }
}
