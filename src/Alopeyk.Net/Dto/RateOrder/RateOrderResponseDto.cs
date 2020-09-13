using System;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net.Dto.RateOrder
{
    public class RateOrderResponseDto
    {
        public AlopeykTransportTypes TransportType { get; set; }

        public int Id { get; set; }

        public string Status { get; set; }

        public int CourierId { get; set; }

        public int CustomerId { get; set; }

        public DateTime AcceptedAt { get; set; }

        public string City { get; set; }

        public int Price { get; set; }

        public int Rate { get; set; }

        public object NextAddressAny { get; set; }

        public ResourceDescriptorDto Signature { get; set; }

        public object OrderToken { get; set; }

        public object NPrice { get; set; }

        public object Subsidy { get; set; }

        public string SignedBy { get; set; }

        public int FinalPrice { get; set; }
    }
}