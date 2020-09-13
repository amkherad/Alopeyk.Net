namespace Alopeyk.Net.Dto.GetOrderDetails.Remote
// ReSharper disable InconsistentNaming
{
    internal class GetOrderDetailsEtaMinimalResponseRemoteDto
    {
        public int id { get; set; }
        public int last_position_id { get; set; }
        public int duration { get; set; }
        public int distance { get; set; }
        public string action { get; set; }
        public string address_id { get; set; }
        public string updated_at { get; set; }
    }
}