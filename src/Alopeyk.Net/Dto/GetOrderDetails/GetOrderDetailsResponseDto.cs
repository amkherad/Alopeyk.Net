using System;
using System.Collections.Generic;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net.Dto.GetOrderDetails
{
    public class GetOrderDetailsAddressResponseDto
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Type { get; set; }
        public int Priority { get; set; }
        public DateTime ArrivedAt { get; set; }
        public DateTime HandledAt { get; set; }
        public int Id { get; set; }
        public string City { get; set; }
        public string CityFa { get; set; }
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CourierId { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Number { get; set; }
        public string PersonFullName { get; set; }
        public string PersonPhone { get; set; }
        public string SignedBy { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
        public string ArriveLatitude { get; set; }
        public string ArriveLongitude { get; set; }
        public string HandleLatitude { get; set; }
        public string HandleLongitude { get; set; }
        public RemoteResourceDto Signature { get; set; }
    }

    public class GetOrderDetailsEtaMinimal
    {
        public int Id { get; set; }
        public int LastPositionId { get; set; }
        public int Duration { get; set; }
        public int Distance { get; set; }
        public string Action { get; set; }
        public string AddressId { get; set; }
        public string UpdatedAt { get; set; }
    }

    public class GetOrderDetailsCourierInfo
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ReferralCode { get; set; }
        public string PlateNumber { get; set; }
        public string RatesAverage { get; set; }
        public RemoteResourceDto Avatar { get; set; }
        public RemoteResourceDto AbsAvatar { get; set; }
        public object LastOnline { get; set; }
        public object IsOnline { get; set; }
    }

    public class GetOrderDetailsAddressesTimeline
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public object Signature { get; set; }
        public object CityFa { get; set; }
    }

    public class GetOrderDetailsScoreCalc
    {
        public int Score { get; set; }
        public Dictionary<int, string> ScoreDetail { get; set; }
    }


    public class GetOrderDetailsResponseDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int CustomerId { get; set; }
        public object DeviceId { get; set; }
        public int CourierId { get; set; }
        public object CancelledBy { get; set; }
        public string Status { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public bool Credit { get; set; }
        public bool Cashed { get; set; }
        public bool HasReturn { get; set; }
        public bool PayAtDest { get; set; }
        public int Delay { get; set; }
        public int IsVip { get; set; }
        public AlopeykTransportTypes TransportType { get; set; }
        public string City { get; set; }
        public bool IsApi { get; set; }
        public int Weight { get; set; }
        public int TrafficOddEvenZone { get; set; }
        public int TrafficCongestionZone { get; set; }
        public double AcceptLatitude { get; set; }
        public double AcceptLongitude { get; set; }
        public int Rate { get; set; }
        public object Comment { get; set; }
        public object ScheduledAt { get; set; }
        public DateTime LaunchedAt { get; set; }
        public DateTime AcceptedAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public object FinishedAt { get; set; }
        public object StoppedAt { get; set; }
        public object RemovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public object DeletedAt { get; set; }
        public DateTime PickingAt { get; set; }
        public DateTime DeliveringAt { get; set; }
        public RemoteResourceDto Screenshot { get; set; }
        public GetOrderDetailsAddressResponseDto[] Addresses { get; set; }
        public GetOrderDetailsEtaMinimal EtaMinimal { get; set; }
        public GetOrderDetailsCourierInfo CourierInfo { get; set; }
        public DateTime LaunchedOrCreatedAt { get; set; }
        public string Progress { get; set; }
        public object LastPositionMinimal { get; set; }
        public GetOrderDetailsAddressesTimeline[] AddressesTimeline { get; set; }
        public object NextAddressAnyFull { get; set; }
        public RemoteResourceDto Signature { get; set; }
        public string OrderToken { get; set; }
        public object NPrice { get; set; }
        public object Subsidy { get; set; }
        public string SignedBy { get; set; }
        public int FinalPrice { get; set; }
        public GetOrderDetailsScoreCalc ScoreCalc { get; set; }
        public object OrderDiscount { get; set; }
        public object ExtraParam { get; set; }
        public object CourierVehicle { get; set; }
        public int CustomerScore { get; set; }
    }
}