namespace Alopeyk.Net.Dto.GetOrderDetails
{
    public class GetOrderDetailsEtaMinimal
    {
        public int Id { get; set; }
        public int LastPositionId { get; set; }
        public int Duration { get; set; }
        public int Distance { get; set; }
        public string Action { get; set; }
        public string AddressId { get; set; }
        public string UpdatedAt { get; set; }
    }
}