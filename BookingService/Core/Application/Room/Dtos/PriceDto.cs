namespace Application.Rooms.Dtos
{
    public class PriceDto
    {
        public decimal Value { get; }
        public string Currency { get; } = string.Empty;
    }
}