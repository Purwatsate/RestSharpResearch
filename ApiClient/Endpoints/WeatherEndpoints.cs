using ApiClient.Exceptions;
using ApiClient.Services.Interface;
using ApiShared.Models;

namespace ApiClient.Endpoints
{
    public static class WeatherEndpoints
    {
        public static void MapWeatherEndpoints(this WebApplication app)
        {
            app.MapGet(
                "/test/{r1}/{r2}",
                async (
                    IMyApiClient apiClient,
                    string r1,
                    string r2,
                    string q1,
                    string q2,
                    CancellationToken ct) =>
                {
                    try
                    {
                        var queryVars = new Dictionary<string, object>
                        {
                            { "q1_modifed", (int.TryParse(q1, out int q1Number) ? q1Number : 0)   + 100 },
                            { "q2_modified", (int.TryParse(q2, out int q2Number) ? q2Number : 0)   + 100 }
                        };

                        var routeVars = new Dictionary<string, object>
                        {
                            { "r1", r1 },
                            { "r2", r2 }
                        };

                        var data = await apiClient.GetAsync<WeatherDto>(
                            "/weather/{r1}/{r2}",
                            routeVariables: routeVars,
                            queryParams: queryVars);

                        return Results.Ok(data);
                    }
                    catch (ApiException ex)
                    {
                        return Results.Problem(
                            $"Call failed ({(int)ex.StatusCode}): {ex.Message}");
                    }
                }
            );

            app.MapGet(
                "/test-error",
                async (IMyApiClient apiClient, CancellationToken token) =>
                {
                    try
                    {
                        var data = await apiClient.GetAsync<dynamic>("/fail", token);
                        return Results.Ok(data);
                    }
                    catch (ApiException ex)
                    {
                        return Results.Problem(
                            $"Server error ({(int)ex.StatusCode}): {ex.Message}");
                    }
                }
            );
        }
    }
}
