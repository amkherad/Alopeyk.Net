using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.DeleteHiddenDescription;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public Task<BaseResponseDto<DeleteHiddenDescriptionResponseDto>> DeleteHiddenDescription(
            DeleteHiddenDescriptionRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }
    }
}