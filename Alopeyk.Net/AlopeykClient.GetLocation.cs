using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private class traffic_zone_model
        {
            public bool congestion { get; set; }

            public bool odd_even { get; set; }
        }

        private class object_model
        {
            public string[] address { get; set; }

            public string region { get; set; }

            public string district { get; set; }

            public string city { get; set; }

            public traffic_zone_model traffic_zone { get; set; }

            public string city_fa { get; set; }

            public string province { get; set; }
        }

        private class GetLocationResponseRDto
        {
            public string status { get; set; }

            public string message { get; set; }

            public object_model @object { get; set; }
        }

        public virtual async Task<BaseResponseDto<GetLocationResponseDto>> GetLocation(
            GetLocationRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var latlng = Uri.EscapeDataString($"{request.Latitude} {request.Longitude}");

            var path = CreatePath(GetLocationV2EndpointPath);

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
                        ThrowInvalidStatusCode(response);
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

            var output = new BaseResponseDto<GetLocationResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = new GetLocationResponseDto
                {
                }
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}