using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Alopeyk.Net
{
    public interface IHttpClient 
    {
        Task<HttpResponseMessage> Send(
            HttpRequestMessage message,
            CancellationToken cancellationToken
        );
    }
}