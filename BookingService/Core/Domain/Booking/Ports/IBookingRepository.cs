using Domain.Bookings.Entities;
using Shared.Pagination;

namespace Domain.Bookings.Ports;

public interface IBookingRepository
{
    Task<Booking> Create(Booking booking);
    Task<Booking?> Get(int bookingId);
    Task<(IEnumerable<Booking>, PaginationInfo)> ListBookings(PaginationQuery pagination);
    Task<(IEnumerable<Booking>, PaginationInfo)> ListBookingsByRoom(int roomId, PaginationQuery pagination);
    Task<(IEnumerable<Booking>, PaginationInfo)> ListBookingsByGuest(int guestId, PaginationQuery pagination);
}

