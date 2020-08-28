using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetPrice;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private const string GetPricesV2EndpointPath = "v2/orders/batch-price";

        private class GetPricesResponseRDto
        {
            public string status { get; set; }
            
            public string message { get; set; }
            
            public GetPriceResponseItemRDto[] @object { get; set; }
        }
        
        public async Task<BaseResponseDto<IEnumerable<GetPriceResponseDto>>> GetPrices(
            IEnumerable<GetPriceRequestDto> requests,
            CancellationToken cancellationToken
        )
        {
            if (requests is null) throw new ArgumentNullException(nameof(requests));

            var payload = requests.Select(GetPriceLocalRequestToRemoteRequest);
            var payloadJson = JsonSerializer.Serialize(payload);

            var path = CreatePath(GetPricesV2EndpointPath);

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;
            
            var content = new StringContent(payloadJson, Encoding.UTF8, ApplicationJsonMime);

            do
            {
                try
                {
                    response = await HttpClient.PostAsync(path, content, cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        ThrowOnInvalidStatusCode(response);
                        return null; //unreachable code.
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseStream = await response.Content.ReadAsStreamAsync();

            var result = JsonSerializer.Deserialize<GetPricesResponseRDto>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var output = new BaseResponseDto<IEnumerable<GetPriceResponseDto>>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = result.@object?
                    .Select(GetPriceRemoteResponseToLocalResponse)
                    .ToArray()
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}