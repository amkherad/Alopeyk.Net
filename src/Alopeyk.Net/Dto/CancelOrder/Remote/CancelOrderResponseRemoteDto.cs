// ReSharper disable InconsistentNaming
namespace Alopeyk.Net.Dto.CancelOrder.Remote
{
    internal class CancelOrderResponseRemoteDto
    {
        public int id { get; set; }

        public string status { get; set; }

        public int courier_id { get; set; }

        public int customer_id { get; set; }

        public ResourceDescriptorDto signature { get; set; }

        public string order_token { get; set; }

        public decimal? nprice { get; set; }

        public decimal? subsidy { get; set; }

        public string signed_by { get; set; }

        public decimal? final_price { get; set; }
    }
}