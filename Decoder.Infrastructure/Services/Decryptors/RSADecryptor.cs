using Decryptor.Infrastructure.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Decryptor.Infrastructure.Services.Decryptors
{
    internal class RSADecryptor : IAsymmetricDecryptor
    {
        private readonly RSAParameters _privateKey;
        private readonly string _publicKeyString;

        public RSADecryptor()
        {
            var rsaProvider = new RSACryptoServiceProvider(2048);
            _privateKey = rsaProvider.ExportParameters(true);
            _publicKeyString = rsaProvider.ToXmlString(false);
        }

        public string PublicKey { get => _publicKeyString; }

        public string Decrypt(string encryptedData)
        {
            var byteData = Convert.FromBase64String(encryptedData);
            var rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.ImportParameters(_privateKey);
            var bytePlainTextData = rsaProvider.Decrypt(byteData, false);
            var plainText = Encoding.Unicode.GetString(bytePlainTextData);
            return plainText;
        }
    }
}
