using Encryptor.Client.Interfaces.Encryptors;
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryptor.Client.Services.Encryptors
{
    internal class AESEncryptor : ISymmetricEncryptor
    {
        private readonly string _passwordSalt;

        public AESEncryptor()
        {
            _passwordSalt = ConfigurationManager.AppSettings["aesKeySalt"];
        }

        public (string encryptedText, string IV) Encrypt(string plainText, string password)
        {
            using var aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(password);

            using MemoryStream encryptedStream = new();
            using CryptoStream cryptoStream = new(encryptedStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(Encoding.Unicode.GetBytes(plainText));
            cryptoStream.FlushFinalBlock();

            var encryptedBase64String = Convert.ToBase64String(encryptedStream.ToArray());
            var IVBase64String = Convert.ToBase64String(aes.IV);
            return (encryptedBase64String, IVBase64String);
        }

        private byte[] DeriveKeyFromPassword(string password)
        {
            var byteSalt = Encoding.Unicode.GetBytes(_passwordSalt);
            var iterations = 1000;
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.Unicode.GetBytes(password),
                                             byteSalt,
                                             iterations,
                                             HashAlgorithmName.SHA384);
            return rfc2898DeriveBytes.GetBytes(32); // 32 bytes equal 256 bits
        }
    }
}
