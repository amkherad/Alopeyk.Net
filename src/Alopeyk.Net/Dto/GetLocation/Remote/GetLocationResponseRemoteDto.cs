// ReSharper disable InconsistentNaming
namespace Alopeyk.Net.Dto.GetLocation.Remote
{
    internal class GetLocationResponseRemoteDto
    {
        public string[] address { get; set; }

        public string region { get; set; }

        public string district { get; set; }

        public string city { get; set; }

        public GetLocationTrafficZoneRemoteDto traffic_zone { get; set; }

        public string city_fa { get; set; }

        public string province { get; set; }
    }
}