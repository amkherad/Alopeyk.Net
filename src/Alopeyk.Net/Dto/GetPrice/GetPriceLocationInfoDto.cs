using System.Collections.Generic;

namespace Alopeyk.Net.Dto.GetPrice
{
    public class GetPriceLocationInfoDto
    {
        public GetPriceAddressRequestDto Origin { get; set; }
        
        public IEnumerable<GetPriceAddressRequestDto> Destinations { get; set; }
    }
}