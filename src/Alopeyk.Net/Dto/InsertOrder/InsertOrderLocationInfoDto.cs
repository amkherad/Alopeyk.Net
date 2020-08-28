using System.Collections.Generic;
using Alopeyk.Net.Dto.GetPrice;

namespace Alopeyk.Net.Dto.InsertOrder
{
    public class InsertOrderLocationInfoDto
    {
        public InsertOrderAddressRequestDto Origin { get; set; }
        
        public IEnumerable<InsertOrderAddressRequestDto> Destinations { get; set; }
    }
}