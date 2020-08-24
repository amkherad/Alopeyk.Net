using System.Collections.Generic;

namespace Alopeyk.Net
{
    public class GetPriceObjectResponseDto
    {
        public List<GetPriceAddressResponseDto> Addresses { get; set; }
        public string Price { get; set; }
        public string Credit { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
        public string User_credit { get; set; }
        public string Delay { get; set; }
        public string City { get; set; }
        public string Transport_type { get; set; }
        public string Has_return { get; set; }
        public string Cashed { get; set; }
        public string Price_with_return { get; set; }
    }
}
