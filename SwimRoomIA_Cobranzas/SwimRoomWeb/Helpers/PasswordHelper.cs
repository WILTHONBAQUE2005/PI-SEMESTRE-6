using System;
using System.Security.Cryptography;
using System.Text;

namespace SwimRoomWeb.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash); 
        }

        public static bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return string.Equals(hashOfInput, hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
