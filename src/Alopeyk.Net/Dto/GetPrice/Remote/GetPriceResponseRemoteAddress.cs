namespace Alopeyk.Net.Dto.GetPrice.Remote
// ReSharper disable InconsistentNaming
{
    internal class GetPriceResponseRemoteAddress
    {
        public string type { get; set; } 
        public double lat { get; set; } 
        public double lng { get; set; } 
        public string address { get; set; } 
        public string city { get; set; } 
        public string city_fa { get; set; } 
        public int priority { get; set; } 
        public int? distance { get; set; } 
        public int? duration { get; set; } 
        public double? coefficient { get; set; } 
        public int? price { get; set; } 
    }
}