using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Alopeyk.Net
{
    public class AlopeykClient : IAlopeykClient, IDisposable
    {
        public AlopeykApiInfoDto ApiInfo { get; }

        public IHttpClient HttpClient { get; }

        public IJsonSerializer JsonSerializer { get; }

        public string LocationURI { get; set; }

        public AlopeykClient(
            AlopeykApiInfoDto apiInfo,
            IHttpClient httpClient,
            IJsonSerializer jsonSerializer
        )
        {
            ApiInfo = apiInfo;
            HttpClient = httpClient;
            JsonSerializer = jsonSerializer;
        }


        public virtual void Dispose()
        {
        }


        public virtual async Task<object> GetLocation(
            double latitude,
            double longitude,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public virtual Task<object> GetPrice(
            string appId,
            int orderId,
            string transportType,
            string city,
            object[] addresses,
            bool hasReturn,
            bool cached,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public virtual Task<object> TakeAlopeyk(
            string appId,
            int orderId,
            string transportType,
            string city,
            object[] addresses,
            bool hasReturn,
            bool cashed,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public virtual Task<object> GetAlopeykOrderStatus(
            long transferId,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public virtual Task<object> CancelAlopeyk(
            long transferId,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }
    }
}