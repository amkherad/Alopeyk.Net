using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetLocation;
using Alopeyk.Net.Dto.GetLocation.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public virtual async Task<BaseResponseDto<GetLocationResponseDto>> GetLocation(
            GetLocationRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var latlng = Uri.EscapeDataString($"{request.Latitude} {request.Longitude}");

            var path = GetLocationV2EndpointPath;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);
            
            do
            {
                try
                {
                    path = $"{path}?latlng={latlng}";
                    
                    response = await Send(new HttpRequestMessage(HttpMethod.Get, path), cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<GetLocationResponseDto>(response);
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

            var result = JsonSerializer.Deserialize<RemoteBaseResponseDto<GetLocationResponseRemoteDto>>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<GetLocationResponseDto>
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