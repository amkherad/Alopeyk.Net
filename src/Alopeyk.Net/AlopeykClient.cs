using System;
using System.Linq;
using System.Net.Http;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net
{
    public partial class AlopeykClient : IAlopeykClient, IDisposable
    {
        private const string ApplicationJsonMime = "application/json";

        private const string GetOrderStatusV2EndpointPath = "v2/orders/{order_id}";
        private const string InsertOrderV2EndpointPath = "v2/orders";

        private const string OrderIdPlaceholder = "{order_id}";

        public Uri RemoteServiceUri { get; }

        public string Token { get; set; }

        public HttpClient HttpClient { get; }

        public bool DisposeHttpClient { get; set; } = true;

        public IJsonSerializer JsonSerializer { get; }

        public IRetryHandler RetryHandler { get; }


        public AlopeykClient(
            Uri remoteServiceUri,
            string token,
            HttpClient httpClient,
            IJsonSerializer jsonSerializer,
            IRetryHandler retryHandler
        )
        {
            RemoteServiceUri = remoteServiceUri;
            Token = token;
            HttpClient = httpClient;
            JsonSerializer = jsonSerializer;
            RetryHandler = retryHandler;
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
            return Helpers.JoinUrls(RemoteServiceUri.AbsolutePath, relativePath);
        }

        protected virtual void ThrowOnInvalidStatusCode(
            HttpResponseMessage response
        )
        {
            throw new AlopeykException(
                $"Alopeyk remote service returned an invalid http status code, statusCode: {response.StatusCode}");
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
                case "cancelled":
                {
                    return AlopeykOrderStates.Cancelled;
                }
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
    }
}