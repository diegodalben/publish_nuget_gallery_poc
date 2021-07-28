using System;
using System.Security.Cryptography;
using System.Text;

namespace Extensions
{
    public static class String
    {
        public static Guid GenerateGuid(this string value)
        {
            using var md5 = MD5.Create();
            var valueBytes = Encoding.UTF8.GetBytes(value);
            var hash = md5.ComputeHash(valueBytes);
            return new Guid(hash);
        }
    }
}