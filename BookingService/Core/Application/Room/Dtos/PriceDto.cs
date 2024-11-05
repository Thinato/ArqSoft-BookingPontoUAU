namespace Application.Rooms.Dtos
{
    public class PriceDto
    {
        public decimal Value { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}