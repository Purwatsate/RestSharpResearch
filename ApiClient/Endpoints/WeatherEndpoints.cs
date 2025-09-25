using ApiClient.Exceptions;
using ApiClient.Services.Interface;

namespace ApiClient.Endpoints
{
    public static class WeatherEndpoints
    {
        public static void MapWeatherEndpoints(this WebApplication app)
        {
            app.MapGet("/test", async (IMyApiClient apiClient, CancellationToken token) =>
            {
                try
                {
                    var data = await apiClient.GetAsync<dynamic>("/weather", token);
                    return Results.Ok(data);
                }
                catch (ApiException ex)
                {
                    return Results.Problem($"Call failed ({(int)ex.StatusCode}): {ex.Message}");
                }
            });

            app.MapGet("/test-error", async (IMyApiClient apiClient, CancellationToken token) =>
            {
                try
                {
                    var data = await apiClient.GetAsync<dynamic>("/fail", token);
                    return Results.Ok(data);
                }
                catch (ApiException ex)
                {
                    return Results.Problem($"Server error ({(int)ex.StatusCode}): {ex.Message}");
                }
            });
        }
    }
}
