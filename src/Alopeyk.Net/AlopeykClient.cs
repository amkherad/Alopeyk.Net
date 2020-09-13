using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net
{
    public partial class AlopeykClient : IAlopeykClient, IDisposable
    {
        private const string ApplicationJsonMime = "application/json";

        private const string OrderIdPlaceholder = "{order_id}";
        private const string AddressIdPlaceholder = "{address_id}";
        private const string HiddenDescriptionIdPlaceholder = "{hidden_description_id}";


        public Uri RemoteServiceUri { get; }

        public string Token { get; set; }

        public HttpClient HttpClient { get; }

        public bool DisposeHttpClient { get; set; } = true;

        public IJsonSerializer JsonSerializer { get; }

        public IRetryHandler RetryHandler { get; }


        public string AddHiddenDescriptionV2EndpointPath { get; set; } =
            "v2/orders/{order_id}/address/{address_id}/hidden_description";

        public string CancelOrderV2EndpointPath { get; set; } = "v2/orders/{order_id}/cancel";
        public string GetLocationV2EndpointPath { get; set; } = "v2/locations";

        public string DeleteHiddenDescriptionV2EndpointPath { get; set; } =
            "v2/orders/{order_id}/address/{address_id}/hidden_description/{hidden_description_id}";

        public string LiveTrackingUrlPattern { get; set; } = "https://sandbox-tracking.alopeyk.com/#/";
        public string GetLocationSuggestionsV2EndpointPath { get; set; } = "v2/locations";
        public string GetOrderDetailsV2EndpointPath { get; set; } = "v2/orders/{order_id}";
        public string GetPriceV2EndpointPath { get; set; } = "v2/orders/price/calc";
        public string GetPricesV2EndpointPath { get; set; } = "v2/orders/batch-price";
        public string InsertOrderV2EndpointPath { get; set; } = "v2/orders";
        public string UpdateOrderV2EndpointPath { get; set; } = "v2/orders/{order_id}";
        public string RateOrderV2EndpointPath { get; set; } = "v2/orders/{order_id}/finish";


        public AlopeykClient(
            Uri remoteServiceUri,
            string token,
            IJsonSerializer jsonSerializer
        )
            : this(remoteServiceUri, token, new HttpClient(), jsonSerializer, null)
        {
        }

        public AlopeykClient(
            Uri remoteServiceUri,
            string token,
            HttpClient httpClient,
            IJsonSerializer jsonSerializer
        )
            : this(remoteServiceUri, token, httpClient, jsonSerializer, null)
        {
        }

        public AlopeykClient(
            Uri remoteServiceUri,
            string token,
            HttpClient httpClient,
            IJsonSerializer jsonSerializer,
            IRetryHandler retryHandler
        )
        {
            if (remoteServiceUri is null) throw new ArgumentNullException(nameof(remoteServiceUri));
            if (httpClient is null) throw new ArgumentNullException(nameof(httpClient));
            if (jsonSerializer is null) throw new ArgumentNullException(nameof(jsonSerializer));

            RemoteServiceUri = remoteServiceUri;
            Token = token;
            HttpClient = httpClient;
            JsonSerializer = jsonSerializer;
            RetryHandler = retryHandler ?? Alopeyk.Net.RetryHandler.NoRetry.Value;
        }


        public virtual void Dispose()
        {
            if (DisposeHttpClient)
            {
                HttpClient.Dispose();
            }
        }

        protected virtual string CreatePath(
            string relativePath
        )
        {
            return AlopeykHelpers.JoinUrls(RemoteServiceUri.AbsoluteUri, relativePath);
        }

        protected virtual async Task<BaseResponseDto<T>> ThrowOnInvalidStatusCode<T>(
            HttpResponseMessage response
        )
        {
            try
            {
                var bodyStream = await response.Content.ReadAsStreamAsync();

                var model = JsonSerializer.Deserialize<BaseResponseDto<T>>(bodyStream);

                return model;
            }
            catch (Exception ex)
            {
                throw new AlopeykException(
                    $"Alopeyk remote service returned an invalid http status code, statusCode: {response.StatusCode}",
                    ex
                );
            }
        }

        protected virtual void BindBaseResponse<T>(
            BaseResponseDto<T> result,
            HttpResponseMessage response
        )
        {
            if (response.Headers.TryGetValues("X-MinuteRateLimit-Identifier",
                out var valuesMinuteRateLimitIdentifierStr))
            {
                result.MinuteRateLimitIdentifier = valuesMinuteRateLimitIdentifierStr.FirstOrDefault();
            }

            if (response.Headers.TryGetValues("X-MinuteRateLimit-Limit", out var valuesMinuteRateLimitLimitStr))
            {
                if (int.TryParse(valuesMinuteRateLimitLimitStr.FirstOrDefault(), out var valuesMinuteRateLimitLimit))
                {
                    result.MinuteRateLimitLimit = valuesMinuteRateLimitLimit;
                }
            }

            if (response.Headers.TryGetValues("X-MinuteRateLimit-Remaining", out var valuesMinuteRateLimitRemainingStr))
            {
                if (int.TryParse(valuesMinuteRateLimitRemainingStr.FirstOrDefault(),
                    out var valuesMinuteRateLimitRemaining))
                {
                    result.MinuteRateLimitRemaining = valuesMinuteRateLimitRemaining;
                }
            }


            if (response.Headers.TryGetValues("X-DailyRateLimit-Identifier", out var valuesDailyRateLimitIdentifierStr))
            {
                result.DailyRateLimitIdentifier = valuesDailyRateLimitIdentifierStr.FirstOrDefault();
            }

            if (response.Headers.TryGetValues("X-DailyRateLimit-Limit", out var valuesDailyRateLimitLimitStr))
            {
                if (int.TryParse(valuesDailyRateLimitLimitStr.FirstOrDefault(), out var valuesDailyRateLimitLimit))
                {
                    result.DailyRateLimitLimit = valuesDailyRateLimitLimit;
                }
            }

            if (response.Headers.TryGetValues("X-DailyRateLimit-Remaining", out var valuesDailyRateLimitRemainingStr))
            {
                if (int.TryParse(valuesDailyRateLimitRemainingStr.FirstOrDefault(),
                    out var valuesDailyRateLimitRemaining))
                {
                    result.DailyRateLimitRemaining = valuesDailyRateLimitRemaining;
                }
            }


            if (response.Headers.TryGetValues("X-RateLimit-Identifier", out var valuesRateLimitIdentifierStr))
            {
                result.RateLimitIdentifier = valuesRateLimitIdentifierStr.FirstOrDefault();
            }

            if (response.Headers.TryGetValues("X-RateLimit-Limit", out var valuesRateLimitLimitStr))
            {
                if (int.TryParse(valuesRateLimitLimitStr.FirstOrDefault(), out var valuesRateLimitLimit))
                {
                    result.RateLimitLimit = valuesRateLimitLimit;
                }
            }

            if (response.Headers.TryGetValues("X-RateLimit-Remaining", out var valuesRateLimitRemainingStr))
            {
                if (int.TryParse(valuesRateLimitRemainingStr.FirstOrDefault(),
                    out var valuesRateLimitRemaining))
                {
                    result.RateLimitRemaining = valuesRateLimitRemaining;
                }
            }
        }

        protected virtual AlopeykStatusCodes FormatStatusCode(
            string statusCode
        )
        {
            switch (statusCode?.ToLower())
            {
                case "success":
                {
                    return AlopeykStatusCodes.Success;
                }
                default:
                {
                    return AlopeykStatusCodes.Failure;
                }
            }
        }

        protected virtual AlopeykOrderStates FormatOrderStatusCode(
            string statusCode
        )
        {
            switch (statusCode?.ToLower())
            {
                case "new": return AlopeykOrderStates.New;
                case "searching": return AlopeykOrderStates.Searching;
                case "cancelled": return AlopeykOrderStates.Cancelled;
                case "expired": return AlopeykOrderStates.Expired;
                case "accepted": return AlopeykOrderStates.Accepted;
                case "picking": return AlopeykOrderStates.Picking;
                case "delivering": return AlopeykOrderStates.Delivering;
                case "delivered": return AlopeykOrderStates.Delivered;
                case "finished": return AlopeykOrderStates.Finished;
                case "scheduled": return AlopeykOrderStates.Scheduled;
                default:
                {
                    return AlopeykOrderStates.Unknown;
                }
            }
        }

        protected virtual AlopeykTransportTypes StringToTransportType(
            string transportType
        )
        {
            switch (transportType?.ToLower())
            {
                case "motorbike":
                    return AlopeykTransportTypes.Motorbike;
                case "motor_taxi":
                    return AlopeykTransportTypes.MotorTaxi;
                case "cargo":
                    return AlopeykTransportTypes.Cargo;
                case "cargo_s":
                    return AlopeykTransportTypes.CargoS;
                case "car":
                    return AlopeykTransportTypes.Car;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transportType));
            }
        }

        protected virtual string TransportTypeToString(
            AlopeykTransportTypes transportType
        )
        {
            switch (transportType)
            {
                case AlopeykTransportTypes.Motorbike:
                    return "motorbike";
                case AlopeykTransportTypes.MotorTaxi:
                    return "motor_taxi";
                case AlopeykTransportTypes.Cargo:
                    return "cargo";
                case AlopeykTransportTypes.CargoS:
                    return "cargo_s";
                case AlopeykTransportTypes.Car:
                    return "car";
                default:
                    throw new ArgumentOutOfRangeException(nameof(transportType), transportType, null);
            }
        }

        protected virtual async Task<HttpResponseMessage> Send(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var req = new HttpRequestMessage(request.Method, request.RequestUri)
            {
                Content = request.Content,
                Version = request.Version,
            };

            foreach (var kv in request.Headers)
            {
                req.Headers.Add(kv.Key, kv.Value);
            }

            req.Headers.Add("Authorization", $"Bearer {Token}");

            var response = await HttpClient.SendAsync(req, cancellationToken);

            return response;
        }
    }
}