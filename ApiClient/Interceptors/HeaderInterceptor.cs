using RestSharp.Interceptors;

namespace ApiClient.Interceptors
{
    class HeaderInterceptor(string headerName, string headerValue) : Interceptor
    {
        public override ValueTask BeforeHttpRequest(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            requestMessage.Headers.Add(headerName, headerValue);
            return ValueTask.CompletedTask;
        }
    }
}
