using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.GetOrderDetails;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public Task<BaseResponseDto<GetOrderDetailsResponseDto>> GetOrderDetails(
            GetOrderDetailsRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }
    }
}