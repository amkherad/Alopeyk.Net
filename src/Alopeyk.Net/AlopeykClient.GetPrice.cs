using System;
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
        public virtual async Task<BaseResponseDto<GetPriceResponseDto>> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var payload = GetPriceLocalRequestToRemoteRequest(request);
            var payloadJson = JsonSerializer.Serialize(payload);

            var path = GetPriceV2EndpointPath;

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
                        return await ThrowOnInvalidStatusCode<GetPriceResponseDto>(response);
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

            var result = JsonSerializer.Deserialize<RemoteBaseResponseDto<GetPriceResponseRemoteDto>>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<GetPriceResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = obj is null
                    ? null
                    : GetPriceRemoteResponseToLocalResponse(obj)
            };

            BindBaseResponse(output, response);

            return output;
        }

        private GetPriceRequestRemoteDto GetPriceLocalRequestToRemoteRequest(
            GetPriceRequestDto request
        )
        {
            if (request.Addresses is null)
            {
                throw new AlopeykException("Addresses must have value for GetPrice()");
            }

            if (request.Addresses.Origin is null || request.Addresses.Destinations is null ||
                !request.Addresses.Destinations.Any())
            {
                throw new AlopeykException("Addresses must have origin and at least one destination for GetPrice()");
            }

            return new GetPriceRequestRemoteDto
            {
                transport_type = TransportTypeToString(request.TransportType),
                cached = request.Cashed,
                has_return = request.HasReturn,
                //optimized = request.Optimized,
                addresses = request.Addresses.Destinations.Select(
                    dst => new GetPriceAddressRequestRemoteDto
                    {
                        type = "destination",
                        lng = dst.Longitude,
                        lat = dst.Latitude,
                    }
                ).Prepend(new GetPriceAddressRequestRemoteDto
                {
                    type = "origin",
                    lng = request.Addresses.Origin.Longitude,
                    lat = request.Addresses.Origin.Latitude,
                }).ToArray()
            };
        }

        private GetPriceResponseDto GetPriceRemoteResponseToLocalResponse(
            GetPriceResponseRemoteDto obj
        )
        {
            return new GetPriceResponseDto
            {
                TransportType = StringToTransportType(obj.transport_type),
                Status = FormatOrderStatusCode(obj.status),
                Cached = obj.cashed,
                City = obj.city,
                CityFa = obj.city_fa,
                Credit = obj.credit,
                Delay = obj.delay,
                Distance = obj.distance,
                Duration = obj.duration,
                Price = obj.price,
                Scheduled = obj.scheduled,
                FailedDiscount = obj.failed_discount,
                FinalPrice = obj.final_price,
                Score = obj.score,
                ScoreDetail = obj.score_detail,
                HasReturn = obj.has_return,
                UserCredit = obj.user_credit,
                Discount = obj.discount,
                DiscountCoupons = obj.discount_coupons,
                FailedDiscountCoupons = obj.failed_discount_coupons,
                FailedFinalPrice = obj.failed_final_price,
                InvalidDiscountCoupons = obj.invalid_discount_coupons,
                PriceWithReturn = obj.price_with_return
            };
        }
    }
}