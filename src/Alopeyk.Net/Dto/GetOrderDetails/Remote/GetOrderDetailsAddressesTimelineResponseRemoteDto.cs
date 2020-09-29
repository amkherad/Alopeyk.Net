namespace Alopeyk.Net.Dto.GetOrderDetails.Remote
// ReSharper disable InconsistentNaming
{
    internal class GetOrderDetailsAddressesTimelineResponseRemoteDto
    {
        public int id { get; set; }
        public int priority { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public ResourceDescriptorDto signature { get; set; }
        public string city_fa { get; set; }
    }
}