namespace Alopeyk.Net.Dto.GetOrderDetails.Remote
// ReSharper disable InconsistentNaming
{
    internal class GetOrderDetailsCourierInfoResponseRemoteDto
    {
        public int id { get; set; }
        public string phone { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string referral_code { get; set; }
        public string plate_number { get; set; }
        public string rates_avg { get; set; }
        public ResourceDescriptorDto avatar { get; set; }
        public ResourceDescriptorDto abs_avatar { get; set; }
        public string last_online { get; set; }
        public bool? is_online { get; set; }
    }
}