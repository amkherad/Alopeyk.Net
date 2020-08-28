using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetLocationSuggestions;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private const string GetLocationSuggestionsV2EndpointPath = "v2/locations";

        private class GetLocationSuggestionsItemRDto
        {
            public string title { get; set; }

            public string region { get; set; }

            public double lat { get; set; }

            public double lng { get; set; }

            public string district { get; set; }

            public string city { get; set; }

            public string city_fa { get; set; }
        }

        private class GetLocationSuggestionsRDto
        {
            public string status { get; set; }

            public string message { get; set; }

            public GetLocationSuggestionsItemRDto[] @object { get; set; }
        }

        public async Task<BaseResponseDto<GetLocationSuggestionsResponseDto[]>> GetLocationSuggestions(
            GetLocationSuggestionsRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var input = Uri.EscapeDataString(request.Input);

            var path = CreatePath(GetLocationSuggestionsV2EndpointPath);

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            do
            {
                try
                {
                    response = await HttpClient.GetAsync($"{path}?input={input}", cancellationToken);

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

            var result = JsonSerializer.Deserialize<GetLocationSuggestionsRDto>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<GetLocationSuggestionsResponseDto[]>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = obj?.Select(o => new GetLocationSuggestionsResponseDto
                {
                    Title = o.title,
                    City = o.city,
                    CityFa = o.city_fa,
                    District = o.district,
                    Region = o.region,
                    Latitude = o.lat,
                    Longitude = o.lng
                }).ToArray()
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}