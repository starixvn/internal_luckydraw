using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace LuckyDraw.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool Authenticate(string password)
        {
            var hashPass = ConfigurationManager.AppSettings["password"];
            return hashPass == Hash(password);
        }

        private static string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
