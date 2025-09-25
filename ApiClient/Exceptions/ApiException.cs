using System.Net;

namespace ApiClient.Exceptions
{
    public class ApiException(HttpStatusCode code, string message) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = code;
    }
}
