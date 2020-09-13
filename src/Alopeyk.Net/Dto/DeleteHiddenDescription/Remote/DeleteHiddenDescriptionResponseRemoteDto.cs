using System;
// ReSharper disable InconsistentNaming

namespace Alopeyk.Net.Dto.DeleteHiddenDescription.Remote
{
    internal class DeleteHiddenDescriptionResponseRemoteDto
    {
        public int id { get; set; }
            
        public int user_id { get; set; }
            
        public int order_id { get; set; }
            
        public int address_id { get; set; }
            
        public string description { get; set; }
            
        public DateTime updated_at { get; set; }
            
        public DateTime created_at { get; set; }
    }
}