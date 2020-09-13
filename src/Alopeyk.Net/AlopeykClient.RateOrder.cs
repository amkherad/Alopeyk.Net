using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.RateOrder;
using Alopeyk.Net.Dto.RateOrder.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<RateOrderResponseDto>> RateOrder(
            RateOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = RateOrderV2EndpointPath.Replace(OrderIdPlaceholder, request.OrderId);

            var payload = new RateOrderRequestRemoteDto
            {
                rate = request.Rate,
                comment = request.Comment
            };
            var payloadJson = JsonSerializer.Serialize(payload);

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
                        return await ThrowOnInvalidStatusCode<RateOrderResponseDto>(response);
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

            var result = JsonSerializer.Deserialize<RemoteBaseResponseDto<RateOrderResponseRemoteDto>>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<RateOrderResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = obj is null
                    ? null
                    : new RateOrderResponseDto
                    {
                        TransportType = StringToTransportType(obj.transport_type),
                        Id = obj.id,
                        City = obj.city,
                        Price = obj.price,
                        Rate = obj.rate,
                        Signature = obj.signature is null
                            ? null
                            : new ResourceDescriptorDto
                            {
                                Url = obj.signature.Url
                            },
                        Status = obj.status,
                        Subsidy = obj.subsidy,
                        AcceptedAt = obj.accepted_at,
                        CourierId = obj.courier_id,
                        CustomerId = obj.customer_id,
                        FinalPrice = obj.final_price,
                        NPrice = obj.nprice,
                        OrderToken = obj.order_token,
                        SignedBy = obj.signed_by,
                        NextAddressAny = obj.next_address_any
                    }
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}