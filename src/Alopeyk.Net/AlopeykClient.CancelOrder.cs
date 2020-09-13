using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.CancelOrder;
using Alopeyk.Net.Dto.CancelOrder.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<CancelOrderResponseDto>> CancelOrder(
            CancelOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = CancelOrderV2EndpointPath.Replace(OrderIdPlaceholder, request.OrderId);

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);
            
            do
            {
                try
                {
                    response = await Send(
                        new HttpRequestMessage(HttpMethod.Get, path),
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<CancelOrderResponseDto>(response);
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
                JsonSerializer.Deserialize<RemoteBaseResponseDto<CancelOrderResponseRemoteDto>>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<CancelOrderResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = new CancelOrderResponseDto
                {
                    Id = obj.id,
                    Status = FormatOrderStatusCode(obj.status),
                    SignedBy = obj.signed_by,
                    Subsidy = obj.subsidy,
                    CourierId = obj.courier_id,
                    CustomerId = obj.customer_id,
                    FinalPrice = obj.final_price,
                    NPrice = obj.nprice,
                    OrderToken = obj.order_token,
                    Signature = obj.signature is null
                        ? null
                        : new ResourceDescriptorDto
                        {
                            Url = obj.signature.Url
                        }
                }
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}