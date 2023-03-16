using Polly;
using Polly.Fallback;
using Polly.Retry;
using Polly.Wrap;
using RestSharp;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Encryptor.Client.Services.ApiClients
{
    internal class ApiClientBase
    {
        protected readonly string _apiUrl;
        protected readonly IRestClient _restClient;

        public ApiClientBase(string apiUrl)
        {
            _apiUrl = apiUrl;
            _restClient = new RestClient(_apiUrl);
        }

        protected AsyncPolicyWrap<RestResponse> CreateRequestPolicy(string requestName) => 
            CreateRequestFallbackPolicy(requestName).WrapAsync(CreateRequestRetryPolicy(requestName));

        private static AsyncRetryPolicy CreateRequestRetryPolicy(string requestName) => Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: attemptCount => TimeSpan.FromSeconds(attemptCount),
                onRetry: (_, sleepDuration, _, _) =>
                {
                    Console.WriteLine("Request failed - " + requestName + $". Waiting for {sleepDuration.TotalSeconds} seconds to retry...");
                });

        private static AsyncFallbackPolicy<RestResponse> CreateRequestFallbackPolicy(string requestName) => Policy<RestResponse>
            .Handle<HttpRequestException>()
            .FallbackAsync(
                fallbackAction: async _ => { return null; },
                onFallbackAsync: async e =>
                {
                    Console.WriteLine("Request failed with message: " + e.Exception.Message + $". Exiting the application...");
                    Environment.Exit(0);
                });
    }
}

