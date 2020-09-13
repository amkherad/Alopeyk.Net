using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Alopeyk.Net.Dto.GetPrice.Remote
{
    internal class GetPriceResponseRemoteDto
    {
        public GetPriceResponseRemoteAddress[] addresses { get; set; } 
        public int price { get; set; } 
        public bool credit { get; set; } 
        public int distance { get; set; } 
        public int duration { get; set; } 
        public string status { get; set; } 
        public int user_credit { get; set; } 
        public int delay { get; set; } 
        public string city { get; set; } 
        public string city_fa { get; set; } 
        public string transport_type { get; set; } 
        public bool has_return { get; set; } 
        public bool cashed { get; set; } 
        public decimal price_with_return { get; set; } 
        public int score { get; set; } 
        public Dictionary<string, double> score_detail { get; set; } 
        public int final_price { get; set; } 
        public int discount { get; set; } 
        public string[] discount_coupons { get; set; } 
        public string[] invalid_discount_coupons { get; set; } 
        public int failed_final_price { get; set; } 
        public int failed_discount { get; set; } 
        public string[] failed_discount_coupons { get; set; } 
        public bool scheduled { get; set; } 
    }
}