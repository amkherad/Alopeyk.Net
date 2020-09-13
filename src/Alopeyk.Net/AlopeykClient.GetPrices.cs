using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetPrice;
using Alopeyk.Net.Dto.GetPrice.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<IEnumerable<GetPriceResponseDto>>> GetPrices(
            IEnumerable<GetPriceRequestDto> requests,
            CancellationToken cancellationToken
        )
        {
            if (requests is null) throw new ArgumentNullException(nameof(requests));

            var payload = requests.Select(GetPriceLocalRequestToRemoteRequest);
            var payloadJson = JsonSerializer.Serialize(payload);

            var path = GetPricesV2EndpointPath;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            var content = new StringContent(payloadJson, Encoding.UTF8, ApplicationJsonMime);

            path = CreatePath(path);
            
            do
            {
                try
                {
                    response = await Send(
                        new HttpRequestMessage(HttpMethod.Post, path)
                        {
                            Content = content
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<IEnumerable<GetPriceResponseDto>>(response);
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

            var result = JsonSerializer.Deserialize<RemoteBaseResponseDto<GetPriceResponseRemoteDto[]>>(responseStream);

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