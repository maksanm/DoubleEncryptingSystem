using RestSharp;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Encryptor.Client.Services.ApiClients
{
    internal class DecryptorApiClient : ApiClientBase
    {
        public DecryptorApiClient(string apiUrl) : base(apiUrl) { }

        public async Task<string> GetDecryptedMessage(string encryptedMessageInfo)
        {
            var request = new RestRequest("message/" + HttpUtility.UrlEncode(encryptedMessageInfo), Method.Get);
            var requestRetryPolicy = CreateRequestPolicy("Decryption request");
            var response = await requestRetryPolicy
                .ExecuteAsync(async () => await _restClient.ExecuteGetAsync(request));
            if ((int)response.StatusCode != 418)
                throw new ApplicationException("Failed to decrypt message");
            return response.Content;

        }

        public async Task<string> GetPublicRSAKey()
        {
            var request = new RestRequest("rsa/public", Method.Get);
            var requestRetryPolicy = CreateRequestPolicy("RSA public key request");
            var response = await requestRetryPolicy
                .ExecuteAsync(async () => await _restClient.GetAsync(request));
            if (!response.IsSuccessful)
                throw new ApplicationException("Failed to fetch server public RSA-key");
            return response.Content;
        }
    }
}
