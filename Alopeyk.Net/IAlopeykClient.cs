using System.Collections.Generic;
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


        Task<IEnumerable<GetPriceResponseDto>> GetPrices(
            IEnumerable<GetPriceRequestDto> requests,
            CancellationToken cancellationToken
        );


        Task<InsertOrderResponseDto> InsertOrder(
            InsertOrderRequestDto request,
            CancellationToken cancellationToken
        );


        Task<GetOrderDetailsResponseDto> GetOrderDetails(
            GetOrderDetailsRequestDto request,
            CancellationToken cancellationToken
        );


        Task<UpdateOrderResponseDto> UpdateOrder(
            UpdateOrderRequestDto request,
            CancellationToken cancellationToken
        );


        Task<CancelOrderResponseDto> CancelOrder(
            CancelOrderRequestDto request,
            CancellationToken cancellationToken
        );


        Task<RateOrderResponseDto> RateOrder(
            RateOrderRequestDto request,
            CancellationToken cancellationToken
        );


        Task<AddHiddenDescriptionResponseDto> AddHiddenDescription(
            AddHiddenDescriptionRequestDto request,
            CancellationToken cancellationToken
        );

        Task<DeleteHiddenDescriptionResponseDto> DeleteHiddenDescription(
            DeleteHiddenDescriptionRequestDto request,
            CancellationToken cancellationToken
        );


        Task<string> GetLiveMapLink(
            string token,
            CancellationToken cancellationToken
        );
    }
}