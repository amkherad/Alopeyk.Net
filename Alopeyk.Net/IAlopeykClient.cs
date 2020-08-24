using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.DTOs.GetPrice;

namespace Alopeyk.Net
{
    public interface IAlopeykClient
    {
        Task<object> GetLocation(
            double latitude,
            double longitude,
            CancellationToken cancellationToken
        );


        Task<GetPriceResponseDto> GetPrice(
            GetPriceRequestDto requestDto,
            CancellationToken cancellationToken
        );

        Task<object> TakeAlopeyk(
            string appId,
            int orderId,
            string transportType,
            string city,
            object[] addresses,
            bool hasReturn,
            bool cashed,
            CancellationToken cancellationToken
        );

        Task<object> GetAlopeykOrderStatus(
            long transferId,
            CancellationToken cancellationToken
        );


        Task<object> CancelAlopeyk(
            long transferId,
            CancellationToken cancellationToken
        );
    }
}