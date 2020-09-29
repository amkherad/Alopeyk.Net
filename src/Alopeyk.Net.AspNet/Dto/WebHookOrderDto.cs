using System;
using System.Collections.Generic;
using Alopeyk.Net.Dto;

namespace Alopeyk.Net.AspNet.Dto
{
    public class WebHookOrderDto
    {
        public int id { get; set; } 
        public string invoice_number { get; set; } 
        public string status { get; set; } 
        public string city { get; set; } 
        public string transport_type { get; set; } 
        public int delay { get; set; } 
        public int distance { get; set; } 
        public int duration { get; set; } 
        public int price { get; set; } 
        public bool credit { get; set; } 
        public bool cashed { get; set; } 
        public bool has_return { get; set; } 
        public bool pay_at_dest { get; set; } 
        public double accept_lat { get; set; } 
        public double accept_lng { get; set; } 
        public ResourceDescriptorDto signature { get; set; } 
        public WebHookScreenshotDto screenshot { get; set; } 
        public string progress { get; set; } 
        public string order_token { get; set; } 
        public string tracking_url { get; set; } 
        public string cancelled_by { get; set; } 
        public DateTime? scheduled_at { get; set; } 
        public DateTime launched_at { get; set; } 
        public DateTime accepted_at { get; set; } 
        public DateTime? picking_at { get; set; } 
        public DateTime? delivering_at { get; set; } 
        public DateTime? delivered_at { get; set; } 
        public DateTime? finished_at { get; set; } 
        public DateTime? stopped_at { get; set; } 
        public DateTime created_at { get; set; } 
        public DateTime updated_at { get; set; } 
        public List<WebHookAddressDto> addresses { get; set; } 
        public WebHookCourierDto courier { get; set; } 
        public WebHookEtaMinimalDto eta_minimal { get; set; } 
    }
}