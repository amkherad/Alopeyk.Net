// ReSharper disable InconsistentNaming
namespace Alopeyk.Net.Dto.InsertOrder.Remote
{
    internal class InsertOrderAddressRequestRemoteDto
    {
        public string type { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string description { get; set; }
        public string unit { get; set; }
        public string number { get; set; }
        public string person_fullname { get; set; }
        public string person_phone { get; set; }
    }
}