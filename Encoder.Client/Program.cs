using Encoder.Client.Interfaces;
using Encoder.Client.Services;
using Encryptor.Client.Interfaces;
using Encryptor.Client.Services;
using Encryptor.Client.Services.Encryptors;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using System.Net.Http;
using Encryptor.Client.Interfaces.Encryptors;

namespace Encoder.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IApiClient apiClient = new ApiClient();
            ISymmetricEncryptor aesEncryptor = new AESEncryptor();
            IAsymmetricEncryptor rsaEncryptor = new RSAEncryptor();

            var rsaPublicKey = await apiClient.GetPublicRSAKey();
            Console.WriteLine("\nRSA public key fetched from the server: " + rsaPublicKey);
            Console.WriteLine("\n=======================================================================\n");

            while (true)
            {
                var message = GenerateString(25);
                var password = GenerateString();
                (var aesEncryptedMessage, var aesIV) = aesEncryptor.Encrypt(message, password);
                Console.WriteLine("Generated message: " + message);
                Console.WriteLine("With password (AES key): " + password);
                Console.WriteLine("\nAES initialization vector: " + aesIV);
                Console.WriteLine("\nAES-encrypted message: " + aesEncryptedMessage);

                var id = Guid.NewGuid();                
                if (!InsertMessageToDatabase(id.ToString(), aesEncryptedMessage))
                    break;
                Console.WriteLine("Generated message ID: " + id.ToString());

                var rsaEncryptedIdWithAesKeys = rsaEncryptor.Encrypt(id.ToString() + "$" + password + "$" + aesIV, rsaPublicKey);
                Console.WriteLine("\nRSA-encrypted ID with AES key and AES initialization vector:\n" + rsaEncryptedIdWithAesKeys);

                var decryptedMessage = await apiClient.GetDecryptedMessage(rsaEncryptedIdWithAesKeys);
                Console.WriteLine("\nDecrypted message fetched from the server: " + decryptedMessage);
                Console.WriteLine("\n=======================================================================\n");

                Thread.Sleep(15000);
            }
        }

        private static bool InsertMessageToDatabase(string id, string message)
        {
            IMessageDatabaseFacade dbFacade = new MessageDatabaseFacade();
            var inserted = dbFacade.InsertMessage(id.ToString(), message);
            return inserted;
        }

        private static Random random = new Random();

        private static string GenerateString(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
    }
}
