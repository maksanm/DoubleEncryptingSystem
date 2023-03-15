using Encryptor.Client.Interfaces;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Encryptor.Client.Services
{
    internal class ApiClient : IApiClient
    {
        private readonly string _apiUrl;
        private readonly IRestClient _restClient;

        public ApiClient(string apiUrl = "https://localhost:5001")
        {
            _apiUrl = apiUrl;
            _restClient = new RestClient(_apiUrl);
        }

        public async Task<string> GetDecryptedMessage(string encryptedMessageInfo)
        {
            var request = new RestRequest("message/" + HttpUtility.UrlEncode(encryptedMessageInfo), Method.Get);
            RestResponse response;
            response = await _restClient.ExecuteGetAsync(request);
            if ((int)response.StatusCode != 418)
                throw new ApplicationException("Failed to decrypt message");
            return response.Content;
            
        }

        public async Task<string> GetPublicRSAKey()
        {
            var request = new RestRequest("rsa/public", Method.Get);
            var response = await _restClient.GetAsync(request);
            if (!response.IsSuccessful)
                throw new ApplicationException("Failed to fetch server public RSA-key");
            return response.Content;
        }
    }
}
