using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetLocationSuggestions;
using Alopeyk.Net.Dto.GetLocationSuggestions.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<GetLocationSuggestionsResponseDto[]>> GetLocationSuggestions(
            GetLocationSuggestionsRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var input = Uri.EscapeDataString(request.Input);

            var path = GetLocationSuggestionsV2EndpointPath;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);
            
            do
            {
                try
                {
                    path = $"{path}?input={input}";
                    
                    response = await Send(new HttpRequestMessage(HttpMethod.Get, path), cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<GetLocationSuggestionsResponseDto[]>(response);
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

            var result = JsonSerializer.Deserialize<RemoteBaseResponseDto<GetLocationSuggestionsResponseRemoteDto[]>>(responseStream);

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