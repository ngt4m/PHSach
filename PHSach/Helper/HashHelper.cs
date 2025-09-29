using System;
using System.Security.Cryptography;
using System.Text;

namespace PHSach.Helper
{
    public static class HashHelper
    {
        public static string ToMd5(this string input)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }
    }

}
