using Decryptor.Core.Services.Interfaces.Decryptors;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Decryptor.Infrastructure.Services.Decryptors
{
    internal class AESDecryptor : ISymmetricDecryptor
    {
        private readonly string _passwordSalt;

        public AESDecryptor(IConfiguration config)
        {
            _passwordSalt = config["AES:KeySalt"];
        }

        public string Decrypt(string encryptedText, string password, string IV)
        {
            using var aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(password);
            aes.IV = Convert.FromBase64String(IV);

            var encryptedBytes = Convert.FromBase64String(encryptedText);
            using MemoryStream encryptedStream = new(encryptedBytes);
            using CryptoStream cryptoStream = new(encryptedStream, aes.CreateDecryptor(), CryptoStreamMode.Read);

            using MemoryStream decodedStream = new();
            cryptoStream.CopyTo(decodedStream);

            var decryptedText = Encoding.Unicode.GetString(decodedStream.ToArray());
            return decryptedText;
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
