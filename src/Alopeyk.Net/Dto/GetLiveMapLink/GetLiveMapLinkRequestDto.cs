namespace Alopeyk.Net.Dto
{
    public class GetLiveMapLinkRequestDto
    {
        public string OrderToken { get; set; }
        
        /// <summary>
        /// Replace default “alopeyk logo” with your custom logo.
        /// </summary>
        public string Logo { get; set; }
        
        /// <summary>
        /// Replace your alopeyk profile image with your own customer image.
        /// </summary>
        public string CustomerImage { get; set; }
        
        /// <summary>
        /// Replace your alopeyk profile name with your own customer name.
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// By sending false for this variable, you can hide payment status.
        /// </summary>
        public bool? ShowPaymentStatus { get; set; }
        
        /// <summary>
        /// By sending false for this variable as value, you can hide order price box.
        /// </summary>
        public bool? ShowOrderPrice { get; set; }
        
        /// <summary>
        /// By sending false for this variable, you can hide courier phone number.
        /// </summary>
        public bool? ShowCourierPhone { get; set; }
        
        /// <summary>
        /// By sending false for this variable as value, you can hide order info box.
        /// </summary>
        public bool? ShowOrderInfoBox { get; set; }
    }
}