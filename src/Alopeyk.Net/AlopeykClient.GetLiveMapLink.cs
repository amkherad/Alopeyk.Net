using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        private const string LiveTrackingUrl = "https://sandbox-tracking.alopeyk.com/#/";

        public async Task<string> GetLiveMapLink(
            GetLiveMapLinkRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var values = new Dictionary<string, string>
            {
                {"logo", request.Logo},
                {"customer_image", request.CustomerImage},
                {"customer_name", request.CustomerName},
                {"show_payment_status", request.ShowPaymentStatus?.ToString()},
                {"show_order_price", request.ShowOrderPrice?.ToString()},
                {"show_courier_phone", request.ShowCourierPhone?.ToString()},
                {"show_order_info_box", request.ShowOrderInfoBox?.ToString()},
            };

            var dict = values.Where(kv => !(kv.Value is null))
                .ToDictionary(kv => kv.Key, kv => Uri.EscapeDataString(kv.Value));

            var query = string.Join('&', dict.Select(kv => $"{kv.Key}={kv.Value}"));

            if (string.IsNullOrWhiteSpace(query))
            {
                return $"{LiveTrackingUrl}{request.OrderToken}";
            }

            return $"{LiveTrackingUrl}{request.OrderToken}?{query}";
        }
    }
}