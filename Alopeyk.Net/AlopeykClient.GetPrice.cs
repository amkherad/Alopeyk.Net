using System;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.DTOs.GetPrice;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public virtual async Task<GetPriceResponseDto> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }
    }
}