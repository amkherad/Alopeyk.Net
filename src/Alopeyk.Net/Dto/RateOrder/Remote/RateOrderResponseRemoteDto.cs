using System;
// ReSharper disable InconsistentNaming

namespace Alopeyk.Net.Dto.RateOrder.Remote
{
    internal class RateOrderResponseRemoteDto
    {
        public int id { get; set; }
        public string status { get; set; }
        public int courier_id { get; set; }
        public int customer_id { get; set; }
        public DateTime accepted_at { get; set; }
        public string city { get; set; }
        public string transport_type { get; set; }
        public int price { get; set; }
        public int rate { get; set; }
        public string next_address_any { get; set; }
        public ResourceDescriptorDto signature { get; set; }
        public string order_token { get; set; }
        public decimal nprice { get; set; }
        public decimal subsidy { get; set; }
        public string signed_by { get; set; }
        public int final_price { get; set; }
    }
}