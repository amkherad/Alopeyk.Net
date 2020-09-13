using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.AddHiddenDescription;
using Alopeyk.Net.Dto.AddHiddenDescription.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<AddHiddenDescriptionResponseDto>> AddHiddenDescription(
            AddHiddenDescriptionRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = AddHiddenDescriptionV2EndpointPath.Replace(OrderIdPlaceholder, request.OrderId);
            path = path.Replace(AddressIdPlaceholder, request.AddressId);

            var payload = new AddHiddenDescriptionRequestRemoteDto
            {
                description = request.Description
            };
            var payloadJson = JsonSerializer.Serialize(payload);

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
                        return await ThrowOnInvalidStatusCode<AddHiddenDescriptionResponseDto>(response);
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

            var result = JsonSerializer.Deserialize<RemoteBaseResponseDto<AddHiddenDescriptionResponseRemoteDto>>(
                responseStream
            );

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<AddHiddenDescriptionResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = new AddHiddenDescriptionResponseDto
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