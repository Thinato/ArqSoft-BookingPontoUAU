using Application.Bookings.Requests;
using Application.Bookings.Responses;
using Shared.Pagination;

namespace Application.Ports
{
    public interface IBookingManager
    {
        public Task<BookingResponse> Create(CreateBookingRequest request);
        public Task<BookingResponseList> GetBookings(PaginationQuery pagination);
        public Task<BookingResponseList> GetBookingsByRoom(int roomId, PaginationQuery pagination);
        public Task<BookingResponseList> GetBookingsByGuest(int guestId, PaginationQuery pagination);
        public Task<BookingResponse> PayBooking(CreateBookingRequest request);
    }
}

