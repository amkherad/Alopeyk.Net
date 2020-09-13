using System;
// ReSharper disable InconsistentNaming

namespace Alopeyk.Net.Dto.GetOrderDetails.Remote
{
    internal class GetOrderDetailsAddressResponseRemoteDto
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string type { get; set; }
        public int priority { get; set; }
        public DateTime arrived_at { get; set; }
        public DateTime handled_at { get; set; }
        public int id { get; set; }
        public string city { get; set; }
        public string order_id { get; set; }
        public string customer_id { get; set; }
        public string courier_id { get; set; }
        public string status { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string unit { get; set; }
        public string number { get; set; }
        public string person_fullname { get; set; }
        public string person_phone { get; set; }
        public string signed_by { get; set; }
        public string distance { get; set; }
        public string duration { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string deleted_at { get; set; }
        public string arrive_lat { get; set; }
        public string arrive_lng { get; set; }
        public string handle_lat { get; set; }
        public string handle_lng { get; set; }
        public ResourceDescriptorDto signature { get; set; }
        public string city_fa { get; set; }
    }
}