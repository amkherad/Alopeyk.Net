using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetOrderDetails;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private const string GetOrderStatusV2EndpointPath = "v2/orders/{order_id}";



        private class GetOrderDetailsResponseRDto
        {
            
        }
        
        
        public async Task<BaseResponseDto<GetOrderDetailsResponseDto>> GetOrderDetails(
            GetOrderDetailsRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = CreatePath(GetOrderStatusV2EndpointPath);
            path = path.Replace(OrderIdPlaceholder, request.OrderId);

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

            var result = JsonSerializer.Deserialize<InsertOrderResponseRDto>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<GetOrderDetailsResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = obj is null
                    ? null
                    : new GetOrderDetailsResponseDto
                    {
                        TransportType = StringToTransportType(obj.transport_type),
                        Status = obj.status,
                        
                    }
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}