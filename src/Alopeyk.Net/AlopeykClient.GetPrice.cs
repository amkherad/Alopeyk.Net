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
        private const string GetPriceV2EndpointPath = "v2/orders/price/calc";

        private class GetPriceAddressRequestRDto
        {
            public string type { get; set; }

            public double lat { get; set; }

            public double lng { get; set; }
        }

        private class GetPriceRequestRDto
        {
            public string transport_type { get; set; }

            public GetPriceAddressRequestRDto[] addresses { get; set; }

            public bool has_return { get; set; }

            public bool cached { get; set; }

            public bool optimize { get; set; }
        }
        
        private class GetPriceResponseAddress    {
            public string type { get; set; } 
            public double lat { get; set; } 
            public double lng { get; set; } 
            public string address { get; set; } 
            public string city { get; set; } 
            public string city_fa { get; set; } 
            public int priority { get; set; } 
            public int? distance { get; set; } 
            public int? duration { get; set; } 
            public double? coefficient { get; set; } 
            public int? price { get; set; } 
        }

        private class GetPriceResponseItemRDto
        {
            public GetPriceResponseAddress[] addresses { get; set; } 
            public int price { get; set; } 
            public bool credit { get; set; } 
            public int distance { get; set; } 
            public int duration { get; set; } 
            public string status { get; set; } 
            public int user_credit { get; set; } 
            public int delay { get; set; } 
            public string city { get; set; } 
            public string city_fa { get; set; } 
            public string transport_type { get; set; } 
            public bool has_return { get; set; } 
            public bool cashed { get; set; } 
            public int price_with_return { get; set; } 
            public int score { get; set; } 
            public Dictionary<int, string> score_detail { get; set; } 
            public int final_price { get; set; } 
            public int discount { get; set; } 
            public string[] discount_coupons { get; set; } 
            public string[] invalid_discount_coupons { get; set; } 
            public int failed_final_price { get; set; } 
            public int failed_discount { get; set; } 
            public string[] failed_discount_coupons { get; set; } 
            public bool scheduled { get; set; } 
        }

        private class GetPriceResponseRDto
        {
            public string status { get; set; }
            
            public string message { get; set; }
            
            public GetPriceResponseItemRDto @object { get; set; }
        }

        public virtual async Task<BaseResponseDto<GetPriceResponseDto>> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var payload = GetPriceLocalRequestToRemoteRequest(request);
            var payloadJson = JsonSerializer.Serialize(payload);

            var path = CreatePath(GetPriceV2EndpointPath);

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

            var result = JsonSerializer.Deserialize<GetPriceResponseRDto>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<GetPriceResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object =  obj is null
                    ? null
                    : GetPriceRemoteResponseToLocalResponse(obj)
            };

            BindBaseResponse(output, response);

            return output;
        }

        private GetPriceRequestRDto GetPriceLocalRequestToRemoteRequest(
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
            
            return new GetPriceRequestRDto
            {
                transport_type = TransportTypeToString(request.TransportType),
                cached = request.Cashed,
                has_return = request.HasReturn,
                optimize = request.Optimized,
                addresses = request.Addresses.Destinations.Select(
                    dst => new GetPriceAddressRequestRDto
                    {
                        type = "destination",
                        lng = dst.Longitude,
                        lat = dst.Latitude
                    }
                ).Prepend(new GetPriceAddressRequestRDto
                {
                    type = "origin",
                    lng = request.Addresses.Origin.Longitude,
                    lat = request.Addresses.Origin.Latitude
                }).ToArray()
            };
        } 

        private GetPriceResponseDto GetPriceRemoteResponseToLocalResponse(
            GetPriceResponseItemRDto obj
        )
        {
            return new GetPriceResponseDto
            {
                TransportType = StringToTransportType(obj.transport_type),
                Cached = obj.cashed,
                City = obj.city,
                CityFa = obj.city_fa,
                Credit = obj.credit,
                Delay = obj.delay,
                Distance = obj.distance,
                Duration = obj.duration,
                Price = obj.price,
                Scheduled = obj.scheduled,
                Status = obj.status,
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