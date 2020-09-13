using System;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net.Dto.InsertOrder
{
    public class InsertOrderResponseDto
    {
        public AlopeykTransportTypes TransportType { get; set; }
        public int CustomerId { get; set; }
        public AlopeykOrderStates Status { get; set; }
        public bool TrafficCongestionZone { get; set; }
        public bool TrafficOddEvenZone { get; set; }
        public DateTime LaunchedAt { get; set; }
        public string City { get; set; }
        public int Delay { get; set; }
        public int Price { get; set; }
        public bool Credit { get; set; }
        public bool Cashed { get; set; }
        public bool HasReturn { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public string InvoiceNumber { get; set; }
        public bool PayAtDest { get; set; }
        public object DeviceId { get; set; }
        public int Weight { get; set; }
        public bool IsApi { get; set; }
        public bool IsVip { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
        public ResourceDescriptorDto Signature { get; set; }
        public string OrderToken { get; set; }
        public object NPrice { get; set; }
        public object Subsidy { get; set; }
        public string SignedBy { get; set; }
        public int FinalPrice { get; set; }
        public InsertOrderScoreInfoDto ScoreInfo { get; set; }
        public object OrderDiscount { get; set; }
        public object ExtraParam { get; set; }
    }
}