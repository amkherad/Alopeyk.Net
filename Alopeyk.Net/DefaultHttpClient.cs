using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Alopeyk.Net
{
    public class DefaultHttpClient : IHttpClient, IDisposable
    {
        public HttpClient HttpClient { get; }

        public DefaultHttpClient()
        {
            HttpClient = new HttpClient();
        }

        public DefaultHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public Task<HttpResponseMessage> Send(
            HttpRequestMessage message,
            CancellationToken cancellationToken
        )
        {
            return HttpClient.SendAsync(message, cancellationToken);
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}