namespace Alopeyk.Net.Dto.GetOrderDetails.Remote
{
    public class GetOrderDetailsCourierVehicleInfoResponseRemoteDto
    {
        public class VehicleDetails
        {
            public string brand_name { get; set; }
            
            public string model_name { get; set; }
            
            public string public_name { get; set; }
        }
        
        
        public string color { get; set; }
        
        public string build_year { get; set; }
        
        public string plate_number { get; set; }
        
        public string color_fa { get; set; }
        
        public VehicleDetails vehicle { get; set; }
    }
}