
namespace Application.Bookings.Dtos;

public class BookingDto
{
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int RoomID { get; set; }

    public int GuestId { get; set; }

    public int StatusId { get; set; }
}

