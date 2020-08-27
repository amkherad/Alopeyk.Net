using Alopeyk.Net.Enums;

namespace Alopeyk.Net.Dto
{
    public class CancelOrderResponseDto
    {
        public int Id { get; set; }
        
        public AlopeykOrderStates Status { get; set; }
        
        public int CourierId { get; set; }
        
        public int CustomerId { get; set; }
        
        public SignatureDto Signature { get; set; }
        
        public string OrderToken { get; set; }
        
        public string NPrice { get; set; }
        
        public string Subsidy { get; set; }
        
        public string SignedBy { get; set; }
        
        public string FinalPrice { get; set; }
    }
}