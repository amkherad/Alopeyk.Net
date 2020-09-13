using System;

namespace Alopeyk.Net.Dto.DeleteHiddenDescription
{
    public class DeleteHiddenDescriptionResponseDto
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