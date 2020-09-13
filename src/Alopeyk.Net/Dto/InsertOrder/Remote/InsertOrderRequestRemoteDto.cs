// ReSharper disable InconsistentNaming
namespace Alopeyk.Net.Dto.InsertOrder.Remote
{
    internal class InsertOrderRequestRemoteDto
    {
        public string transport_type { get; set; }

        public InsertOrderAddressRequestRemoteDto[] addresses { get; set; }

        public bool has_return { get; set; }

        public int? delay { get; set; }

        public string scheduled_at { get; set; }

        public bool cashed { get; set; }

        public object extra_param { get; set; }
    }
}