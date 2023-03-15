using Encoder.Client.Interfaces;
using Encoder.Client.Services;
using Encryptor.Client.Interfaces;
using Encryptor.Client.Interfaces.Encryptors;
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

                IApiClient apiClient = new ApiClient();
                var rsaPublicKey = await apiClient.GetPublicRSAKey();
                IAsymmetricEncryptor rsaEncryptor = new RSAEncryptor();
                var encryptedData = rsaEncryptor.Encrypt(id.ToString(), rsaPublicKey);

                await apiClient.GetDecryptedMessage(encryptedData);
                
                if (!InsertMessageToDatabase(id.ToString(), message))
                    break;

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
