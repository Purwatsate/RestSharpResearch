using ApiClient.Exceptions;
using ApiClient.Interceptors;
using ApiClient.Services.Interface;
using ApiShared.Settings;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ApiClient.Services
{
    public class MyApiClient : IMyApiClient
    {
        private readonly RestClient _client;

        public MyApiClient(IOptions<ApiClientSettings> settings, ITokenProvider tokenProvider)
        {
            var options = new RestClientOptions(settings.Value.BaseUrl)
            {
                Interceptors = [new HeaderInterceptor("Authorization", $"Bearer {tokenProvider.GetToken()}")]
            };
            _client = new RestClient(options);
        }

        public async Task<T> GetAsync<T>(string endpoint, CancellationToken token = default, Dictionary<string, object>? routeVariables = null,
            Dictionary<string, object>? queryParams = null)
        {
            var request = new RestRequest(endpoint, Method.Get);

            // Add route Variables 
            if (routeVariables != null)
            {
                foreach (var kvp in routeVariables)
                {
                    request.AddUrlSegment(kvp.Key, kvp.Value.ToString() ?? "");
                }
            }

            // Add query parameters
            if (queryParams != null)
            {
                foreach (var kvp in queryParams)
                {
                    request.AddQueryParameter(kvp.Key, kvp.Value?.ToString());
                }
            }

            var response = await _client.ExecuteGetAsync<T>(request, token);
            return HandleResponse(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, object body, CancellationToken token = default)
        {
            var request = new RestRequest(endpoint, Method.Post)
                .AddJsonBody(body);
            var response = await _client.ExecuteAsync<T>(request, token);
            return HandleResponse(response);
        }

        private static T HandleResponse<T>(RestResponse<T> response)
        {
            if (!response.IsSuccessful)
                throw new ApiException(response.StatusCode, response.Content ?? response.ErrorMessage ?? "Unknown error");

            return response.Data!;
        }
    }
}