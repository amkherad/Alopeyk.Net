using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.DeleteHiddenDescription;
using Alopeyk.Net.Dto.DeleteHiddenDescription.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<DeleteHiddenDescriptionResponseDto>> DeleteHiddenDescription(
            DeleteHiddenDescriptionRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = DeleteHiddenDescriptionV2EndpointPath.Replace(OrderIdPlaceholder, request.OrderId);
            path = path.Replace(AddressIdPlaceholder, request.AddressId);
            path = path.Replace(HiddenDescriptionIdPlaceholder, request.HiddenDescriptionId);

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);
            
            do
            {
                try
                {
                    response = await Send(
                        new HttpRequestMessage(HttpMethod.Delete, path),
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<DeleteHiddenDescriptionResponseDto>(response);
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

            var result =
                JsonSerializer.Deserialize<RemoteBaseResponseDto<DeleteHiddenDescriptionResponseRemoteDto>>(
                    responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<DeleteHiddenDescriptionResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = new DeleteHiddenDescriptionResponseDto
                {
                    Id = obj.id,
                    Description = obj.description,
                    AddressId = obj.address_id,
                    CreatedAt = obj.created_at,
                    OrderId = obj.order_id,
                    UpdatedAt = obj.updated_at,
                    UserId = obj.user_id
                }
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}