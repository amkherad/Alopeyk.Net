using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.AddHiddenDescription;
using Alopeyk.Net.Dto.GetPrice;

namespace Alopeyk.Net
{
    public partial class AlopeykClient
    {
        public Task<BaseResponseDto<AddHiddenDescriptionResponseDto>> AddHiddenDescription(
            AddHiddenDescriptionRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }
    }
}