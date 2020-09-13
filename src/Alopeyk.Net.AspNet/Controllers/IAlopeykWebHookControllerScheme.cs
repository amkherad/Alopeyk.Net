using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.AspNet.Dto;
using Alopeyk.Net.Dto;

namespace Alopeyk.Net.AspNet.Controllers
{
    public interface IAlopeykWebHookControllerScheme
    {
        Task<BaseResponseDto<bool>> WebHook(
            WebHookDto webHookDto,
            CancellationToken cancellationToken
        );
    }
}