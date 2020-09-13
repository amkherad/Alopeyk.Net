namespace Alopeyk.Net.Dto.GetPrice.Remote
// ReSharper disable InconsistentNaming
{
    internal class GetPriceRequestRemoteDto
    {
        public string transport_type { get; set; }

        public GetPriceAddressRequestRemoteDto[] addresses { get; set; }

        public bool has_return { get; set; }

        public bool cached { get; set; }

        //public bool? optimized { get; set; }
    }
}