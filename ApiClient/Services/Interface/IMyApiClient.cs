namespace ApiClient.Services.Interface
{
    public interface IMyApiClient
    {
        Task<T> GetAsync<T>(string endpoint, CancellationToken token = default);
        Task<T> PostAsync<T>(string endpoint, object body, CancellationToken token = default);
    }
}
