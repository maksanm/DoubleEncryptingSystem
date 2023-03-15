using Encryptor.Client.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Encryptor.Client.Services.Encryptors
{
    internal class RSAEncryptor : IEncryptor
    {
        public string Encrypt(string plainTextData, string rsaPublicKey)
        {
            var rsaPublicStringKey = RemoveByteOrderMarks(rsaPublicKey);
            var rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.FromXmlString(rsaPublicStringKey);
            var bytePlainTextData = Encoding.Unicode.GetBytes(plainTextData);
            var byteEncryptedData = rsaProvider.Encrypt(bytePlainTextData, false);
            var encryptedText = Convert.ToBase64String(byteEncryptedData);
            return encryptedText;
        }

        private static string RemoveByteOrderMarks(string rsaPublicStringKey)
        {
            var _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (rsaPublicStringKey.StartsWith(_byteOrderMarkUtf8))
                rsaPublicStringKey = rsaPublicStringKey.Remove(0, _byteOrderMarkUtf8.Length);
            if (rsaPublicStringKey.EndsWith(_byteOrderMarkUtf8))
                rsaPublicStringKey = rsaPublicStringKey.Remove(rsaPublicStringKey.Length - _byteOrderMarkUtf8.Length, _byteOrderMarkUtf8.Length);
            return rsaPublicStringKey;
        }
    }
}
