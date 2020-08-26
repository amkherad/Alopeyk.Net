using Alopeyk.Net.Dto;

namespace Alopeyk.Net.DTOs.GetPrice
{
    public class GetPriceRequestDto
    {
        public string City { get; set; }

        public string TransportType { get; set; }

        public BaseAddressDto Addresses { get; set; }

        public bool HasReturn { get; set; }

        public bool Cashed { get; set; }
    }
}