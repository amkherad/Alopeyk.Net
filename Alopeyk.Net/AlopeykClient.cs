using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.DTOs.GetPrice;

namespace Alopeyk.Net
{
    public class AlopeykClient : IAlopeykClient, IDisposable
    {
        public AlopeykApiInfoDto ApiInfo { get; }

        public IHttpClient HttpClient { get; }

        public IJsonSerializer JsonSerializer { get; }

        public string LocationURI { get; set; }

        private const string APPLICATION_JSON = "application/json";


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

        public virtual async Task<GetPriceResponseDto> GetPrice(
            GetPriceRequestDto requestDto,
            CancellationToken cancellationToken
        )
        {
            var request = new HttpRequestMessage(HttpMethod.Post, ApiInfo.RemoteServiceUri)
            {
                Content = new StringContent(JsonSerializer.Serialize(requestDto),
                    Encoding.UTF8, APPLICATION_JSON)
            };


            var response = await HttpClient.Send(request, cancellationToken);

            return JsonSerializer.Deserialize<GetPriceResponseDto>(await response.Content.ReadAsStreamAsync());
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