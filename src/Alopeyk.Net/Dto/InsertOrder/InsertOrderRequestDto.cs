using Alopeyk.Net.Enums;

namespace Alopeyk.Net.Dto.InsertOrder
{
    public class InsertOrderRequestDto
    {
        /// <summary>
        /// The transport type of the order. Current valid values for this attribute are motorbike for simple package delivery, motor_taxi for passenger transportions, cargo for cargo, cargo_s for Small Cargo, and car for Car transportations.
        /// </summary>
        public AlopeykTransportTypes TransportType { get; set; }
        
        public InsertOrderLocationInfoDto Addresses { get; set; }
        
        
        /// <summary>
        /// If you are going to calculate price for an order which has a return option, set it to true.
        /// </summary>
        public bool HasReturn { get; set; }
        
        /// <summary>
        /// You can apply a integer value in minutes to add a stop time in the locations. If set to this option, you can not reduce it and you can only increase its value.
        /// </summary>
        public int DelayInMinutes { get; set; }
        
        /// <summary>
        /// A timestamp (2020-10-25 18:45) that decides whether you would like to create a scheduled order which will be launched at you desired date and time.
        /// </summary>
        public string ScheduledAt { get; set; }
        
        /// <summary>
        /// If you are going to force the payment type as cash, set it to true.
        /// </summary>
        public bool Cashed { get; set; }
        
        /// <summary>
        /// A JSON object allowing your database to be mapped with AloPeyk’s orders (ex : { my_order_id : 3333 } ). This object is returned on each webhook call.
        /// </summary>
        public object ExtraParams { get; set; }
    }
}