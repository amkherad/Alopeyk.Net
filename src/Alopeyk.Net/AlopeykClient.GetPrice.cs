using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetPrice;
using Alopeyk.Net.DTOs.GetPrice;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private const string GetPriceV2EndpointPath = "v2/orders/price/calc";
        
        
        public virtual async Task<BaseResponseDto<GetPriceResponseDto>> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var latlng = Uri.EscapeDataString($"{request.Latitude} {request.Longitude}");

            var path = CreatePath(GetPriceV2EndpointPath);

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            do
            {
                try
                {
                    response = await HttpClient.GetAsync($"{path}?latlng={latlng}", cancellationToken);

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

            var result = JsonSerializer.Deserialize<GetLocationResponseRDto>(responseStream);

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
                    : new GetLocationResponseDto
                    {
                        Province = obj.province,
                        City = obj.city,
                        CityFa = obj.city_fa,
                        District = obj.district,
                        Region = obj.region,
                        Address = obj.address,
                        TrafficZone = obj.traffic_zone is null
                            ? null
                            : new TrafficZoneDto
                            {
                                Congestion = obj.traffic_zone.congestion,
                                OddEven = obj.traffic_zone.odd_even
                            }
                    }
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
} 