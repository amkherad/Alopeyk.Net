using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.CancelOrder;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private const string CancelOrderV2EndpointPath = "v2/orders/{order_id}/cancel";


        private class CancelOrderResponseObjectRDto
        {
            public int id { get; set; }

            public string status { get; set; }

            public int courier_id { get; set; }

            public int customer_id { get; set; }

            public RemoteResourceDto signature { get; set; }

            public string order_token { get; set; }

            public string nprice { get; set; }

            public string subsidy { get; set; }

            public string signed_by { get; set; }

            public string final_price { get; set; }
        }

        private class CancelOrderResponseRDto
        {
            public string status { get; set; }

            public string message { get; set; }

            public CancelOrderResponseObjectRDto @object { get; set; }
        }


        public async Task<BaseResponseDto<CancelOrderResponseDto>> CancelOrder(
            CancelOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = CancelOrderV2EndpointPath.Replace(OrderIdPlaceholder, request.OrderId);

            path = CreatePath(path);

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            do
            {
                try
                {
                    response = await HttpClient.GetAsync(path, cancellationToken);

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

            var result = JsonSerializer.Deserialize<CancelOrderResponseRDto>(responseStream);

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
                        : new SignatureDto
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