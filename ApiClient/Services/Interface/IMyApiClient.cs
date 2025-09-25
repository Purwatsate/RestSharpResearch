namespace ApiClient.Services.Interface
{
    public interface IMyApiClient
    {
        Task<T> GetAsync<T>(string endpoint, CancellationToken token = default, Dictionary<string, object>? routeVariables = null,
            Dictionary<string, object>? queryParams = null);

        Task<T> PostAsync<T>(string endpoint, object body, CancellationToken token = default);
    }
}
