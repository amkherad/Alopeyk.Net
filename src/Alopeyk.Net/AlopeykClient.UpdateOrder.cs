using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.UpdateOrder;
using Alopeyk.Net.Dto.UpdateOrder.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<UpdateOrderResponseDto>> UpdateOrder(
            UpdateOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var payload = new UpdateOrderRequestRemoteDto
            {
                transport_type = TransportTypeToString(request.TransportType),
                cashed = request.Cashed,
                credit = request.Credit,
                delay = request.Delay,
                has_return = request.HasReturn,
                pay_at_dest = request.PayAtDest
            };
            var payloadJson = JsonSerializer.Serialize(payload);

            var path = UpdateOrderV2EndpointPath.Replace(OrderIdPlaceholder, request.OrderId);

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
                        new HttpRequestMessage(HttpMethod.Put, path)
                        {
                            Content = content
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<UpdateOrderResponseDto>(response);
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

            var result =
                JsonSerializer.Deserialize<RemoteBaseResponseDto<UpdateOrderResponseRemoteDto>>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            UpdateOrderResponseDto resultObject = null;
            if (!(obj is null))
            {
                resultObject = new UpdateOrderResponseDto();
                MapGetOrderDetailsResponseRDtoToResponseDto(resultObject, obj);
            }

            var output = new BaseResponseDto<UpdateOrderResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = resultObject
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}