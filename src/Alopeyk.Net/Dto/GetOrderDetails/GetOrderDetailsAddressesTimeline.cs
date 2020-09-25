namespace Alopeyk.Net.Dto.GetOrderDetails
{
    public class GetOrderDetailsAddressesTimeline
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public ResourceDescriptorDto Signature { get; set; }
        public string CityFa { get; set; }
    }
}