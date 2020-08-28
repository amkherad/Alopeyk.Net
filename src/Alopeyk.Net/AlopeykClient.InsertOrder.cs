using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.InsertOrder;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private const string InsertOrderV2EndpointPath = "v2/orders";


        private class InsertOrderAddressRequestRDto
        {
            public string type { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public string description { get; set; }
            public string unit { get; set; }
            public string number { get; set; }
            public string person_fullname { get; set; }
            public string person_phone { get; set; }
        }

        private class InsertOrderRequestRDto
        {
            public string transport_type { get; set; }

            public InsertOrderAddressRequestRDto[] addresses { get; set; }

            public bool has_return { get; set; }

            public int delay { get; set; }

            public string scheduled_at { get; set; }

            public bool cashed { get; set; }

            public object extra_params { get; set; }
        }


        private class InsertOrderScoreCalc
        {
            public int score { get; set; }
            public Dictionary<int, string> score_detail { get; set; }
        }

        private class InsertOrderResponseItemRDto
        {
            public string transport_type { get; set; }
            public int customer_id { get; set; }
            public string status { get; set; }
            public bool traffic_congestion_zone { get; set; }
            public bool traffic_odd_even_zone { get; set; }
            public DateTime launched_at { get; set; }
            public string city { get; set; }
            public int delay { get; set; }
            public int price { get; set; }
            public bool credit { get; set; }
            public bool cashed { get; set; }
            public bool has_return { get; set; }
            public int distance { get; set; }
            public int duration { get; set; }
            public string invoice_number { get; set; }
            public bool pay_at_dest { get; set; }
            public object device_id { get; set; }
            public int weight { get; set; }
            public bool is_api { get; set; }
            public bool is_vip { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime created_at { get; set; }
            public int id { get; set; }
            public RemoteResourceDto signature { get; set; }
            public string order_token { get; set; }
            public object nprice { get; set; }
            public object subsidy { get; set; }
            public string signed_by { get; set; }
            public int final_price { get; set; }
            public InsertOrderScoreCalc score_calc { get; set; }
            public object order_discount { get; set; }
            public object extra_param { get; set; }
            public object orderDiscount { get; set; }
        }


        private class InsertOrderResponseRDto
        {
            public string status { get; set; }

            public string message { get; set; }

            public InsertOrderResponseItemRDto @object { get; set; }
        }


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

            var payload = new InsertOrderRequestRDto
            {
                transport_type = TransportTypeToString(request.TransportType),
                cashed = request.Cashed,
                delay = request.DelayInMinutes,
                extra_params = request.ExtraParams,
                has_return = request.HasReturn,
                scheduled_at = request.ScheduledAt,

                addresses = request.Addresses.Destinations.Select(
                    dst => new InsertOrderAddressRequestRDto
                    {
                        type = "destination",
                        lng = dst.Longitude,
                        lat = dst.Latitude,
                        description = dst.Description,
                        number = dst.Number,
                        person_fullname = dst.PersonFullName,
                        person_phone = dst.PersonPhone,
                        unit = dst.Unit
                    }
                ).Prepend(new InsertOrderAddressRequestRDto
                {
                    type = "origin",
                    lng = request.Addresses.Origin.Longitude,
                    lat = request.Addresses.Origin.Latitude,
                    description = request.Addresses.Origin.Description,
                    number = request.Addresses.Origin.Number,
                    person_fullname = request.Addresses.Origin.PersonFullName,
                    person_phone = request.Addresses.Origin.PersonPhone,
                    unit = request.Addresses.Origin.Unit
                }).ToArray()
            };
            var payloadJson = JsonSerializer.Serialize(payload);

            var path = CreatePath(InsertOrderV2EndpointPath);

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            var content = new StringContent(payloadJson, Encoding.UTF8, ApplicationJsonMime);

            do
            {
                try
                {
                    response = await HttpClient.PostAsync(path, content, cancellationToken);

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

            var output = new BaseResponseDto<InsertOrderResponseDto>
            {
                Status = FormatStatusCode(result.status),
                Message = result.message,
                Object = obj is null
                    ? null
                    : new InsertOrderResponseDto
                    {
                        TransportType = StringToTransportType(obj.transport_type),
                        Status = obj.status,
                        Cashed = obj.cashed,
                        City = obj.city,
                        Credit = obj.credit,
                        Delay = obj.delay,
                        Distance = obj.distance,
                        Duration = obj.duration,
                        Id = obj.id,
                        Price = obj.price,
                        Signature = obj.signature is null
                            ? null
                            : new RemoteResourceDto
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