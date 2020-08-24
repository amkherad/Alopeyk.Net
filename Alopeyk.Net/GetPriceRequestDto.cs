using System.Collections.Generic;

namespace Alopeyk.Net
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