using Encoder.Client.Interfaces;
using Encoder.Client.Services;
using Encryptor.Client.Interfaces;
using Encryptor.Client.Services;
using Encryptor.Client.Services.Encryptors;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Encoder.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("=================================================================\n");

                IApiClient apiClient = new ApiClient();
                IEncryptor aesEncryptor = new AESEncryptor();
                IEncryptor rsaEncryptor = new RSAEncryptor();

                var message = GenerateString(16);
                var password = GenerateString();
                var encryptedMessage = aesEncryptor.Encrypt(message, password);
                Console.WriteLine("Generated message: " + message);
                Console.WriteLine("With password: " + password + "\n");
                Console.WriteLine("AES-encrypted message: " + encryptedMessage);

                var id = Guid.NewGuid();                
                if (!InsertMessageToDatabase(id.ToString(), encryptedMessage))
                    break;
                Console.WriteLine("Generated message ID: " + id.ToString());

                var rsaPublicKey = await apiClient.GetPublicRSAKey();
                Console.WriteLine("RSA public key fetched from server: " + rsaPublicKey);
                var encryptedIdWithAESKey = rsaEncryptor.Encrypt(id.ToString() + "$" + encryptedMessage, rsaPublicKey);
                Console.WriteLine("RSA-encrypted ID with AES-key: " + encryptedIdWithAESKey + "\n");

                var decryptedMessage = await apiClient.GetDecryptedMessage(encryptedIdWithAESKey);
                Console.WriteLine("Decrypted message: " + decryptedMessage + "\n");

                Thread.Sleep(15000);
            }
        }

        private static string GenerateMessage()
        {
            return "Best test message ever";
        }

        private static bool InsertMessageToDatabase(string id, string message)
        {
            IMessageDatabaseFacade dbFacade = new MessageDatabaseFacade();
            var inserted = dbFacade.InsertMessage(id.ToString(), message);
            return inserted;
        }

        private static Random random = new Random();

        public static string GenerateString(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
    }
}
