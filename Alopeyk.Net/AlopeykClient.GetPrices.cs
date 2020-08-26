using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.DTOs.GetPrice;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public Task<IEnumerable<GetPriceResponseDto>> GetPrices(
            IEnumerable<GetPriceRequestDto> requests,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }
    }
}