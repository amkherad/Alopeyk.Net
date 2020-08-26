using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net
{
    public partial class AlopeykClient : IAlopeykClient, IDisposable
    {
        private const string ApplicationJsonMime = "application/json";

        private const string GetLocationV2EndpointPath = "v2/locations";
        private const string GetPriceV2EndpointPath = "v2/orders/price/calc";
        private const string GetPricesV2EndpointPath = "v2/orders/batch-price";
        private const string GetOrderStatusV2EndpointPath = "v2/orders/{order_id}";
        private const string InsertOrderV2EndpointPath = "v2/orders";
        private const string CancelOrderV2EndpointPath = "v2/orders/{order_id}/cancel";

        private const string OrderIdPlaceholder = "{order_id}";

        public Uri RemoteServiceUri { get; }

        public string Token { get; set; }

        public HttpClient HttpClient { get; }

        public bool DisposeHttpClient { get; set; } = true;

        public IJsonSerializer JsonSerializer { get; }


        public AlopeykClient(
            Uri remoteServiceUri,
            string token,
            HttpClient httpClient,
            IJsonSerializer jsonSerializer
        )
        {
            RemoteServiceUri = remoteServiceUri;
            Token = token;
            HttpClient = httpClient;
            JsonSerializer = jsonSerializer;
        }


        public virtual void Dispose()
        {
            if (DisposeHttpClient)
            {
                HttpClient.Dispose();
            }
        }

        private string CreatePath(
            string relativePath
        )
        {
            return Path.Join(RemoteServiceUri.AbsolutePath, relativePath);
        }

        private void ThrowInvalidStatusCode(
            HttpResponseMessage response
        )
        {
            throw new AlopeykException($"Alopeyk remote service returned an invalid http status code, statusCode: {response.StatusCode}");
        }

        private void BindBaseResponse<T>(
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

        private AlopeykStatusCodes FormatStatusCode(
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
    }
}