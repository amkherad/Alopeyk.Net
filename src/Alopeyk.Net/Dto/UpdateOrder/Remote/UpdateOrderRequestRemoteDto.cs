// ReSharper disable InconsistentNaming
namespace Alopeyk.Net.Dto.UpdateOrder.Remote
{
    internal class UpdateOrderRequestRemoteDto
    {
        public bool has_return { get; set; }
            
        public bool cashed { get; set; }
            
        public bool credit { get; set; }
            
        public bool pay_at_dest { get; set; }
            
        public int delay { get; set; }
            
        public string transport_type { get; set; }
    }
}