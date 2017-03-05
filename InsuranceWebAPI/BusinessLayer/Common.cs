using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace InsuranceWebAPI.BusinessLayer
{
    public class Common
    {
        public static string GetHashPassword(string password, string saltKey)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(String.Concat(password, saltKey), "SHA1");
        }

        public static string GenerateSalt(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[length];
                rng.GetBytes(buffer);
                return Convert.ToBase64String(buffer);
            }
        }

        public static string CreatePasswordHash(string password, string saltKey)
        {
            string saltAndPassword = String.Concat(password, saltKey);
            var hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, "SHA1");
            return hashedPassword;
        }
    }
}