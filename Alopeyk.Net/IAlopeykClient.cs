using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.DTOs.GetPrice;

namespace Alopeyk.Net
{
    public interface IAlopeykClient
    {
        Task<BaseResponseDto<GetLocationResponseDto>> GetLocation(
            GetLocationRequestDto request,
            CancellationToken cancellationToken
        );


        Task<GetPriceResponseDto> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        );


        Task<InsertOrderResponseDto> InsertOrder(
            InsertOrderRequestDto request,
            CancellationToken cancellationToken
        );


        Task<GetOrderStatusResponseDto> GetOrderStatus(
            GetOrderStatusRequestDto request,
            CancellationToken cancellationToken
        );


        Task<CancelOrderResponseDto> CancelOrder(
            CancelOrderRequestDto request,
            CancellationToken cancellationToken
        );

        
        Task<string> GetLiveMapLink(
            string token,
            CancellationToken cancellationToken
        );
    }
}