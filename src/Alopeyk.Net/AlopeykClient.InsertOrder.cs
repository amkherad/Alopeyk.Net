using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.InsertOrder;
using Alopeyk.Net.Dto.InsertOrder.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<InsertOrderResponseDto>> InsertOrder(
            InsertOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            if (request.Addresses is null)
            {
                throw new AlopeykException("Addresses must have value for GetPrice()");
            }

            if (request.Addresses.Origin is null || request.Addresses.Destinations is null ||
                !request.Addresses.Destinations.Any())
            {
                throw new AlopeykException("Addresses must have origin and at least one destination for GetPrice()");
            }

            var payload = new InsertOrderRequestRemoteDto
            {
                transport_type = TransportTypeToString(request.TransportType),
                cashed = request.Cashed,
                delay = request.DelayInMinutes,
                has_return = request.HasReturn,
                scheduled_at = request.ScheduledAt,
                extra_param = request.ExtraParam,

                addresses = request.Addresses.Destinations.Select(
                    dst => new InsertOrderAddressRequestRemoteDto
                    {
                        type = "destination",
                        lng = dst.Longitude,
                        lat = dst.Latitude,
                        description = dst.Description,
                        number = dst.Number ?? "",
                        person_fullname = dst.PersonFullName,
                        person_phone = dst.PersonPhone,
                        unit = dst.Unit ?? "",
                    }
                ).Prepend(new InsertOrderAddressRequestRemoteDto
                {
                    type = "origin",
                    lng = request.Addresses.Origin.Longitude,
                    lat = request.Addresses.Origin.Latitude,
                    description = request.Addresses.Origin.Description,
                    number = request.Addresses.Origin.Number ?? "",
                    person_fullname = request.Addresses.Origin.PersonFullName,
                    person_phone = request.Addresses.Origin.PersonPhone,
                    unit = request.Addresses.Origin.Unit ?? "",
                }).ToArray()
            };
            var payloadJson = JsonSerializer.Serialize(payload);

            var path = InsertOrderV2EndpointPath;

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
                        return await ThrowOnInvalidStatusCode<InsertOrderResponseDto>(response);
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
                JsonSerializer.Deserialize<RemoteBaseResponseDto<InsertOrderResponseRemoteDto>>(responseStream);

            if (result is null)
            {
                throw new AlopeykException("Object was empty in alopeyk's response.");
            }

            var obj = result.@object;

            var output = new BaseResponseDto<InsertOrderResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = obj is null
                    ? null
                    : new InsertOrderResponseDto
                    {
                        Id = obj.id,
                        TransportType = StringToTransportType(obj.transport_type),
                        Status = FormatOrderStatusCode(obj.status),
                        Cashed = obj.cashed,
                        City = obj.city,
                        Credit = obj.credit,
                        Delay = obj.delay,
                        Distance = obj.distance,
                        Duration = obj.duration,
                        Price = obj.price,
                        Signature = obj.signature is null
                            ? null
                            : new ResourceDescriptorDto
                            {
                                Url = obj.signature.Url
                            },
                        Subsidy = obj.subsidy,
                        Weight = obj.weight,
                        CreatedAt = obj.created_at,
                        CustomerId = obj.customer_id,
                        DeviceId = obj.device_id,
                        ExtraParam = obj.extra_param,
                        FinalPrice = obj.final_price,
                        HasReturn = obj.has_return,
                        InvoiceNumber = obj.invoice_number,
                        IsApi = obj.is_api,
                        IsVip = obj.is_vip,
                        LaunchedAt = obj.launched_at,
                        NPrice = obj.nprice,
                        OrderDiscount = obj.order_discount,
                        OrderToken = obj.order_token,
                        ScoreInfo = obj.score_calc is null
                            ? null
                            : new InsertOrderScoreInfoDto
                            {
                                Score = obj.score_calc.score,
                                ScoreDetail = obj.score_calc.score_detail
                            },
                        SignedBy = obj.signed_by,
                        UpdatedAt = obj.updated_at,
                        PayAtDest = obj.pay_at_dest,
                        TrafficCongestionZone = obj.traffic_congestion_zone,
                        TrafficOddEvenZone = obj.traffic_odd_even_zone
                    }
            };

            BindBaseResponse(output, response);

            return output;
        }
    }
}