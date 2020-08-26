using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public Task<GetOrderStatusResponseDto> GetOrderStatus(
            GetOrderStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }
    }
}