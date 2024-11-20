
using Domain.Bookings.Entities;

namespace Application.Bookings.Dtos;

public class BookingDto
{
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int RoomId { get; set; }

    public int GuestId { get; set; }

    public int StatusId { get; set; }

    public static Booking MapToEntity(BookingDto dto)
    {
        return new Booking
        {
            Id = dto.Id,
            PlacedAt = dto.PlacedAt,
        };

    }

    public static BookingDto MapToDto(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            PlacedAt = booking.PlacedAt,
            Start = booking.Start,
            End = booking.End,
            GuestId = booking.Guest.Id,
            RoomId = booking.Room.Id,
            StatusId = (int)booking.Status
        };
    }

}


