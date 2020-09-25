using System;
using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Alopeyk.Net.Dto.GetOrderDetails.Remote
{
    internal class GetOrderDetailsResponseRemoteDto
    {
            public int id { get; set; }
            public string invoice_number { get; set; }
            public int customer_id { get; set; }
            public string device_id { get; set; }
            public int courier_id { get; set; }
            public string cancelled_by { get; set; }
            public string status { get; set; }
            public int distance { get; set; }
            public int duration { get; set; }
            public int price { get; set; }
            public bool credit { get; set; }
            public bool cashed { get; set; }
            public bool has_return { get; set; }
            public bool pay_at_dest { get; set; }
            public int delay { get; set; }
            public int is_vip { get; set; }
            public string transport_type { get; set; }
            public string city { get; set; }
            public bool is_api { get; set; }
            public int weight { get; set; }
            public int traffic_odd_even_zone { get; set; }
            public int traffic_congestion_zone { get; set; }
            public double accept_lat { get; set; }
            public double accept_lng { get; set; }
            public int rate { get; set; }
            public string comment { get; set; }
            public DateTime? scheduled_at { get; set; }
            public DateTime? launched_at { get; set; }
            public DateTime? accepted_at { get; set; }
            public DateTime? delivered_at { get; set; }
            public DateTime? finished_at { get; set; }
            public DateTime? stopped_at { get; set; }
            public DateTime? removed_at { get; set; }
            public DateTime created_at { get; set; }
            public DateTime? updated_at { get; set; }
            public DateTime? deleted_at { get; set; }
            public DateTime? picking_at { get; set; }
            public DateTime? delivering_at { get; set; }
            public ResourceDescriptorDto screenshot { get; set; }
            public List<GetOrderDetailsAddressResponseRemoteDto> addresses { get; set; }
            public GetOrderDetailsEtaMinimalResponseRemoteDto eta_minimal { get; set; }
            public GetOrderDetailsCourierInfoResponseRemoteDto courier_info { get; set; }
            public DateTime launched_or_created_at { get; set; }
            public string progress { get; set; }
            public object last_position_minimal { get; set; }
            public List<GetOrderDetailsAddressesTimelineResponseRemoteDto> addresses_timeline { get; set; }
            public object next_address_any_full { get; set; }
            public ResourceDescriptorDto signature { get; set; }
            public string order_token { get; set; }
            public decimal? nprice { get; set; }
            public decimal? subsidy { get; set; }
            public string signed_by { get; set; }
            public int final_price { get; set; }
            public GetOrderDetailsScoreResponseRemoteDto score_calc { get; set; }
            public decimal? order_discount { get; set; }
            public object extra_param { get; set; }
            public GetOrderDetailsCourierVehicleInfoResponseRemoteDto courier_vehicle { get; set; }
            public object orderDiscount { get; set; }
            public int customerScore { get; set; }
            public object courierVehicle { get; set; }
    }
}