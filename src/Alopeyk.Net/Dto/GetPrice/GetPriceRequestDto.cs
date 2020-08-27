using Alopeyk.Net.Enums;

namespace Alopeyk.Net.Dto.GetPrice
{
    public class GetPriceRequestDto
    {
        /// <summary>
        /// The transport type of the order. Currently valid values for this attribute are motorbike for simple package delivery, motor_taxi for passenger transportations, cargo for cargo, cargo_s for Small Cargo, and car for Car transportations.
        /// </summary>
        public AlopeykTransportTypes TransportType { get; set; }

        public BaseAddressDto[] Addresses { get; set; }
        
        /// <summary>
        /// If you are going to calculate price for an order which has a return option, set it to true.
        /// </summary>
        public bool HasReturn { get; set; }

        /// <summary>
        /// If you are going to force the payment type as cash, set it to true.
        /// </summary>
        public bool Cashed { get; set; }
        
        /// <summary>
        /// Estimate an optimized route. Set to true to use on price calculations.
        /// </summary>
        public bool Optimized { get; set; }
    }
}