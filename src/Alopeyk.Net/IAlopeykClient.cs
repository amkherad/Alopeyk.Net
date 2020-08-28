using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alopeyk.Net.Dto;
using Alopeyk.Net.Dto.AddHiddenDescription;
using Alopeyk.Net.Dto.CancelOrder;
using Alopeyk.Net.Dto.DeleteHiddenDescription;
using Alopeyk.Net.Dto.GetLiveMapLink;
using Alopeyk.Net.Dto.GetLocation;
using Alopeyk.Net.Dto.GetLocationSuggestions;
using Alopeyk.Net.Dto.GetOrderDetails;
using Alopeyk.Net.Dto.GetPrice;
using Alopeyk.Net.Dto.InsertOrder;
using Alopeyk.Net.Dto.RateOrder;

namespace Alopeyk.Net
{
    public interface IAlopeykClient
    {
        /// <summary>
        /// This endpoint retrieves place information by its latitude and longitude.
        /// </summary>
        /// <remarks>
        ///     GET https://sandbox-api.alopeyk.com/api/v2/locations
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<GetLocationResponseDto>> GetLocation(
            GetLocationRequestDto request,
            CancellationToken cancellationToken
        );

        
        /// <summary>
        /// This endpoint retrieves suggestions by search input.
        /// The result will be an array of suggestions. Each one includes the region and the name of the retrieved place, and offers coordinates for that item.
        /// </summary>
        /// <remarks>
        ///     GET https://sandbox-api.alopeyk.com/api/v2/locations
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<GetLocationSuggestionsResponseDto[]>> GetLocationSuggestions(
            GetLocationSuggestionsRequestDto request,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// Request a quote for an order with origin address and destination address.
        /// This endpoint retrieves calculation information for a pair of {latitude,longitude}s.
        /// </summary>
        /// <remarks>
        ///     POST https://sandbox-api.alopeyk.com/api/v2/orders/price/calc
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<GetPriceResponseDto>> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// This endpoint is the same as Normal Price But the difference is you can calculate up to 15 pairs of Normal Price in one request.
        /// </summary>
        /// <remarks>
        ///     POST https://sandbox-api.alopeyk.com/api/v2/orders/batch-price
        /// </remarks>
        /// <param name="requests"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<IEnumerable<GetPriceResponseDto>>> GetPrices(
            IEnumerable<GetPriceRequestDto> requests,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// Once you calculated the price of your order, you can use this endpoint in order to create a new order.
        /// </summary>
        /// <remarks>
        ///     POST https://sandbox-api.alopeyk.com/api/v2/orders
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<InsertOrderResponseDto>> InsertOrder(
            InsertOrderRequestDto request,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// In order to get the order details, call this endpoint.
        /// </summary>
        /// <remarks>
        ///     GET https://sandbox-api.alopeyk.com/api/v2/orders/{order_id}
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<GetOrderDetailsResponseDto>> GetOrderDetails(
            GetOrderDetailsRequestDto request,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// For order editions and updates, transport types of the same group can be changed to each other. This means that the transport types of motor orders can not be changed into transport types belonging to car or cargo group.
        /// 
        /// motor group => (“motorbike”, “motor_taxi”)
        /// car group => (“car”, “car_taxi”)
        /// cargo group => (“cargo”, “cargo_s”)
        /// If an order has a return policy and courier has started the trip back the origin address. The has_return parameter cannot be turned off. In order to edit the order details, this endpoint can be called: 
        /// </summary>
        /// <remarks>
        ///     PUT https://sandbox-api.alopeyk.com/api/v2/orders/{order_id}
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<UpdateOrderResponseDto>> UpdateOrder(
            UpdateOrderRequestDto request,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// You can cancel any order before courier arrival (before the accepted status)
        /// </summary>
        /// <remarks>
        ///     GET https://sandbox-api.alopeyk.com/api/v2/orders/{order_id}/cancel
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<CancelOrderResponseDto>> CancelOrder(
            CancelOrderRequestDto request,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// When an order is in its final status (delivered or returned due to the order’s has_return attribute),
        /// you can call this endpoint, to fill the rate and the comment attributes.
        /// </summary>
        /// <remarks>
        ///     POST https://sandbox-api.alopeyk.com/api/v2/orders/{order_id}/finish
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<RateOrderResponseDto>> RateOrder(
            RateOrderRequestDto request,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// This type of description is invisible for courier. It is worth noting that customer and AloPeyk support team can view content of this field.
        /// </summary>
        /// <remarks>
        ///     POST https://sandbox-api.alopeyk.com/api/v2/orders/{order_id}/address/{address_id}/hidden_description
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<AddHiddenDescriptionResponseDto>> AddHiddenDescription(
            AddHiddenDescriptionRequestDto request,
            CancellationToken cancellationToken
        );

        
        /// <summary>
        /// Use this method in order to delete hidden description fields.
        /// </summary>
        /// <remarks>
        ///     DELETE https://sandbox-api.alopeyk.com/api/v2/orders/{order_id}/address/{address_id}/hidden_description/{hidden_description_id}
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BaseResponseDto<DeleteHiddenDescriptionResponseDto>> DeleteHiddenDescription(
            DeleteHiddenDescriptionRequestDto request,
            CancellationToken cancellationToken
        );


        /// <summary>
        /// Once you successfully have created an order, you will be able to watch the courier on a live map.
        /// At Sandbox environment, courier location is static and order status will change every 30 seconds.
        /// But at Production environment, the courier location and order status based on reality will be change.
        /// You can access tracking URL (tracking_url) trough Webhook data.
        /// Even you can manually create this URL by concatenation Order Token (‘order_token’)
        /// which is accessible in Order details method and the tracking base URL
        /// </summary>
        /// <remarks>
        ///     https://sandbox-tracking.alopeyk.com/#/{order_token}
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetLiveMapLink(
            GetLiveMapLinkRequestDto request,
            CancellationToken cancellationToken
        );
    }
}