// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System;
using System.Security.Cryptography;
using System.Text;

namespace IKhom.UtilitiesLibrary.Runtime
{
    public class CustomMD5Encryptor
    {
        private const string SECRET_KEY = "rnKmI1CnPXwJejCewAeYGxBiCuX4ELdS";

        public string GetMD5Hash(string data)
        {
            using var provider = new MD5CryptoServiceProvider();
            var input = Encoding.UTF8.GetBytes(SECRET_KEY + data);
            var hash = provider.ComputeHash(input);
            var md5 = BitConverter.ToString(hash).Replace("-", string.Empty);
            return md5;
        }

        public string GetMD5Hash(string data, string secretKey)
        {
            using var provider = new MD5CryptoServiceProvider();
            var input = Encoding.UTF8.GetBytes(secretKey + data);
            var hash = provider.ComputeHash(input);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}