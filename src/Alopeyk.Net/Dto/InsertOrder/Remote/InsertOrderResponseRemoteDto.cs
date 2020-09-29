using System;
// ReSharper disable InconsistentNaming

namespace Alopeyk.Net.Dto.InsertOrder.Remote
{
    internal class InsertOrderResponseRemoteDto
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
        public string device_id { get; set; }
        public int weight { get; set; }
        public bool is_api { get; set; }
        public bool is_vip { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public int id { get; set; }
        public ResourceDescriptorDto signature { get; set; }
        public string order_token { get; set; }
        public object nprice { get; set; }
        public object subsidy { get; set; }
        public string signed_by { get; set; }
        public int final_price { get; set; }
        public InsertOrderScoreRemoteDto score_calc { get; set; }
        public decimal order_discount { get; set; }
        public object extra_param { get; set; }
        public decimal orderDiscount { get; set; }
    }
}