using Encoder.Client.Interfaces;
using Encoder.Client.Services;
using Encryptor.Client.Interfaces;
using Encryptor.Client.Services;
using Encryptor.Client.Services.Encryptors;
using System;
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
                var message = GenerateMessage();
                var id = Guid.NewGuid();                

                IEncryptor aesEncryptor = new AESEncryptor();
                var encryptedMessage = aesEncryptor.Encrypt(message, "password");

                IApiClient apiClient = new ApiClient();
                var rsaPublicKey = await apiClient.GetPublicRSAKey();
                IEncryptor rsaEncryptor = new RSAEncryptor();
                var encryptedData = rsaEncryptor.Encrypt(id.ToString(), rsaPublicKey);

                
                if (!InsertMessageToDatabase(id.ToString(), message))
                    break;

                var decryptedMessage = await apiClient.GetDecryptedMessage(encryptedData);
                Console.WriteLine("Decrypted message: " + decryptedMessage);

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
    }
}
