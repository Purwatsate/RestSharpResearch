using ApiClient.Exceptions;
using ApiClient.Services.Interface;
using RestSharp;

namespace ApiClient.Services
{
    public class MyApiClient(string baseUrl) : IMyApiClient
    {
        private readonly RestClient _client = new(baseUrl);

        public async Task<T> GetAsync<T>(string endpoint, CancellationToken token = default)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _client.ExecuteGetAsync<T>(request, token);

            if (!response.IsSuccessful)
                throw new ApiException(response.StatusCode, response.Content ?? response.ErrorMessage ?? "Unknown error");

            return response.Data!;
        }

        public async Task<T> PostAsync<T>(string endpoint, object body, CancellationToken token = default)
        {
            var request = new RestRequest(endpoint, Method.Post).AddJsonBody(body);
            var response = await _client.ExecuteAsync<T>(request, token);

            if (!response.IsSuccessful)
                throw new ApiException(response.StatusCode, response.Content ?? response.ErrorMessage ?? "Unknown error");

            return response.Data!;
        }
    }

}