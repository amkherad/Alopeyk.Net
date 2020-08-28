using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetPrice;
using Alopeyk.Net.Dto.RateOrder;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private const string RateOrderV2EndpointPath =
            "https://sandbox-api.alopeyk.com/api/v2/orders/{order_id}/finish";


        private class RateOrderRequestRDto
        {
            public int Rate { get; set; }

            public string Comment { get; set; }
        }

        private class RateOrderResponseItemRDto
        {
            public int id { get; set; }
            public string status { get; set; }
            public int courier_id { get; set; }
            public int customer_id { get; set; }
            public DateTime accepted_at { get; set; }
            public string city { get; set; }
            public string transport_type { get; set; }
            public int price { get; set; }
            public int rate { get; set; }
            public object next_address_any { get; set; }
            public RemoteResourceDto signature { get; set; }
            public object order_token { get; set; }
            public string nprice { get; set; }
            public object subsidy { get; set; }
            public string signed_by { get; set; }
            public int final_price { get; set; }
        }

        private class RateOrderResponseRDto
        {
            public string status { get; set; }

            public string message { get; set; }

            public RateOrderResponseItemRDto @object { get; set; }
        }


        public async Task<BaseResponseDto<RateOrderResponseDto>> RateOrder(
            RateOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = CreatePath(RateOrderV2EndpointPath);
            path = path.Replace(OrderIdPlaceholder, request.OrderId);

            var payload = new RateOrderRequestRDto
            {
                Rate = request.Rate,
                Comment = request.Comment
            };
            var payloadJson = JsonSerializer.Serialize(payload);

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

            var result = JsonSerializer.Deserialize<RateOrderResponseRDto>(responseStream);

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
                            : new RemoteResourceDto
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