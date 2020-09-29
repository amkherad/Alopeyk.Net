using System;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net.Dto.GetOrderDetails
{
    public class GetOrderDetailsResponseDto
    {
        public int Id { get; set; }
        public AlopeykTransportTypes TransportType { get; set; }
        public AlopeykOrderStates Status { get; set; }
        public string InvoiceNumber { get; set; }
        public int CustomerId { get; set; }
        public string DeviceId { get; set; }
        public int CourierId { get; set; }
        public string CancelledBy { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public bool Credit { get; set; }
        public bool Cashed { get; set; }
        public bool HasReturn { get; set; }
        public bool PayAtDest { get; set; }
        public int Delay { get; set; }
        public int IsVip { get; set; }
        public string City { get; set; }
        public bool IsApi { get; set; }
        public int Weight { get; set; }
        public int TrafficOddEvenZone { get; set; }
        public int TrafficCongestionZone { get; set; }
        public double AcceptLatitude { get; set; }
        public double AcceptLongitude { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? LaunchedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime? StoppedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? PickingAt { get; set; }
        public DateTime? DeliveringAt { get; set; }
        public ResourceDescriptorDto Screenshot { get; set; }
        public GetOrderDetailsAddressResponseDto[] Addresses { get; set; }
        public GetOrderDetailsEtaMinimal EtaMinimal { get; set; }
        public GetOrderDetailsCourierInfo CourierInfo { get; set; }
        public DateTime LaunchedOrCreatedAt { get; set; }
        public string Progress { get; set; }
        public object LastPositionMinimal { get; set; }
        public GetOrderDetailsAddressesTimeline[] AddressesTimeline { get; set; }
        public string NextAddressAnyFull { get; set; }
        public ResourceDescriptorDto Signature { get; set; }
        public string OrderToken { get; set; }
        public decimal? NPrice { get; set; }
        public decimal? Subsidy { get; set; }
        public string SignedBy { get; set; }
        public int FinalPrice { get; set; }
        public GetOrderDetailsScoreCalc ScoreCalc { get; set; }
        public decimal? OrderDiscount { get; set; }
        public object ExtraParam { get; set; }
        public GetOrderDetailsCourierVehicleResponseDto CourierVehicle { get; set; }
        public int CustomerScore { get; set; }
    }
}