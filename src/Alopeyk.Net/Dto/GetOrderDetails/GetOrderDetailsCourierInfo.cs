namespace Alopeyk.Net.Dto.GetOrderDetails
{
    public class GetOrderDetailsCourierInfo
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ReferralCode { get; set; }
        public string PlateNumber { get; set; }
        public string RatesAverage { get; set; }
        public ResourceDescriptorDto Avatar { get; set; }
        public ResourceDescriptorDto AbsAvatar { get; set; }
        public string LastOnline { get; set; }
        public bool IsOnline { get; set; }
    }
}