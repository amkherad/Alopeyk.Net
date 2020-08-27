using System.Collections.Generic;

namespace Alopeyk.Net.Dto.GetPrice
{
    public class GetPriceObjectResponseDto
    {
        public List<GetPriceAddressResponseDto> Addresses { get; set; }
        public string Price { get; set; }
        public string Credit { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
        public string UserCredit { get; set; }
        public string Delay { get; set; }
        public string City { get; set; }
        public string TransportType { get; set; }
        public string HasReturn { get; set; }
        public string Cashed { get; set; }
        public string PriceWithReturn { get; set; }
    }
}
