namespace Alopeyk.Net.Dto.GetLocation
{
    public class GetLocationResponseDto
    {
        public string Province { get; set; }
        
        public string CityFa { get; set; }
        
        public string City { get; set; }
        
        public string District { get; set; }
        
        public string Region { get; set; }

        public string[] Address { get; set; }

        public TrafficZoneDto TrafficZone { get; set; }
    }
}