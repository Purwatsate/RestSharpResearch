using ApiClient.Services.Interface;
using ApiShared.Settings;
using Microsoft.Extensions.Options;

namespace ApiClient.Services
{
    public class BearerTokenProvider(IOptions<ApiClientSettings> settings) : ITokenProvider
    {
        public string GetToken()
        {
            Console.WriteLine($"using username {settings.Value.Username} : password {settings.Value.Password}");
            return "This is Token";
        }
    }
}
