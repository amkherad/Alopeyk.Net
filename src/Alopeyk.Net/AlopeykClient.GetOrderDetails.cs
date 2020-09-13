using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetOrderDetails;
using Alopeyk.Net.Dto.GetOrderDetails.Remote;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public async Task<BaseResponseDto<GetOrderDetailsResponseDto>> GetOrderDetails(
            GetOrderDetailsRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = GetOrderDetailsV2EndpointPath.Replace(OrderIdPlaceholder, request.OrderId);

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);
            
            do
            {
                try
                {
                    response = await Send(new HttpRequestMessage(HttpMethod.Get, path), cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<GetOrderDetailsResponseDto>(response);
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

            var result = JsonSerializer.Deserialize<RemoteBaseResponseDto<GetOrderDetailsResponseRemoteDto>>(responseStream);

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
                    : MapGetOrderDetailsResponseRDtoToResponseDto(obj)
            };

            BindBaseResponse(output, response);

            return output;
        }

        private GetOrderDetailsResponseDto MapGetOrderDetailsResponseRDtoToResponseDto(
            GetOrderDetailsResponseRemoteDto response
        )
        {
            var result = new GetOrderDetailsResponseDto();

            MapGetOrderDetailsResponseRDtoToResponseDto(result, response);

            return result;
        }

        private void MapGetOrderDetailsResponseRDtoToResponseDto(
            GetOrderDetailsResponseDto result,
            GetOrderDetailsResponseRemoteDto obj
        )
        {
            result.TransportType = StringToTransportType(obj.transport_type);
            result.Status = FormatOrderStatusCode(obj.status);
            result.Addresses = obj.addresses?.Select(a => new GetOrderDetailsAddressResponseDto
            {
                Id = a.id,
                Type = a.type,
                Status = a.status,
                Address = a.address,
                City = a.city,
                Description = a.description,
                Distance = a.distance,
                Duration = a.duration,
                Latitude = a.lat,
                Longitude = a.lng,
                Number = a.number,
                Priority = a.priority,
                Signature = a.signature is null
                    ? null
                    : new ResourceDescriptorDto
                    {
                        Url = a.signature.Url
                    },
                Unit = a.unit,
                ArrivedAt = a.arrived_at,
                ArriveLatitude = a.arrive_lat,
                ArriveLongitude = a.arrive_lng,
                CityFa = a.city_fa,
                CourierId = a.courier_id,
                CreatedAt = a.created_at,
                CustomerId = a.customer_id,
                DeletedAt = a.deleted_at,
                HandledAt = a.handled_at,
                HandleLatitude = a.handle_lat,
                HandleLongitude = a.handle_lng,
                OrderId = a.order_id,
                PersonPhone = a.person_phone,
                SignedBy = a.signed_by,
                UpdatedAt = a.updated_at,
                PersonFullName = a.person_fullname
            }).ToArray();
            result.Cashed = obj.cashed;
            result.City = obj.city;
            result.Comment = obj.comment;
            result.Credit = obj.credit;
            result.Delay = obj.delay;
            result.Distance = obj.distance;
            result.Duration = obj.duration;
            result.Id = obj.id;
            result.Price = obj.price;
            result.Progress = obj.progress;
            result.Rate = obj.rate;
            result.Screenshot = obj.screenshot is null
                ? null
                : new ResourceDescriptorDto
                {
                    Url = obj.screenshot.Url
                };
            result.Signature = obj.signature is null
                ? null
                : new ResourceDescriptorDto
                {
                    Url = obj.signature.Url
                };
            result.Subsidy = obj.subsidy;
            result.Weight = obj.weight;
            result.AcceptedAt = obj.accepted_at;
            result.AcceptLatitude = obj.accept_lat;
            result.AcceptLongitude = obj.accept_lng;
            result.AddressesTimeline = obj.addresses_timeline?.Select(at => new GetOrderDetailsAddressesTimeline
            {
                Id = at.id,
                Priority = at.priority,
                Signature = at.signature is null
                    ? null
                    : new ResourceDescriptorDto
                    {
                        Url = at.signature.Url
                    },
                Status = at.status,
                Type = at.type,
                CityFa = at.city_fa
            }).ToArray();
            result.CancelledBy = obj.cancelled_by;
            result.CourierId = obj.courier_id;
            result.CourierInfo = obj.courier_info is null
                ? null
                : new GetOrderDetailsCourierInfo
                {
                    Avatar = obj.courier_info.avatar is null
                        ? null
                        : new ResourceDescriptorDto
                        {
                            Url = obj.courier_info.avatar.Url
                        },
                    Email = obj.courier_info.email,
                    Id = obj.courier_info.id,
                    Phone = obj.courier_info.phone,
                    AbsAvatar = obj.courier_info.abs_avatar is null
                        ? null
                        : new ResourceDescriptorDto
                        {
                            Url = obj.courier_info.abs_avatar.Url
                        },
                    FirstName = obj.courier_info.firstname,
                    IsOnline = obj.courier_info.is_online ?? false,
                    LastName = obj.courier_info.lastname,
                    PlateNumber = obj.courier_info.phone,
                    RatesAverage = obj.courier_info.rates_avg,
                    ReferralCode = obj.courier_info.referral_code,
                    LastOnline = obj.courier_info.last_online
                };
            result.CourierVehicle = obj.courier_vehicle;
            result.CreatedAt = obj.created_at;
            result.CustomerId = obj.customer_id;
            result.CustomerScore = obj.customerScore;
            result.DeletedAt = obj.deleted_at;
            result.DeliveredAt = obj.delivered_at;
            result.DeliveringAt = obj.delivering_at;
            result.DeviceId = obj.device_id;
            result.FinalPrice = obj.final_price;
            result.HasReturn = obj.has_return;
            result.IsApi = obj.is_api;
            result.IsVip = obj.is_vip;
            result.InvoiceNumber = obj.invoice_number;
            result.NPrice = obj.nprice;
            result.SignedBy = obj.signed_by;
            result.OrderToken = obj.order_token;
            result.OrderDiscount = obj.order_discount;
            result.ScoreCalc = obj.score_calc is null
                ? null
                : new GetOrderDetailsScoreCalc
                {
                    Score = obj.score_calc.score,
                    ScoreDetail = obj.score_calc.score_detail
                };
            result.PickingAt = obj.picking_at;
            result.UpdatedAt = obj.updated_at;
            result.LaunchedOrCreatedAt = obj.launched_or_created_at;
            result.TrafficOddEvenZone = obj.traffic_odd_even_zone;
            result.TrafficCongestionZone = obj.traffic_congestion_zone;
            result.PayAtDest = obj.pay_at_dest;
            result.EtaMinimal = obj.eta_minimal is null
                ? null
                : new GetOrderDetailsEtaMinimal
                {
                    Action = obj.eta_minimal.action,
                    Distance = obj.eta_minimal.distance,
                    Duration = obj.eta_minimal.duration,
                    Id = obj.eta_minimal.id,
                    AddressId = obj.eta_minimal.address_id,
                    UpdatedAt = obj.eta_minimal.updated_at,
                    LastPositionId = obj.eta_minimal.last_position_id
                };
            result.LaunchedAt = obj.launched_at;
            result.FinishedAt = obj.finished_at;
            result.RemovedAt = obj.removed_at;
            result.ScheduledAt = obj.scheduled_at;
            result.StoppedAt = obj.stopped_at;
            result.NextAddressAnyFull = null;
            result.LastPositionMinimal = null;
            result.ExtraParam = null;
        }
    }
}