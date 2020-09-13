using System;

namespace Alopeyk.Net.Dto.AddHiddenDescription
{
    public class AddHiddenDescriptionResponseDto
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public int OrderId { get; set; }
        
        public int AddressId { get; set; }
        
        public string Description { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}