using Encryptor.Client.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryptor.Client.Services.Encryptors
{
    internal class AESEncryptor : IEncryptor
    {
        private readonly string _passwordSalt = "super_hard_salt";

        public string Encrypt(string plainText, string password)
        {
            using var aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(password);

            using MemoryStream encryptedStream = new();
            using CryptoStream cryptoStream = new(encryptedStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(Encoding.Unicode.GetBytes(plainText));
            cryptoStream.FlushFinalBlock();

            var encryptedText = Convert.ToBase64String(encryptedStream.ToArray());
            return encryptedText;
        }

        private byte[] DeriveKeyFromPassword(string password)
        {
            var byteSalt = Encoding.ASCII.GetBytes(_passwordSalt);
            var iterations = 1000;
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.Unicode.GetBytes(password),
                                             byteSalt,
                                             iterations,
                                             HashAlgorithmName.SHA384);
            return rfc2898DeriveBytes.GetBytes(32); // 32 bytes equal 256 bits
        }
    }
}
